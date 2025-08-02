export interface Basket {
  id: string
  ownerId: string
  referenceNumber: string
  platform: number
  basketStatus: number
  description: any
  totalDiscountAmount: number
  totalDiscountType: number
  totalWithoutDiscount: number
  subtotalBeforeBasketDiscount: number
  basketTotal: number
  basketItems: BasketItem[]
  totalItemDiscounts: number
}

export interface BasketItem {
  id: string
  productName: string
  productId: string
  quantity: number
  basePrice: number
  discountValue: number
  discountType: number
  totalPrice: number
  attributes: Attribute[]
  priceAdjustments: number
  totalPriceWithAdjustment: number
}

export interface Attribute {
  productAttributeId: string
  productAttributeName: string
  productAttributeValueId: string
  productAttributeValue: string
  priceAdjustment: number
}
