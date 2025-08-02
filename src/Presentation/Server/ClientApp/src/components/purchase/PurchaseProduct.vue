
<template>
    <div v-if="item" class="row product">
        <div class="col-12">
            <div class="row">
                <div class="col-12 col-md-2 pl-0">
                    <img :src="imageSrc" :alt="productName" />
                    <div class="d-flex flex-column align-items-center mt-2">
                        <div class="counter-box">
                            <i v-if="quantityRef <= 1" class="bi bi-trash icon-button"></i>
                            <i v-if="quantityRef > 1" @click="quantityRef--" class="bi bi-dash icon-button"></i>

                            <div class="d-flex flex-column align-items-center">
                                <input type="hidden" name="Quantity" :value="quantityRef" />
                                <span v-if="loading == true" class="spinner-border spinner-border-sm text-danger" role="status"
                                      aria-hidden="true"></span>
                                <div v-if="loading == false" class="counter-number">{{ quantityRef }}</div>
                                <!-- <div class="counter-label">حداکثر</div> -->
                            </div>
                            <i @click="quantityRef++" class="bi bi-plus icon-button"></i>
                        </div>
                    </div>
                </div>

                <div class="col-10 col-md-7">
                    <RouterLink :to="productLink" target="_blank" class="title pt-2">
                        {{ productName }}
                    </RouterLink>

                    <div class="pt-1">
                        <span class="product-total h5">{{ formatCurrency(totalPrice) }}</span>
                        <span>تومان</span>
                    </div>

                    <div class="pt-2">
                        <template v-if="!attributes?.length">
                            <i class="bi bi-bag-plus" />
                            <span class="text-black-50">امکانات افزودنی (0 تومان)</span>
                        </template>
                        <template v-else>
                            <div v-for="attr in attributes" :key="attr.Id">
                                <i class="bi bi-bag-plus"></i>
                                <span class="text-black-50">
                                    {{ attr.ProductAttributeName }} :
                                    {{ attr.ProductAttributeValue }}
                                    ({{ formatCurrency(attr.PriceAdjustment) }})
                                </span>
                            </div>
                        </template>
                    </div>

                    <div v-if="discountValue > 0" class="pt-2">
                        <span class="product-discount">{{ formatCurrency(discountValue) }}</span>
                        <span>تومان تخفیف</span>
                    </div>

                    <div class="col-12 pr-0 pt-2">
                        <div class="pt-1 pr-2">
                            <span class="product-total h3">
                                {{ formatCurrency(totalPriceWithAdjustment) }}
                            </span>
                            <span>تومان</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div v-else>
        <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>

</template>



<script setup lang="ts">
    import { computed, defineProps } from 'vue'
    import { ref, watch } from 'vue'

    interface Attribute {
        Id: string
        ProductAttributeName: string
        ProductAttributeValue: string
        PriceAdjustment: number
    }

    interface Item {
        Id: string
        ProductName: string
        ImageUrl?: string
        Quantity: number
        TotalPrice: number
        TotalPriceWithAdjustment: number
        DiscountValue: number
        Attributes?: Attribute[]
    }

    const props = defineProps<{
        itemJson: string
        basketId: string
    }>()

    var item = ref(JSON.parse(props.itemJson) as Item)

    const imageSrc = computed(() => item.value.ImageUrl ?? '/images/products/p100.png')
    const productLink = computed(() => `/Product/${item.value.Id}`)
    const productName = item.value.ProductName
    const quantity = item.value.Quantity
    const totalPrice = item.value.TotalPrice
    const totalPriceWithAdjustment = item.value.TotalPriceWithAdjustment
    const discountValue = item.value.DiscountValue
    const attributes = item.value.Attributes
    const id = item.value.Id

    function formatCurrency(val: number): string {
        return new Intl.NumberFormat('fa-IR').format(val)
    }

    const loading = ref(false)
    const quantityRef = ref(quantity)
    const postUrl = '/Purchase/Cart?handler=UpdateQuantity'


    async function updateQuantity(newVal) {
        loading.value = true
        try {
            const res = await fetch(postUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    quantity: newVal,
                    basketItemId: id,
                    basketId: props.basketId
                })
            })

            const response = await res.json()
            
            if(response && response.isSuccessful)
            {   
                const itmes = response.data?.basketItems as Item[];
                const baksetItem = itmes?.filter(item => item.Id == id);
                item.value = baksetItem[0];
            }

        } catch (err) {
            console.error(err)
        } finally {
            loading.value = false
        }
    }


    watch(quantityRef, (newVal, oldVal) => {
        if (newVal !== oldVal) updateQuantity(newVal)
    })
</script>

<style scoped>
.counter-box {
    border: 1px solid #dee2e6;
    border-radius: 12px;
    padding: 0.5rem 1rem;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 160px;
}

.icon-button {
    cursor: pointer;
    color: #f44336;
    font-size: 1.25rem;
}

.icon-button.disabled {
    color: #ccc;
    cursor: not-allowed;
}

.counter-number {
    color: #f44336;
    font-weight: bold;
    font-size: 1.25rem;
}

.counter-label {
    font-size: 0.8rem;
    color: #999;
    text-align: center;
}
</style>