<template>
    <div class="counter-box">
        <i v-if="quantity <= 1" class="bi bi-trash icon-button"></i>
        <i v-if="quantity > 1" @click="quantity--" class="bi bi-dash icon-button"></i>

        <div class="d-flex flex-column align-items-center">
            <input type="hidden" name="Quantity" :value="quantity" />
            <span v-if="loading == true" class="spinner-border spinner-border-sm text-danger" role="status"
                aria-hidden="true"></span>
            <div v-if="loading == false" class="counter-number">{{ quantity }}</div>
            <!-- <div class="counter-label">حداکثر</div> -->
        </div>
        <i @click="quantity++" class="bi bi-plus icon-button"></i>
    </div>
</template>

<script setup>
import { ref, watch } from 'vue'

const props = defineProps({
    initialValue: Number,
    basketItemId: String,
    basketId: String,
    maxValue: Number,
    isDisabled: Boolean
})

const loading = ref(false)
const quantity = ref(props.initialValue)
const postUrl = '/Purchase/Cart?handler=UpdateQuantity'


async function updateQuantity(newVal) {
    loading.value = true
    try {
        const res = await fetch(postUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                quantity: newVal,
                basketItemId: props.basketItemId,
                basketId: props.basketId
            })
        })

        const data = await res.json()

        if (data.quantity !== quantity.value) quantity.value = data.quantity
    } catch (err) {
        console.error(err)
    } finally {
        loading.value = false
    }
}


watch(quantity, (newVal, oldVal) => {
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