namespace Module6.Options;

public static class OptionDemo
{
    public static void Run()
    {
        var ada   = new Customer("Ada Lovelace", ManagerName: "Grace Hopper");
        var grace = new Customer("Grace Hopper", ManagerName: null);

        Console.WriteLine($"Ada:   {DescribeManager(ada)}");
        Console.WriteLine($"Grace: {DescribeManager(grace)}");
    }

    
    private static string DescribeManager(Customer customer) =>
        Directory.FindManager(customer) switch
        {
            Some<Customer>(var manager) => $"Notify {manager.Name}",
            None<Customer>              => "No manager — escalate instead",

            _ => throw new InvalidOperationException("Unreachable.")
        };
}
