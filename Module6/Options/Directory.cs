namespace Module6.Options;

public sealed record Customer(string Name, string? ManagerName);

public static class Directory
{
    public static Option<Customer> FindManager(Customer customer) =>
        customer.ManagerName is { } name
            ? new Some<Customer>(new Customer(name, ManagerName: null))
            : new None<Customer>();
}