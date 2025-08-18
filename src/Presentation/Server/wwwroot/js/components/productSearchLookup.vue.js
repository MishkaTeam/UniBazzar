const SearchLookup = {
    emits: ['selected'],
    template: `
    <div class="search-box position-relative w-100">
      <label class="form-label" for="product-search">کالا</label>
      <input id="product-search" class="form-control" autocomplete="off"
             v-model="searchText"
             @input="onInputChanged"
             @keydown.down.prevent="moveSelection(1)"
             @keydown.up.prevent="moveSelection(-1)"
             @keydown.enter.prevent="selectSuggestion(selectedIndex)"
             @focus="showSuggestions = true"
             placeholder="جستجو..." />
      <div v-if="showSuggestions" class="search-suggestions shadow-sm p-2">
        <div v-if="isLoading" class="text-center py-3">
          <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">در حال جستجو</span>
          </div>
        </div>
        <div v-else-if="suggestions.length === 0" class="text-center py-3 text-muted">
          چیزی پیدا نشد
        </div>
        <div v-else v-for="(item, index) in suggestions" :key="index"
             class="suggestion-item" :class="{ 'bg-light': selectedIndex === index }"
             @click="selectSuggestion(index)">
          <div class="suggestion-text">
            <span class="suggestion-title" v-html="highlight(item.productTitle)"></span>
            <span v-if="item.category" class="suggestion-category">{{ item.category }}</span>
          </div>
        </div>
      </div>
    </div>
  `,
    data() {
        return {
            searchText: '',
            searchId: '',
            suggestions: [],
            isLoading: false,
            showSuggestions: false,
            selectedIndex: -1,
        }
    },
    methods: {
        async onInputChanged() {
            if (this.searchText.length < 2) {
                this.suggestions = []
                return
            }
            this.isLoading = true
            try {
                const response = await fetch(`/api/v1/product/search?query=${this.searchText}`, {
                    credentials: 'same-origin'
                })
                this.suggestions = await response.json()
            } finally {
                this.isLoading = false
            }
        },
        highlight(text) {
            const regex = new RegExp(`(${this.searchText})`, 'gi')
            return text.replace(regex, '<mark>$1</mark>')
        },
        moveSelection(delta) {
            const len = this.suggestions.length
            if (len === 0) return
            this.selectedIndex = (this.selectedIndex + delta + len) % len
        },
        selectSuggestion(index) {
            const item = this.suggestions[index]
            if (!item) return
            this.searchText = item.productTitle
            this.searchId = item.productId
            this.showSuggestions = false
            this.$emit('selected', item)
        },
        handleClickOutside(event) {
            if (!this.$el.contains(event.target)) {
                this.showSuggestions = false
            }
        }
    },
    mounted() {
        document.addEventListener('click', this.handleClickOutside)
    },
    unmounted() {
        document.removeEventListener('click', this.handleClickOutside)
    }
}
