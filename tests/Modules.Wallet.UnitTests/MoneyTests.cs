namespace Modules.WalletOps.UnitTests;

using FluentAssertions;
using Modules.WalletOps.Domain.ValueObjects;
using System;
using Xunit;

public class MoneyTests
{
    [Fact]
    public void Constructor_Should_Throw_ArgumentException_When_Amount_Is_Negative()
    {
        Action action = () => Money.Create(-100, "IRR");

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_Should_Create_Instance_For_Valid_Amount_And_Currency()
    {
        var amount = 1000m;
        var currency = "IRR";

        var money = Money.Create(amount, currency);

        money.Amount.Should().Be(amount);
        money.Currency.Should().Be(currency);
    }

    [Fact]
    public void Two_Money_Objects_With_Same_Value_Should_Be_Equal()
    {
        var moneyA = Money.Create(100, "IRR");
        var moneyB = Money.Create(100, "IRR");

        (moneyA == moneyB).Should().BeTrue();
        moneyA.Equals(moneyB).Should().BeTrue();
        (moneyA != moneyB).Should().BeFalse();
    }

    [Fact]
    public void Addition_Should_Return_Correct_Sum_With_Same_Currency()
    {
        var moneyA = Money.Create(100, "IRR");
        var moneyB = Money.Create(50, "IRR");

        var result = moneyA + moneyB;

        result.Should().Be(Money.Create(150, "IRR"));
    }

    [Fact]
    public void Addition_Should_Throw_InvalidOperationException_For_Different_Currencies()
    {
        var moneyA = Money.Create(100, "IRR");
        var moneyB = Money.Create(50, "USD");
        Action action = () => _ = moneyA + moneyB;

        action.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Subtraction_Should_Throw_InvalidOperationException_When_Result_Is_Negative()
    {
        var moneyA = Money.Create(50, "IRR");
        var moneyB = Money.Create(100, "IRR");
        Action action = () => _ = moneyA - moneyB;

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Resulting money amount cannot be negative.");
    }

    [Theory]
    [InlineData(100, 200, true)]
    [InlineData(200, 200, false)]
    [InlineData(300, 200, false)]
    public void LessThan_Operator_Should_Work_Correctly(decimal amount1, decimal amount2, bool expected)
    {
        var moneyA = Money.Create(amount1, "IRR");
        var moneyB = Money.Create(amount2, "IRR");

        var result = moneyA < moneyB;

        result.Should().Be(expected);
    }
}