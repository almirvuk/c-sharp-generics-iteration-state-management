namespace Module2.Builders;

public enum OrderStatus { Pending, Paid, Shipped, Delivered, Cancelled, Refunded }

public sealed record OrderEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CustomerEmail { get; set; } = "test@example.com";
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTimeOffset PlacedAt { get; set; } = DateTimeOffset.UtcNow;
}
