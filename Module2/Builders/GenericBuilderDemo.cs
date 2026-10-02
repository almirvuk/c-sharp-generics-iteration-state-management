namespace Module2.Builders;

public static class GenericBuilderDemo
{
    public static void Run()
    {
        var paidOrder = new Builder<OrderEntity>()
            .With(o => o.Total = 250.00m)
            .With(o => o.Status = OrderStatus.Paid)
            .Build();

        Console.WriteLine($"Total:  {paidOrder.Total}");
        Console.WriteLine($"Status: {paidOrder.Status}");
        Console.WriteLine($"Email:  {paidOrder.CustomerEmail}  (untouched default)");
    }
}
