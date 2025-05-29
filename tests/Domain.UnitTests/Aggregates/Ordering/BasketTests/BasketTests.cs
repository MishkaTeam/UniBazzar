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
        var ownerId = Guid.Parse("bc7b0185-9b1e-4c39-a952-24e1cb3dcb28");

        var basket = Basket.Initialize(platform, ownerId);

        basket.Should().NotBeNull();
        basket.PlatForm.Should().Be(platform);
        basket.BasketStatus.Should().Be(BasketStatus.INITIAL);
        basket.BasketItems.Should().BeEmpty();
        basket.OwnerId.Should().Be(ownerId);
    }
    
    [Fact]
    public void AddItem_ShouldAddBasketItemToBasket()
    {
        var ownerId = Guid.Parse("bc7b0185-9b1e-4c39-a952-24e1cb3dcb28");

        var basket = Basket.Initialize(Platform.POS, ownerId);
        var product = ProductType.Create(Guid.NewGuid(), "Test Product");
        var basketItem = BasketItem.Create(Guid.NewGuid(), "123456", product, ProductAmount.Create(1, 100), DiscountAmount.Create(10, DiscountType.Price));
        
        basket.AddItem(basketItem);
        
        basket.BasketItems.Should().ContainSingle();
        basket.BasketItems.Should().Contain(basketItem);
    }
    
    [Fact]
    public void Checkout_ShouldChangeBasketStatusToCheckout()
    {
        var ownerId = Guid.Parse("bc7b0185-9b1e-4c39-a952-24e1cb3dcb28");

        var basket = Basket.Initialize(Platform.POS, ownerId);
        
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
