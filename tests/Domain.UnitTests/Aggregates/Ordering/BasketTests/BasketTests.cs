using Domain.Aggregates.Ordering.Baskets;
using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Ordering.BasketTests;

public class BasketTests
{
    [Fact]
    public void Initialize_ShouldCreateBasketWithCorrectPlatformAndInitialStatus()
    {
        var platform = Platform.POS;

        var basket = Basket.Initialize(platform);

        basket.Should().NotBeNull();
        basket.Platform.Should().Be(platform);
        basket.BasketStatus.Should().Be(BasketStatus.INITIAL);
        basket.BasketItems.Should().BeEmpty();
    }

    [Theory]
    [InlineData(1, 100, DiscountType.Price, 10, 90)]     // تخفیف مبلغی 10 تومانی
    [InlineData(2, 200, DiscountType.Percent, 25, 300)]  // 25% تخفیف روی 400 = 100 تخفیف، مبلغ نهایی: 300
    [InlineData(3, 150, DiscountType.None, 0, 450)]      // بدون تخفیف
    public void AddItem_ShouldCalculateCorrectLineTotal(
        int quantity,
        decimal unitPrice,
        DiscountType discountType,
        decimal discountValue,
        decimal expectedLineTotal)
    {
        var ownerId = Guid.Parse("bc7b0185-9b1e-4c39-a952-24e1cb3dcb28");

        var basket = Basket.Initialize(Platform.POS);
        var product = ProductType.Create(Guid.NewGuid(), "Test Product");

        var basketItem = BasketItem.Create(
            Guid.NewGuid(),
            "123456",
            product,
            ProductAmount.Create(quantity, unitPrice),
            DiscountAmount.Create(discountValue, discountType)
        );

        basket.AddItem(basketItem);

        basket.BasketItems.Should().ContainSingle();

        var item = basket.BasketItems.First();
        item.TotalPrice.Should().Be(expectedLineTotal);
    }


    [Fact]
    public void Checkout_ShouldChangeBasketStatusToCheckout()
    {
        var ownerId = Guid.Parse("bc7b0185-9b1e-4c39-a952-24e1cb3dcb28");

        var basket = Basket.Initialize(Platform.POS);
        
        basket.Checkout();
        
        basket.BasketStatus.Should().Be(BasketStatus.CHECKOUT);
    }
}

public class BasketItemTests
{
    [Fact]
    public void Create_ShouldInitializeBasketItemCorrectly()
    {
        var basketId = Guid.NewGuid();
        string referenceNumber = "123456";
        var product = ProductType.Create(Guid.NewGuid(), "Sample Product");
        var productAmount = ProductAmount.Create(2, 50);
        var discountAmount = DiscountAmount.Create(5, DiscountType.Percent);

        var basketItem = BasketItem.Create(basketId, referenceNumber, product, productAmount, discountAmount);
        
        basketItem.Should().NotBeNull();
        basketItem.BasketId.Should().Be(basketId);
        basketItem.BasketReferenceNumber.Should().Be(referenceNumber);
        basketItem.Product.Should().Be(product);
        basketItem.ProductAmount.Should().Be(productAmount);
        basketItem.DiscountAmount.Should().Be(discountAmount);
    }
}
