namespace Module1.NestedLoops;

public sealed record LineItem(Guid Id, string Sku, decimal Price, IReadOnlySet<string> Flags);
public sealed record Order(Guid Id, DateTimeOffset PlacedAt, IReadOnlyList<LineItem> LineItems);
public sealed record Customer(Guid Id, string Name, IReadOnlyList<Order> Orders);
