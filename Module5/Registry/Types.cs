namespace Module5.Registry;

public sealed record Order(Guid Id, string CustomerEmail, decimal Total);
public sealed record OrderDto(Guid Id, string CustomerEmail, decimal Total);