namespace Module6.NullObject;

public interface IDiscountPolicy
{
    decimal DiscountFor(decimal total);
}

public sealed class NoDiscount : IDiscountPolicy
{
    public decimal DiscountFor(decimal total) => 0m;
}

public sealed class PercentageDiscount(decimal percent) : IDiscountPolicy
{
    public decimal DiscountFor(decimal total) => total * (percent / 100m);
}
