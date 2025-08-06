namespace Modules.WalletOps.Domain.Aggregates.WalletTrx.Enums;

public enum TransactionType : byte
{
    Withdrawable_Deposit,
    Non_Withdrawable_Deposit,
    Withdrawal,
    Purchase
}
