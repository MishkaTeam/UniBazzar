<template>
    <div class="row">
        <div class="col-12 col-lg-9">
            <div id="cart-products">
                <div class="container">
                    <div class="row">
                        <div class="col-12 py-3">
                            <div v-if="basket">
                                <div v-for="item in basket.basketItems" class="row product">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12 col-md-2 pl-0">
                                                <img :src="'/images/products/p100.png'" :alt="item.productName" />
                                                <div class="d-flex flex-column align-items-center mt-2">
                                                    <div class="counter-box">
                                                        <i v-if="item.quantity <= 1"
                                                            @click="removeProduct(item.id)"
                                                            class="bi bi-trash icon-button"></i>
                                                        <i v-if="item.quantity > 1"
                                                            @click="updateQuantity(item.id, --item.quantity)"
                                                            class="bi bi-dash icon-button"></i>

                                                        <div class="d-flex flex-column align-items-center">
                                                            <input type="hidden" name="Quantity"
                                                                :value="item.quantity" />
                                                            <span v-if="loading == true"
                                                                class="spinner-border spinner-border-sm text-danger"
                                                                role="status" aria-hidden="true"></span>
                                                            <div v-if="loading == false" class="counter-number">{{
                                                                item.quantity }}</div>
                                                            <!-- <div class="counter-label">حداکثر</div> -->
                                                        </div>
                                                        <i @click="updateQuantity(item.id, ++item.quantity)"
                                                            class="bi bi-plus icon-button"></i>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-10 col-md-7">
                                                <RouterLink :to="`/Product/${item.productId}`" target="_blank"
                                                    class="title pt-2">
                                                    {{ item.productName }}
                                                </RouterLink>

                                                <div class="pt-1">
                                                    <span class="product-total h5">{{ formatCurrency(item.totalPrice)
                                                        }}</span>
                                                    <span>تومان</span>
                                                </div>

                                                <div class="pt-2">
                                                    <template v-if="!item.attributes?.length">
                                                        <i class="bi bi-bag-plus" />
                                                        <span class="text-black-50">امکانات افزودنی (0 تومان)</span>
                                                    </template>
                                                    <template v-else>
                                                        <div v-for="attr in item.attributes"
                                                            :key="attr.productAttributeId">
                                                            <i class="bi bi-bag-plus"></i>
                                                            <span class="text-black-50">
                                                                {{ attr.productAttributeName }} :
                                                                {{ attr.productAttributeValue }}
                                                                ({{ formatCurrency(attr.priceAdjustment) }})
                                                            </span>
                                                        </div>
                                                    </template>
                                                </div>

                                                <div v-if="item.discountValue > 0" class="pt-2">
                                                    <span class="product-discount">{{ formatCurrency(item.discountValue)
                                                        }}</span>
                                                    <span>تومان تخفیف</span>
                                                </div>

                                                <div class="col-12 pr-0 pt-2">
                                                    <div class="pt-1 pr-2">
                                                        <span class="product-total h3">
                                                            {{ formatCurrency(item.totalPriceWithAdjustment) }}
                                                        </span>
                                                        <span>تومان</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="basket.basketItems && basket.basketItems.length == 0">
                                    سبد خرید شما خالی می باشد
                                </div>
                            </div>
                            <div v-else>
                                <div class="spinner-border text-primary" role="status">
                                    <span class="sr-only">Loading...</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 col-lg-3 mt-2 mt-lg-0 pr-3 pr-lg-0">
            <div id="factor">
                <div class="container">
                    <div class="row py-2">
                        <div class="col-6">
                            <div>جمع کل فاکتور:</div>
                        </div>
                        <div class="col-6">
                            <div><span id="factor-total-price">{{ formatCurrency(basket.totalWithoutDiscount) }}</span>
                                تومان</div>
                        </div>
                    </div>
                    <div class="row py-2 bg-light">
                        <div class="col-6">
                            <div>جمع تخفیف:</div>
                        </div>
                        <div class="col-6">
                            <div><span id="factor-total-discount">{{ formatCurrency(basket.totalItemDiscounts) }}</span>
                                تومان</div>
                        </div>
                    </div>
                    <div class="row py-2" id="total">
                        <div class="col-6">
                            <div>مبلغ قابل پرداخت:</div>
                        </div>
                        <div class="col-6">
                            <div><span id="factor-total">{{ formatCurrency(basket.basketTotal) }}</span> تومان</div>
                        </div>
                    </div>
                    <div class="row py-2">
                        <div class="col-12">
                            <a asp-page="/Checkout"><input type="submit" value="ادامه ثبت سفارش"
                                    class="btn btn-success w-100"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>



<script setup lang="ts">
import { defineProps, ref } from 'vue'
import { Basket } from './CartModel'

const props = defineProps<{
    basketJson: string
}>()

var basket = ref(JSON.parse(props.basketJson) as Basket)

function formatCurrency(val: number): string {
    return new Intl.NumberFormat('fa-IR').format(val)
}

const loading = ref(false)
const postUrl = '/Purchase/Cart?handler='


async function removeProduct(itemId) {
    loading.value = true
    try {
        const res = await fetch(postUrl + "RemoveProduct", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                basketItemId: itemId,
                basketId: basket.value.id
            })
        })

        const response = await res.json()

        if (response && response.isSuccessful) {
            const basketRes = response.data as Basket;
            basket.value = basketRes;
        }

    } catch (err) {
        console.error(err)
    } finally {
        loading.value = false
    }
}


async function updateQuantity(itemId, quantity) {
    loading.value = true
    try {
        const res = await fetch(postUrl + "UpdateQuantity", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                quantity: quantity,
                basketItemId: itemId,
                basketId: basket.value.id
            })
        })

        const response = await res.json()

        if (response && response.isSuccessful) {
            const basketRes = response.data as Basket;
            basket.value = basketRes;
        }

    } catch (err) {
        console.error(err)
    } finally {
        loading.value = false
    }
}

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