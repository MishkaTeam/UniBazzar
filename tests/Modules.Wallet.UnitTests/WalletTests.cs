using FluentAssertions;
using Modules.WalletOps.Domain.Aggregates.WalletTrx;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.Exceptions;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.UnitTests;


public class WalletTests
{
    private const string CurrencyIRR = "IRR";

    [Fact]
    public void CreateWallet_Should_CreateWallet_WithCorrectInitialState()
    {
        var userId = Guid.NewGuid();
        var initialBalance = Money.Create(10000, CurrencyIRR);

        var wallet = Wallet.CreateWallet();
        wallet.WithdrawableDeposit(initialBalance);
        wallet.SetOwner(userId);

        wallet.Should().NotBeNull();
        wallet.OwnerId.Should().Be(userId);
        wallet.Balance.Should().Be(initialBalance);
        wallet.Status.Should().Be(WalletStatusType.Active);
    }

    [Fact]
    public void Deposit_ToActiveWallet_ShouldIncreaseBalance_And_AddTransaction()
    {
        var wallet = Wallet.CreateWallet();
        var depositAmount = Money.Create(50000, CurrencyIRR);

        wallet.WithdrawableDeposit(depositAmount);

        wallet.Balance.Amount.Should().Be(50000);
        wallet.Transactions.Should().ContainSingle(t => t.Type == TransactionType.Withdrawable_Deposit && t.Amount == depositAmount);
    }

    [Fact]
    public void Deposit_ToFrozenWallet_ShouldThrowDomainException()
    {
        var wallet = Wallet.CreateWallet();
        wallet.Freeze();

        Action act = () => wallet.WithdrawableDeposit(Money.Create(1000, CurrencyIRR));

        act.Should().Throw<WalletDeactivateException>();
    }

    [Fact]
    public void Withdraw_WithSufficientFunds_ShouldDecreaseBalance_And_AddTransaction()
    {
        var depositAmount = Money.Create(100000, CurrencyIRR);
        var withdrawAmount = Money.Create(40000, CurrencyIRR);

        var wallet = Wallet.CreateWallet();
        wallet.WithdrawableDeposit(depositAmount);

        wallet.Withdraw(withdrawAmount);

        wallet.Balance.Amount.Should().Be(60000);
        wallet.Transactions.Should().Contain(t => t.Type == TransactionType.Withdrawable_Deposit && t.Amount == depositAmount);
        wallet.Transactions.Should().Contain(t => t.Type == TransactionType.Withdrawal && t.Amount == withdrawAmount);
    }

    [Fact]
    public void Withdraw_WithInsufficientFunds_ShouldThrowDomainException()
    {
        var withdrawAmount = Money.Create(30000, CurrencyIRR);
        var depositAmount = Money.Create(10000, CurrencyIRR);

        var wallet = Wallet.CreateWallet();
        wallet.WithdrawableDeposit(depositAmount);

        Action act = () => wallet.Withdraw(withdrawAmount);

        act.Should().Throw<InvalidBalanceException>();
        wallet.Balance.Amount.Should().Be(depositAmount.Amount); 
    }

    [Fact]
    public void FreezeWallet_ShouldChangeStatusToFrozen()
    {
        var wallet = Wallet.CreateWallet();

        wallet.Freeze();

        wallet.Status.Should().Be(WalletStatusType.Frozen);
    }
}