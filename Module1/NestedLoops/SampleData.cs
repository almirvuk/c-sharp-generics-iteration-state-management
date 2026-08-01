namespace Module1.NestedLoops;

public static class SampleData
{
    public static IReadOnlyList<Customer> Customers { get; } = Build();

    private static IReadOnlyList<Customer> Build()
    {
        static LineItem Item(string sku, decimal price, params string[] flags) =>
            new(Guid.NewGuid(), sku, price, flags.ToHashSet());

        return
        [
            new Customer(Guid.NewGuid(), "Ada Lovelace",
            [
                new Order(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(-3),
                [
                    Item("SKU-100", 19.99m),
                    Item("SKU-101", 49.00m, "gift"),
                ]),
                new Order(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(-1),
                [
                    Item("SKU-200", 120.00m, "priority", "fragile"),  // the match
                    Item("SKU-201", 8.50m),
                ]),
            ]),
            new Customer(Guid.NewGuid(), "Alan Turing",
            [
                new Order(Guid.NewGuid(), DateTimeOffset.UtcNow.AddDays(-2),
                [
                    Item("SKU-300", 15.00m),
                ]),
            ]),
        ];
    }
}
