using Modules.WalletOps.Domain.Aggregates.WalletTrx;

namespace Modules.WalletOps.Application.Contracts;

public sealed class WalletBalanceResponseContract
{
    public MoneyContract WithdrawableBalance { get; set; } = new();
    public MoneyContract NonWithdrawableBalance { get; set; } = new();
    public MoneyContract HeldBalance { get; set; } = new();
    public MoneyContract TotalBalance { get; set; } = new();
    public MoneyContract AvailableBalance { get; set; } = new();


    internal static WalletBalanceResponseContract FromWallet(Wallet wallet)
    {
        return new WalletBalanceResponseContract
        {
            AvailableBalance = MoneyContract.FromMoney(wallet.AvailableBalance),
            HeldBalance = MoneyContract.FromMoney(wallet.HeldBalance),
            NonWithdrawableBalance = MoneyContract.FromMoney(wallet.NonWithdrawableBalance),
            TotalBalance = MoneyContract.FromMoney(wallet.TotalBalance),
            WithdrawableBalance = MoneyContract.FromMoney(wallet.WithdrawableBalance),
        };
    }
}
