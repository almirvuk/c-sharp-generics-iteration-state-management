namespace Module5.Mapping;

public enum OrderStatus { Pending, Paid, Shipped, Delivered, Cancelled, Refunded }

public sealed record Order(Guid Id, string CustomerEmail, decimal Total, OrderStatus Status);
public sealed record OrderDto(Guid Id, string CustomerEmail, decimal Total, string Status);
