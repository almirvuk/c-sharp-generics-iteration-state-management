namespace Module6.NullObject;

public sealed class Checkout(IDiscountPolicy discount)
{
    private readonly IDiscountPolicy _discount =
        discount ?? throw new ArgumentNullException(nameof(discount));

    public decimal Apply(decimal total) => total - _discount.DiscountFor(total);
}