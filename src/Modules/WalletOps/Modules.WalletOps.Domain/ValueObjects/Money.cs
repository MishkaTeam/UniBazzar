using System.Numerics;

namespace Modules.WalletOps.Domain.ValueObjects;

// Domain/ValueObjects/Money.cs
public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency must be specified.", nameof(currency));

        if (amount < 0)
            throw new ArgumentException("Transaction amount must be positive.", nameof(amount));

        Amount = amount;
        Currency = currency.ToUpper();
    }

    public static Money Create(decimal amount, string currency) => new(amount, currency);
    public static Money Zero(string currency) => new(0, currency);
    public bool IsGreaterThan(Money other) => Amount > other.Amount && Currency == other.Currency;
    public bool IsGreaterThanOrEqualTo(Money other) => Amount >= other.Amount && Currency == other.Currency;

    internal Money Min(Money nonWithdrawableBalance)
    {
        if (Amount < nonWithdrawableBalance.Amount)
            return this;
        else if (Amount > nonWithdrawableBalance.Amount)
            return nonWithdrawableBalance;
        else return nonWithdrawableBalance;
    }


    public static Money operator +(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        if (a.Amount < b.Amount) throw new InvalidOperationException("Resulting money amount cannot be negative.");
        return new Money(a.Amount - b.Amount, a.Currency);
    }

    //public static bool operator ==(Money? a, Money? b)
    //{
    //    if (ReferenceEquals(a, b)) return true;
    //    if (a is null || b is null) return false;
    //    return a.Equals(b);
    //}

    //public static bool operator !=(Money? a, Money? b)
    //{
    //    EnsureSameCurrency(a, b);
    //    return !(a == b);
    //}

    // --- عملگرهای مقایسه‌ای ---
    public static bool operator <(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return a.Amount < b.Amount;
    }

    public static bool operator >(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return a.Amount > b.Amount;
    }

    public static bool operator <=(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return a.Amount <= b.Amount;
    }

    public static bool operator >=(Money a, Money b)
    {
        EnsureSameCurrency(a, b);
        return a.Amount >= b.Amount;
    }


    private static void EnsureSameCurrency(Money a, Money b)
    {
        if (a.Currency != b.Currency)
        {
            throw new InvalidOperationException($"Cannot perform operation on money with different currencies ('{a.Currency}' and '{b.Currency}').");
        }
    }

    public static Money Min(Money a, Money b) => a <= b ? a : b;
    public static Money Max(Money a, Money b) => a >= b ? a : b;

    public override string ToString() => $"{Amount:N2} {Currency}";

}
