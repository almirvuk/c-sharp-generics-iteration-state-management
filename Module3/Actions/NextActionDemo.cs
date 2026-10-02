using static Module3.Actions.OrderStatus;

namespace Module3.Actions;

public static class NextActionDemo
{
    public static void Run()
    {
        var samples = new[]
        {
            new Order(Pending,   IsPaid: false, IsShipped: false, Total: 40m),
            new Order(Paid,      IsPaid: true,  IsShipped: false, Total: 40m),
            new Order(Shipped,   IsPaid: true,  IsShipped: true,  Total: 40m),
            new Order(Delivered, IsPaid: true,  IsShipped: true,  Total: 40m),
            new Order(Cancelled, IsPaid: false, IsShipped: false, Total: 40m),
        };

        var allAgree = true;

        foreach (var order in samples)
        {
            var before = OrderActions.GetNextAction_Before(order);
            var after  = OrderActions.GetNextAction_After(order);
            Console.WriteLine($"{order.Status,-10} -> {after}");
            if (before != after) allAgree = false;
        }

        Console.WriteLine(allAgree ? "Before and after agree." : "MISMATCH.");
    }
}
