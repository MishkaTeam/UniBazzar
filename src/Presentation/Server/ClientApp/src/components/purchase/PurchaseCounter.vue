<template>
    <div class="counter-box">
        <i v-if="quantity <= 1" class="bi bi-trash icon-button"></i>
        <i v-if="quantity > 1" @click="quantity--" class="bi bi-dash icon-button"></i>

        <div class="d-flex flex-column align-items-center">
            <input type="hidden" name="Quantity" :value="quantity" />
            <div class="counter-number">{{ quantity }}</div>
            <!-- <div class="counter-label">حداکثر</div> -->
        </div>
        <i @click="quantity++" class="bi bi-plus icon-button"></i>
    </div>
</template>

<script setup>
import { ref, watch } from 'vue'

const props = defineProps({
    initialValue: {
        type: Number,
        default: 1
    },
    basketItemId: {
        type: String,
        default: ''
    },
    basketId: {
        type: String,
        default: ''
    },
    maxValue: {
        type: Number,
        default: 0
    },
    isDisabled: {
        type: Boolean,
        default: false
    }
})

const quantity = ref(props.initialValue)
const basketId = ref(props.basketId)
const basketItemId = ref(props.basketItemId)

const postUrl = '/Purchase/Cart?handler=UpdateQuantity'

watch(quantity, async (newVal) => {
    await fetch(postUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ quantity: newVal, basketItemId: basketItemId.value, basketId: basketId.value })
    }).then(res => {
        var qun = quantity;
        res.json().then(x => qun = x.quantity)
        quantity.value = qun
        // quantity = ref(res.json)
    })
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