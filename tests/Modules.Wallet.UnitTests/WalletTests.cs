using FluentAssertions;
using Modules.Wallet.Domain.Aggregates;
using Modules.Wallet.Domain.ValueObjects;

namespace Modules.Wallet.UnitTests;


//public class WalletTests
//{
//    private const string CurrencyIRR = "IRR";

//    [Fact]
//    public void CreatePersonalWallet_Should_CreateWallet_WithCorrectInitialState_And_RaiseEvent()
//    {
//        // Arrange
//        var userId = Guid.NewGuid();
//        var initialBalance = Money.Create(10000, CurrencyIRR);

//        // Act
//        var wallet = Wallet..CreatePersonalWallet(userId, initialBalance);

//        // Assert
//        wallet.Should().NotBeNull();
//        wallet.OwnerId.Should().Be(userId);
//        wallet.OwnerType.Should().Be(WalletOwnerType.Personal);
//        wallet.Balance.Should().Be(initialBalance);
//        wallet.Status.Should().Be(WalletStatus.Active);
//        wallet.GetDomainEvents().Should().ContainSingle(e => e is WalletCreated);
//    }

//    [Fact]
//    public void Deposit_ToActiveWallet_ShouldIncreaseBalance_And_AddTransaction_And_RaiseEvent()
//    {
//        // Arrange
//        var wallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Zero(CurrencyIRR));
//        var depositAmount = Money.Create(50000, CurrencyIRR);

//        // Act
//        wallet.Deposit(depositAmount);

//        // Assert
//        wallet.Balance.Amount.Should().Be(50000);
//        wallet.Transactions.Should().ContainSingle(t => t.Type == TransactionType.Deposit && t.Amount == depositAmount);
//        wallet.GetDomainEvents().Should().Contain(e => e is WalletCharged charge && charge.Amount == depositAmount);
//    }

//    [Fact]
//    public void Deposit_ToFrozenWallet_ShouldThrowDomainException()
//    {
//        // Arrange
//        var wallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Zero(CurrencyIRR));
//        wallet.Freeze();

//        // Act
//        Action act = () => wallet.Deposit(Money.Create(1000, CurrencyIRR));

//        // Assert
//        act.Should().Throw<DomainException>().WithMessage("Operation cannot be performed on a non-active wallet.");
//    }

//    [Fact]
//    public void Withdraw_WithSufficientFunds_ShouldDecreaseBalance_And_AddTransaction_And_RaiseEvent()
//    {
//        // Arrange
//        var wallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Create(100000, CurrencyIRR));
//        var withdrawAmount = Money.Create(40000, CurrencyIRR);

//        // Act
//        wallet.Withdraw(withdrawAmount);

//        // Assert
//        wallet.Balance.Amount.Should().Be(60000);
//        wallet.Transactions.Should().ContainSingle(t => t.Type == TransactionType.Withdrawal && t.Amount == withdrawAmount);
//        wallet.GetDomainEvents().Should().Contain(e => e is WalletWithdrawn withdrawn && withdrawn.Amount == withdrawAmount);
//    }

//    [Fact]
//    public void Withdraw_WithInsufficientFunds_ShouldThrowDomainException_And_RaiseEvent()
//    {
//        // Arrange
//        var wallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Create(20000, CurrencyIRR));
//        var withdrawAmount = Money.Create(30000, CurrencyIRR);

//        // Act
//        Action act = () => wallet.Withdraw(withdrawAmount);

//        // Assert
//        act.Should().Throw<DomainException>().WithMessage("Insufficient funds for this operation.");
//        wallet.Balance.Amount.Should().Be(20000); // Balance should not change
//        wallet.GetDomainEvents().Should().ContainSingle(e => e is InsufficientFundsAttempted);
//    }

//    [Fact]
//    public void FreezeWallet_ShouldChangeStatusToFrozen_And_RaiseEvent()
//    {
//        // Arrange
//        var wallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Zero(CurrencyIRR));

//        // Act
//        wallet.Freeze();

//        // Assert
//        wallet.Status.Should().Be(WalletStatus.Frozen);
//        wallet.GetDomainEvents().Should().Contain(e => e is WalletStatusChanged statusChange && statusChange.NewStatus == WalletStatus.Frozen);
//    }
//}


