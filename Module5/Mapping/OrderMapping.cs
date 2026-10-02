namespace Module5.Mapping;

public static class OrderMapping
{
    extension(Order order)
    {
        public OrderDto ToDto() =>
            new(order.Id, order.CustomerEmail, order.Total, order.Status.ToString());

        public bool IsRefundable => order.Status == OrderStatus.Delivered;
    }

    extension(OrderDto dto)
    {
        public Order ToDomain() =>
            new(dto.Id, dto.CustomerEmail, dto.Total,
                Enum.Parse<OrderStatus>(dto.Status));
    }

    extension(OrderDto)
    {
        public static OrderDto Empty =>
            new(Guid.Empty, "", 0m, nameof(OrderStatus.Pending));
    }
}