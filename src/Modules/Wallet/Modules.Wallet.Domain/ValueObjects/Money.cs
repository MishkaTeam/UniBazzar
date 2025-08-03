namespace Modules.Wallet.Domain.ValueObjects;

// Domain/ValueObjects/Money.cs
public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency must be specified.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpper();
    }

    public static Money Create(decimal amount, string currency) => new(amount, currency);
    public static Money Zero(string currency) => new(0, currency);

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot sum money of different currencies.");
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new InvalidOperationException("Cannot subtract money of different currencies.");
        return new Money(a.Amount - b.Amount, a.Currency);
    }

    public bool IsGreaterThan(Money other) => this.Amount > other.Amount && this.Currency == other.Currency;
    public bool IsGreaterThanOrEqualTo(Money other) => this.Amount >= other.Amount && this.Currency == other.Currency;
}
