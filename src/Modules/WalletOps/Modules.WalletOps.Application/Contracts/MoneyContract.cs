namespace Modules.WalletOps.Application.Contracts;

public record MoneyContract
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}