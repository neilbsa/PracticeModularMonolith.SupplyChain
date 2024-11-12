using System;
using System.Linq;

namespace SupplyChain.Common.Domain;
public sealed record Quantity
{
    public decimal Value { get; init; }

    public Quantity(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Cannot set quantity value to < 0");
        }
        Value = value;
    }

    public static Quantity operator +(Quantity First, Quantity Second)
    {
        decimal sum = First.Value + Second.Value;
        return new Quantity(sum);
    }

    public static Quantity operator -(Quantity First, Quantity Second)
    {
        if (First.Value < Second.Value)
        {
            throw new InvalidOperationException("Cannot deduct quantity. Negative value is invalid");
        }


        decimal diff = First.Value - Second.Value;
        return new Quantity(diff);
    }

    public static Quantity Zero() => new Quantity(0);

};
