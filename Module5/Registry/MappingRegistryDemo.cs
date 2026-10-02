namespace Module5.Registry;

public static class MappingRegistryDemo
{
    public static void Run()
    {
        var order = new Order(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "grace@example.com", 99.00m);

        var mapper = new Mapper();
        mapper.Register<Order, OrderDto>(o => new OrderDto(o.Id, o.CustomerEmail, o.Total));

        var dto = mapper.Map<Order, OrderDto>(order);
        Console.WriteLine($"Mapped: {dto.CustomerEmail}, {dto.Total}");

        try
        {
            var bad = mapper.Map<Order, string>(order);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\nUnregistered: {ex.Message}");
        }
    }
}