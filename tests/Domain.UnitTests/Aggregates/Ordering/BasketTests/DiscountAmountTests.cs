using Domain.Aggregates.Ordering.Baskets.Enums;
using Domain.Aggregates.Ordering.Baskets.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Ordering.BasketTests;

public class DiscountAmountTests
{
    [Fact]
    public void CreatePriceDiscount_ValidAmount_CreatesDiscount()
    {
        
        decimal amount = 10.50m;
        
        var discount = DiscountAmount.CreatePriceDiscount(amount);
        
        discount.Value.Should().Be(amount);
        discount.DiscountType.Should().Be(DiscountType.Price);
    }

    [Fact]
    public void CreatePercentDiscount_ValidPercentage_CreatesDiscount()
    {
        
        decimal percentage = 25m;
        
        var discount = DiscountAmount.CreatePercentDiscount(percentage);
        
        discount.Value.Should().Be(percentage);
        discount.DiscountType.Should().Be(DiscountType.Percent);
    }

    [Fact]
    public void Constructor_NegativeValue_ThrowsArgumentException()
    {
        Action act = () => DiscountAmount.CreatePriceDiscount(-5m);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_PercentOver100_ThrowsArgumentException()
    {
        Action act = () => DiscountAmount.CreatePercentDiscount(150m);
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(100, 20, 80)]  // $20 off $100
    [InlineData(50, 10, 40)]   // $10 off $50
    [InlineData(10, 15, 0)]    // $15 off $10 (minimum 0)
    public void ApplyDiscount_PriceDiscount_ReturnsCorrectValue(
        decimal originalPrice, 
        decimal discountAmount, 
        decimal expected)
    {
        
        var discount = DiscountAmount.CreatePriceDiscount(discountAmount);
        
        var result = discount.ApplyDiscount(originalPrice);
        
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(100, 25, 75)]    // 25% off $100
    [InlineData(200, 50, 100)]   // 50% off $200
    [InlineData(50, 100, 0)]     // 100% off $50
    public void ApplyDiscount_PercentDiscount_ReturnsCorrectValue(
        decimal originalPrice, 
        decimal percentage, 
        decimal expected)
    {
        var discount = DiscountAmount.CreatePercentDiscount(percentage);
        
        var result = discount.ApplyDiscount(originalPrice);
        
        result.Should().Be(expected);
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        
        var discount1 = DiscountAmount.CreatePriceDiscount(10m);
        var discount2 = DiscountAmount.CreatePriceDiscount(10m);
        
        discount1.Equals(discount2).Should().BeTrue();
        discount1.Should().Be(discount2);
    }

    [Fact]
    public void Equals_DifferentValues_ReturnsFalse()
    {
        var discount1 = DiscountAmount.CreatePriceDiscount(10m);
        var discount2 = DiscountAmount.CreatePriceDiscount(20m);
        
        discount1.Equals(discount2).Should().BeFalse();
        discount1.Should().NotBe(discount2);
    }

    [Fact]
    public void Equals_DifferentTypes_ReturnsFalse()
    {
        var discount1 = DiscountAmount.CreatePriceDiscount(10m);
        var discount2 = DiscountAmount.CreatePercentDiscount(10m);
        
        discount1.Equals(discount2).Should().BeFalse();
        discount1.Should().NotBe(discount2);
    }

    [Fact]
    public void ToString_PriceDiscount_ReturnsFormattedString()
    {
        
        var discount = DiscountAmount.CreatePriceDiscount(10.50m);
        
        var result = discount.ToString();
        
        result.Should().Be("$10.50 Discount"); // Note: Currency symbol might vary by culture
    }

    [Fact]
    public void ToString_PercentDiscount_ReturnsFormattedString()
    {
        var discount = DiscountAmount.CreatePercentDiscount(25m);
        
        var result = discount.ToString();
        
        result.Should().Be("25% Discount");
    }

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHash()
    {
        var discount1 = DiscountAmount.CreatePriceDiscount(10m);
        var discount2 = DiscountAmount.CreatePriceDiscount(10m);
        
        discount1.GetHashCode().Should().Be(discount2.GetHashCode());
    }
}