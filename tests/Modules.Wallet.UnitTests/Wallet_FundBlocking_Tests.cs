using FluentAssertions;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.UnitTests;
public class Wallet_FundBlocking_Tests
{
    private readonly Wallet _wallet;

    public Wallet_FundBlocking_Tests()
    {
        _wallet = Wallet.CreateWallet();
        _wallet.DepositWithdrawable(Money.Create(1000, "IRR"), "Salary");
        _wallet.DepositNonWithdrawable(Money.Create(200, "IRR"), "Bonus");
    }

    [Fact]
    public void BlockFunds_Should_Decrease_AvailableBalance_And_Increase_HeldBalance()
    {
        var blockAmount = Money.Create(300, "IRR");

        var heldFund = _wallet.BlockFunds(blockAmount, "Payment hold");

        _wallet.TotalBalance.Should().Be(Money.Create(1200, "IRR")); // Unchanged
        _wallet.HeldBalance.Should().Be(blockAmount);
        _wallet.AvailableBalance.Should().Be(Money.Create(900, "IRR"));
        heldFund.Status.Should().Be(HeldStatusType.Held);
    }

    [Fact]
    public void SettleBlockedFund_Should_Permanently_Decrease_Balances()
    {
        var blockAmount = Money.Create(500, "IRR");
        var heldFund = _wallet.BlockFunds(blockAmount, "Payment hold");

        _wallet.SettleBlockedFund(heldFund.Id);

        _wallet.NonWithdrawableBalance.Should().Be(Money.Zero("IRR"));
        _wallet.WithdrawableBalance.Should().Be(Money.Create(700, "IRR"));
        _wallet.HeldBalance.Should().Be(Money.Zero("IRR"));
        _wallet.TotalBalance.Should().Be(Money.Create(700, "IRR"));
        _wallet.AvailableBalance.Should().Be(Money.Create(700, "IRR"));
        heldFund.Status.Should().Be(HeldStatusType.Finalized);
    }

    [Fact]
    public void ReleaseBlockedFund_Should_Return_Funds_To_AvailableBalance()
    {
        var blockAmount = Money.Create(400, "IRR");
        var heldFund = _wallet.BlockFunds(blockAmount, "Payment hold");

        _wallet.ReleaseBlockedFund(heldFund.Id);

        _wallet.HeldBalance.Should().Be(Money.Zero("IRR"));
        _wallet.AvailableBalance.Should().Be(Money.Create(1200, "IRR"));
        _wallet.TotalBalance.Should().Be(Money.Create(1200, "IRR")); // Unchanged
        heldFund.Status.Should().Be(HeldStatusType.Released);
    }
}