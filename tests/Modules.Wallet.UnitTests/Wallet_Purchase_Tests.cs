using FluentAssertions;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.UnitTests;

public class Wallet_Purchase_Tests
{
    [Fact]
    public void Purchase_Should_Prioritize_NonWithdrawableBalance()
    {
        var wallet = Wallet.CreateWallet();
        wallet.DepositWithdrawable(Money.Create(1000, "IRR"), "Salary");
        wallet.DepositNonWithdrawable(Money.Create(200, "IRR"), "Bonus");
        var purchaseAmount = Money.Create(150, "IRR");

        wallet.Purchase(purchaseAmount);

        wallet.NonWithdrawableBalance.Should().Be(Money.Create(50, "IRR"));
        wallet.WithdrawableBalance.Should().Be(Money.Create(1000, "IRR")); // Should be untouched
    }

    [Fact]
    public void Purchase_Should_Use_Both_Balances_When_NonWithdrawable_Is_Insufficient()
    {
        var wallet = Wallet.CreateWallet();
        wallet.DepositWithdrawable(Money.Create(1000, "IRR"), "Salary");
        wallet.DepositNonWithdrawable(Money.Create(200, "IRR"), "Bonus");
        var purchaseAmount = Money.Create(500, "IRR"); // 200 from non-withdrawable, 300 from withdrawable

        wallet.Purchase(purchaseAmount);

        wallet.NonWithdrawableBalance.Should().Be(Money.Zero("IRR"));
        wallet.WithdrawableBalance.Should().Be(Money.Create(700, "IRR"));
        wallet.TotalBalance.Should().Be(Money.Create(700, "IRR"));
    }

    [Fact]
    public void Purchase_Should_Throw_Exception_If_AvailableBalance_Is_Insufficient()
    {
        var wallet = Wallet.CreateWallet();
        wallet.DepositWithdrawable(Money.Create(1000, "IRR"), "Salary");
        Action action = () => wallet.Purchase(Money.Create(1100, "IRR"));

        action.Should().Throw<Exception>().WithMessage("Insufficient available funds.");
    }
}