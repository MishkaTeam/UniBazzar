using Modules.WalletOps.Domain.ValueObjects;

namespace Modules.WalletOps.Application.Contracts;

public record MoneyContract
{
    public decimal Amount { get; set; } = 0;
    public string Currency { get; set; } = "IRR";

    internal static MoneyContract FromMoney(Money availableBalance)
    {
        return new MoneyContract
        {
            Amount = availableBalance.Amount,
            Currency = availableBalance.Currency,
        };
    }
}