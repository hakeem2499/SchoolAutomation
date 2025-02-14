using System;
using System.Collections.Generic;
using System.Globalization;
using Ndeal.SharedKernel;

namespace Ndeal.Domain.PaymentAggregate.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public CurrencyType Currency { get; }

    public Money(decimal amount, CurrencyType currency)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString()
    {
        CultureInfo culture = GetCurrencyCulture(Currency);
        return Amount.ToString("C", culture);
    }

    #region Currency Enum and Formatting
    public enum CurrencyType
    {
        Naira,
        Dollar,
        Pounds,
    }

    private static CultureInfo GetCurrencyCulture(CurrencyType currency)
    {
        return currency switch
        {
            CurrencyType.Naira => new CultureInfo("en-NG"), // Nigerian Naira
            CurrencyType.Dollar => new CultureInfo("en-US"), // US Dollar
            CurrencyType.Pounds => new CultureInfo("en-GB"), // British Pounds
            _ => CultureInfo.InvariantCulture,
        };
    }
    #endregion

    #region Arithmetic Operations
    public Money Add(Money other)
    {
        ValidateSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        ValidateSameCurrency(other);
        decimal newAmount = Amount - other.Amount;

        if (newAmount < 0)
        {
            throw new InvalidOperationException("Subtraction results in negative amount.");
        }

        return new Money(newAmount, Currency);
    }

    public Money Multiply(decimal factor)
    {
        if (factor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), "Factor cannot be negative.");
        }

        decimal newAmount = Amount * factor;
        return CreateRoundedMoney(newAmount);
    }

    public Money Divide(decimal divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Divisor cannot be zero.");
        }

        if (divisor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(divisor), "Divisor cannot be negative.");
        }

        decimal newAmount = Amount / divisor;
        return CreateRoundedMoney(newAmount);
    }

    private Money CreateRoundedMoney(decimal amount)
    {
        CultureInfo culture = GetCurrencyCulture(Currency);
        int decimalDigits = culture.NumberFormat.CurrencyDecimalDigits;
        decimal roundedAmount = Math.Round(amount, decimalDigits, MidpointRounding.AwayFromZero);
        return new Money(roundedAmount, Currency);
    }
    #endregion

    #region Operator Overloads
    public static Money operator +(Money left, Money right) => left.Add(right);

    public static Money operator -(Money left, Money right) => left.Subtract(right);

    public static Money operator *(Money money, decimal factor) => money.Multiply(factor);

    public static Money operator /(Money money, decimal divisor) => money.Divide(divisor);
    #endregion

    #region Comparison Methods
    public bool LessThan(Money other)
    {
        ValidateSameCurrency(other);
        return Amount < other.Amount;
    }

    public bool LessThanOrEqual(Money other)
    {
        ValidateSameCurrency(other);
        return Amount <= other.Amount;
    }

    public bool GreaterThan(Money other)
    {
        ValidateSameCurrency(other);
        return Amount > other.Amount;
    }

    public bool GreaterThanOrEqual(Money other)
    {
        ValidateSameCurrency(other);
        return Amount >= other.Amount;
    }

    private void ValidateSameCurrency(Money other)
    {
        if (Currency != other?.Currency)
        {
            throw new InvalidOperationException("Currency mismatch in operation.");
        }
    }
    #endregion
}
