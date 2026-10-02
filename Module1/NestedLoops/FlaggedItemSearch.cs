namespace Module1.NestedLoops;

public static class FlaggedItemSearch
{
    public static LineItem? FindFirstFlaggedItem_Before(IEnumerable<Customer> customers, string flag)
    {
        LineItem? found = null;

        foreach (var customer in customers)
        {
            foreach (var order in customer.Orders)
            {
                foreach (var item in order.LineItems)
                {
                    if (item.Flags.Contains(flag))
                    {
                        found = item;
                        break;
                    }
                }

                if (found is not null) break;
            }

            if (found is not null) break;
        }

        return found;
    }

    public static LineItem? FindFirstFlaggedItem_After(IEnumerable<Customer> customers, string flag)
    {
        foreach (var customer in customers)
        {
            var match = FindFlaggedItem(customer, flag);
            if (match is not null) return match;
        }

        return null;
    }

    private static LineItem? FindFlaggedItem(Customer customer, string flag)
    {
        foreach (var order in customer.Orders)
            foreach (var item in order.LineItems)
                if (item.Flags.Contains(flag))
                    return item;

        return null;
    }

    
    public static LineItem? FindFirstFlaggedItem_Linq(
        IEnumerable<Customer> customers, string flag) =>
        customers
            .SelectMany(c => c.Orders)
            .SelectMany(o => o.LineItems)
            .FirstOrDefault(i => i.Flags.Contains(flag));

}
