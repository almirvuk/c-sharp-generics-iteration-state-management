namespace Module3.Actions;

public enum OrderStatus { Pending, Paid, Shipped, Delivered, Cancelled, Refunded }

public sealed record Order(
    OrderStatus Status,
    bool IsPaid,
    bool IsShipped,
    decimal Total);
