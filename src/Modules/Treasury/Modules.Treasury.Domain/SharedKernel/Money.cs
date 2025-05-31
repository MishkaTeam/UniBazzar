namespace Modules.Treasury.Domain.SharedKernel;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    private Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.");

        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency) => new(amount, currency);
}
