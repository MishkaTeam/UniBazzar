using BuildingBlocks.Domain.Aggregates;
using Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;
using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx;

public class Transaction : Entity
{
    public Guid WalletId { get; private set; }
    public Money Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Wallet Wallet { get; private set; }

    private Transaction(Guid walletId, Money amount, TransactionType type, Guid? transferId) 
    {
        WalletId = walletId;
        Amount = amount;
        Type = type;
    }

    public static Transaction NonWithdrawableDeposit(Guid walletId, Money amount) => new(walletId, amount, TransactionType.Non_Withdrawable_Deposit, null);
    
    public static Transaction WithdrawableDeposit(Guid walletId, Money amount) => new(walletId, amount, TransactionType.Withdrawable_Deposit, null);

    public static Transaction Withdrawal(Guid walletId, Money amount) => new(walletId, amount, TransactionType.Withdrawal, null);

    internal static Money NonWithdrawableBalance()
    {
        throw new NotImplementedException();
    }


    // For EF Core
    private Transaction() { }
}