using Domain.Aggregates.Ordering.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Aggregates.Ordering.BasketTests;

public class ProductAmountTests
{
    [Fact]
    public void Create_ValidValues_CreatesProductAmount()
    {
        
        long quantity = 5;
        decimal basePrice = 10.99m;

        
        var productAmount = ProductAmount.Create(quantity, basePrice);

        
        productAmount.Quantity.Should().Be(quantity);
        productAmount.BasePrice.Should().Be(basePrice);
        productAmount.TotalPrice.Should().Be(quantity * basePrice);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_InvalidQuantity_ThrowsArgumentException(long invalidQuantity)
    {
         
        Action act = () => ProductAmount.Create(invalidQuantity, 10m);

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(-10)]
    public void Create_NegativeBasePrice_ThrowsArgumentException(decimal negativePrice)
    {
         
        Action act = () => ProductAmount.Create(1, negativePrice);

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(1, 10.50, 10.50)]
    [InlineData(2, 5.25, 10.50)]
    [InlineData(10, 1.99, 19.90)]
    public void TotalPrice_ReturnsCorrectCalculation(
        long quantity, 
        decimal basePrice, 
        decimal expectedTotal)
    {
        
        var productAmount = ProductAmount.Create(quantity, basePrice);

         
        productAmount.TotalPrice.Should().Be(expectedTotal);
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        
        var amount1 = ProductAmount.Create(3, 15.99m);
        var amount2 = ProductAmount.Create(3, 15.99m);

         
        amount1.Equals(amount2).Should().BeTrue();
        amount1.Should().Be(amount2);
    }

    [Fact]
    public void Equals_DifferentQuantities_ReturnsFalse()
    {
        
        var amount1 = ProductAmount.Create(3, 15.99m);
        var amount2 = ProductAmount.Create(4, 15.99m);

         
        amount1.Equals(amount2).Should().BeFalse();
        amount1.Should().NotBe(amount2);
    }

    [Fact]
    public void Equals_DifferentBasePrices_ReturnsFalse()
    {
        
        var amount1 = ProductAmount.Create(3, 15.99m);
        var amount2 = ProductAmount.Create(3, 16.99m);

         
        amount1.Equals(amount2).Should().BeFalse();
        amount1.Should().NotBe(amount2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        
        var amount = ProductAmount.Create(3, 15.99m);

         
        amount.Equals(null).Should().BeFalse();
    }
    
    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHash()
    {
        
        var amount1 = ProductAmount.Create(3, 15.99m);
        var amount2 = ProductAmount.Create(3, 15.99m);

         
        amount1.GetHashCode().Should().Be(amount2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHash()
    {
        
        var amount1 = ProductAmount.Create(3, 15.99m);
        var amount2 = ProductAmount.Create(4, 15.99m);

         
        amount1.GetHashCode().Should().NotBe(amount2.GetHashCode());
    }
}