using FluentAssertions;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.Exceptions;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.UnitTests;

using FluentAssertions;
using System;
using System.Linq;
using Xunit;

public class WalletTests
{
    [Fact]
    public void CreateWallet_Should_Initialize_Wallet_With_Zero_Balances()
    {
        var wallet = Wallet.CreateWallet();

        wallet.WithdrawableBalance.Should().Be(Money.Zero("IRR"));
        wallet.NonWithdrawableBalance.Should().Be(Money.Zero("IRR"));
        wallet.TotalBalance.Should().Be(Money.Zero("IRR"));
        wallet.AvailableBalance.Should().Be(Money.Zero("IRR"));
        wallet.Status.Should().Be(WalletStatusType.Active);
    }

    [Fact]
    public void DepositWithdrawable_Should_Increase_WithdrawableBalance()
    {
        var wallet = Wallet.CreateWallet();
        var depositAmount = Money.Create(1000, "IRR");

        wallet.DepositWithdrawable(depositAmount, "Test deposit", null);

        wallet.WithdrawableBalance.Should().Be(depositAmount);
        wallet.TotalBalance.Should().Be(depositAmount);
        wallet.Transactions.Should().HaveCount(1);
        wallet.Transactions.First().Type.Should().Be(TransactionType.Withdrawable_Deposit);
    }

    [Fact]
    public void DepositNonWithdrawable_Should_Increase_NonWithdrawableBalance()
    {
        var wallet = Wallet.CreateWallet();
        var depositAmount = Money.Create(500, "IRR");

        wallet.DepositNonWithdrawable(depositAmount, "Gift", null);

        wallet.NonWithdrawableBalance.Should().Be(depositAmount);
        wallet.TotalBalance.Should().Be(depositAmount);
    }

    [Fact]
    public void Withdraw_Should_Decrease_WithdrawableBalance()
    {
        var wallet = Wallet.CreateWallet();
        wallet.DepositWithdrawable(Money.Create(1000, "IRR"), "Initial deposit", null);
        var withdrawAmount = Money.Create(700, "IRR");

        wallet.Withdraw(withdrawAmount, null);

        wallet.WithdrawableBalance.Should().Be(Money.Create(300, "IRR"));
        wallet.Transactions.Last().Type.Should().Be(TransactionType.Withdrawal);
    }


    [Fact]
    public void Withdraw_Should_ThrowError_For_NonWithdrawableBalance()
    {
        var wallet = Wallet.CreateWallet();
        var depositAmount = Money.Create(1000, "IRR");
        wallet.DepositNonWithdrawable(depositAmount, "Gift", null);

        var withdrawAmount = Money.Create(700, "IRR");
        Action withdraw = () => wallet.Withdraw(withdrawAmount, null);

        withdraw.Should().Throw<InvalidBalanceException>();
    }

    [Fact]
    public void Withdraw_Should_Throw_Exception_When_Funds_Are_Insufficient()
    {
        var wallet = Wallet.CreateWallet();
        wallet.DepositWithdrawable(Money.Create(500, "IRR"), "Initial deposit", null);
        Action action = () => wallet.Withdraw(Money.Create(600, "IRR"), null);

        action.Should().Throw<Exception>().WithMessage("Insufficient withdrawable funds.");
    }

    [Fact]
    public void Performing_Operation_On_Frozen_Wallet_Should_Throw_Exception()
    {
        var wallet = Wallet.CreateWallet();
        wallet.Freeze();
        Action action = () => wallet.DepositWithdrawable(Money.Create(100, "IRR"), "deposit", null);

        action.Should().Throw<Exception>().WithMessage("Wallet is not active.");
    }
}