namespace Module5.Mapping;

public static class ExtensionMappingDemo
{
    public static void Run()
    {
        Order order = new Order(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "someone@example.com", 250.00m, OrderStatus.Delivered);

        OrderDto dto = order.ToDto();

        Console.WriteLine($"DTO:         {dto.CustomerEmail}, {dto.Total}, {dto.Status}");
        Console.WriteLine($"Refundable:  {order.IsRefundable}");

        var roundTripped = dto.ToDomain();
        Console.WriteLine($"Round-trip: {roundTripped.CustomerEmail}, {roundTripped.Status}");
        Console.WriteLine($"Empty DTO:  '{OrderDto.Empty.CustomerEmail}'");
    }
}