//// WalletService.Domain.Tests/TransferServiceTests.cs
//public class TransferServiceTests
//{
//    private const string CurrencyIRR = "IRR";
//    private readonly TransferService _transferService;

//    public TransferServiceTests()
//    {
//        _transferService = new TransferService();
//    }

//    [Fact]
//    public void InitiateTransfer_WithSufficientFunds_ShouldSucceed_And_HoldFundsInSourceWallet()
//    {
//        // Arrange
//        var sourceWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Create(100000, CurrencyIRR));
//        var destinationWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Zero(CurrencyIRR));
//        var transferAmount = Money.Create(70000, CurrencyIRR);

//        // Act
//        var transfer = _transferService.InitiateTransfer(sourceWallet, destinationWallet, transferAmount);

//        // Assert
//        transfer.Should().NotBeNull();
//        transfer.Status.Should().Be(TransferStatus.Pending);
//        sourceWallet.Balance.Amount.Should().Be(30000); // 100k - 70k
//        sourceWallet.Transactions.Should().ContainSingle(t => t.Type == TransactionType.TransferOut);
//        sourceWallet.GetDomainEvents().Should().Contain(e => e is FundsHeldForTransfer);
//        destinationWallet.Balance.Amount.Should().Be(0); // Destination balance unchanged yet
//    }

//    [Fact]
//    public void InitiateTransfer_WithInsufficientFunds_ShouldThrowException_And_FailTransfer()
//    {
//        // Arrange
//        var sourceWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Create(50000, CurrencyIRR));
//        var destinationWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Zero(CurrencyIRR));
//        var transferAmount = Money.Create(70000, CurrencyIRR);

//        // Act
//        Action act = () => _transferService.InitiateTransfer(sourceWallet, destinationWallet, transferAmount);

//        // Assert
//        act.Should().Throw<DomainException>().WithMessage("Insufficient funds for this operation.");
//        sourceWallet.Balance.Amount.Should().Be(50000); // Balance should not have changed
//    }

//    [Fact]
//    public void FinalizeTransfer_ForPendingTransfer_ShouldCompleteTransfer_And_CreditDestinationWallet()
//    {
//        // Arrange
//        var sourceWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Create(100000, CurrencyIRR));
//        var destinationWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Zero(CurrencyIRR));
//        var transferAmount = Money.Create(70000, CurrencyIRR);
//        var transfer = _transferService.InitiateTransfer(sourceWallet, destinationWallet, transferAmount); // state is now pending, source balance is 30k

//        // Act
//        _transferService.FinalizeTransfer(transfer, sourceWallet, destinationWallet);

//        // Assert
//        transfer.Status.Should().Be(TransferStatus.Completed);
//        destinationWallet.Balance.Amount.Should().Be(70000);
//        destinationWallet.Transactions.Should().ContainSingle(t => t.Type == TransactionType.TransferIn);
//        destinationWallet.GetDomainEvents().Should().Contain(e => e is FundsReceivedFromTransfer);
//        transfer.GetDomainEvents().Should().Contain(e => e is TransferCompleted);
//    }

//    [Fact]
//    public void FinalizeTransfer_WhenDestinationIsFrozen_ShouldFailTransfer_And_RevertFundsToSource()
//    {
//        // Arrange
//        var sourceWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Create(100000, CurrencyIRR));
//        var destinationWallet = Wallet.CreatePersonalWallet(Guid.NewGuid(), Money.Zero(CurrencyIRR));
//        var transferAmount = Money.Create(70000, CurrencyIRR);
//        var transfer = _transferService.InitiateTransfer(sourceWallet, destinationWallet, transferAmount); // state is now pending, source balance is 30k
//        destinationWallet.Freeze(); // Destination is now frozen

//        // Act
//        Action act = () => _transferService.FinalizeTransfer(transfer, sourceWallet, destinationWallet);

//        // Assert
//        act.Should().Throw<DomainException>().WithMessage("Operation cannot be performed on a non-active wallet.");
//        transfer.Status.Should().Be(TransferStatus.Failed);
//        sourceWallet.Balance.Amount.Should().Be(100000); // Funds reverted
//        sourceWallet.GetDomainEvents().Should().Contain(e => e is TransferFailedAndFundsReverted);
//        transfer.GetDomainEvents().Should().Contain(e => e is TransferFailed);
//    }
//}