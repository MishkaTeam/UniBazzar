<script setup lang="ts">
import { computed, defineProps } from 'vue'

/* تعریف تایپ برای props */
interface Attribute {
  id: string
  productAttributeName: string
  productAttributeValue: string
  priceAdjustment: number
}

interface Item {
  id: string
  productName: string
  imageUrl?: string
  quantity: number
  totalPrice: number
  totalPriceWithAdjustment: number
  discountValue: number
  attributes?: Attribute[]
}

/* props ورودی */
const props = defineProps<{
  itemJson: string
  basketId: string
}>()

/* تبدیل JSON به Item با try/catch */
const item = computed<Item>(() => {
  try {
    const parsed = JSON.parse(props.itemJson)
    return parsed as Item
  } catch (error) {
    console.error('❌ Invalid item JSON:', error)
    // بهتره یا throw کنی یا یک default برگردونی
    throw new Error('Failed to parse itemJson')
  }
})

/* محاسبه تصویر و لینک محصول */
const imageSrc = computed(() => item.value.ImageUrl ?? '/images/products/p100.png')
const productLink = computed(() => `/Product/${item.value.Id}`)

/* قالب‌بندی عددی */
function formatCurrency(val: number): string {
  return new Intl.NumberFormat('fa-IR').format(val)
}
</script>

<template>
  <div v-if="item" class="row product">
    <div class="col-12">
      <div class="row">
        <!-- تصویر و کانتر -->
        <div class="col-12 col-md-2 pl-0">
          <img :src="imageSrc" :alt="item.value?.ProductName" />
          <div class="d-flex flex-column align-items-center mt-2">
            <PurchaseCounter
              :initial-value="item.value?.Quantity"
              :basket-item-id="item.value?.Id"
              :basket-id="basketId"
              :max-value="10"
            />
          </div>
        </div>

        <!-- اطلاعات محصول -->
        <div class="col-10 col-md-7">
          <RouterLink :to="productLink" target="_blank" class="title pt-2">
            {{ item.value?.ProductName }}
          </RouterLink>

          <div class="pt-1">
            <span class="product-total h5">{{ formatCurrency(item.value?.TotalPrice) }}</span>
            <span>تومان</span>
          </div>

          <!-- ویژگی‌ها -->
          <div class="pt-2">
            <template v-if="!item.value?.Attributes?.length">
              <i class="bi bi-bag-plus" />
              <span class="text-black-50">امکانات افزودنی (0 تومان)</span>
            </template>
            <template v-else>
              <div v-for="attr in item.value?.Attributes" :key="attr.id">
                <i class="bi bi-bag-plus"></i>
                <span class="text-black-50">
                  {{ attr.ProductAttributeName }} :
                  {{ attr.ProductAttributeValue }}
                  ({{ formatCurrency(attr.PriceAdjustment) }})
                </span>
              </div>
            </template>
          </div>

          <div v-if="item.value?.DiscountValue > 0" class="pt-2">
            <span class="product-discount">{{ formatCurrency(item.value?.DiscountValue) }}</span>
            <span>تومان تخفیف</span>
          </div>

          <div class="col-12 pr-0 pt-2">
            <div class="pt-1 pr-2">
              <span class="product-total h3">
                {{ formatCurrency(item.value?.TotalPriceWithAdjustment) }}
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
