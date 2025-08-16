namespace Modules.WalletOps.Application.Contracts;

public sealed class WalletBalanceResponseContract
{
    public MoneyContract WithdrawableBalance { get; set; }
    public MoneyContract NonWithdrawableBalance { get; set; }
    public MoneyContract HeldBalance { get; set; }
    public MoneyContract TotalBalance { get; set; }
    public MoneyContract AvailableBalance { get; set; }
}
