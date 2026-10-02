namespace Module6.NullObject;

public static class NullObjectDemo
{
    public static void Run()
    {
        var withDiscountPolicy = new Checkout(new PercentageDiscount(10m));
        Console.WriteLine($"With 10% policy: {withDiscountPolicy.Apply(250.00m)}");

        var withoutDiscountPolicy = new Checkout(new NoDiscount());
        Console.WriteLine($"With NoDiscount: {withoutDiscountPolicy.Apply(250.00m)}");
    }
}
