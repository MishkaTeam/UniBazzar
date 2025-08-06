using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx.Extenstions
{
    internal static class WalletCalculationService
    {
        public static Money WithdrawableBalance(IReadOnlyCollection<Transaction> transactions, IReadOnlyCollection<HeldFund> heldFunds)
        {
            var amount = 0m;
            var withdrawableDeposits = transactions.Where(x => x.Type == Enums.TransactionType.Withdrawable_Deposit);
            var purchases = transactions.Where(x => x.Type == Enums.TransactionType.Purchase);
            var withdraws = transactions.Where(x => x.Type == Enums.TransactionType.Withdrawal);

            amount = withdrawableDeposits.Sum(x => x.Amount.Amount) - (purchases.Sum(x => x.Amount.Amount) + withdraws.Sum(x => x.Amount.Amount));

            return Money.Create(amount, "IRR");
        }

        public static Money NonWithdrawableBalance(IReadOnlyCollection<Transaction> transactions)
        {
            var amount = 0m;
            var nonWithdrawableDeposits = transactions.Where(x => x.Type == Enums.TransactionType.Non_Withdrawable_Deposit);
            var purchases = transactions.Where(x => x.Type == Enums.TransactionType.Purchase);
            var withdraws = transactions.Where(x => x.Type == Enums.TransactionType.Withdrawal);

            amount = nonWithdrawableDeposits.Sum(x => x.Amount.Amount) - (purchases.Sum(x => x.Amount.Amount) + withdraws.Sum(x => x.Amount.Amount));

            return Money.Create(amount, "IRR");
        }
    }
}
