using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Domain.Aggregates.WalletTrx.Extenstions
{
    internal static class WalletExtensions
    {
        public static Money WithdrawableBalance(this IReadOnlyCollection<Transaction> transactions)
        {
            var amount = 0m;

            foreach (var item in transactions.Where(x => x.Type == )
            {
                if()
            }
        }

        public static Money NonWithdrawableBalance(this IReadOnlyCollection<Transaction> transactions)
        {

        }


        public static Money HeldBalance(this IReadOnlyCollection<HeldFund> heldFunds)
        {

        }
    }
}
