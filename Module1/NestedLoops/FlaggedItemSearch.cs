namespace Module1.NestedLoops;

// Module 1, Clip 2 — Refactoring a Nested-Loop Search.
// On camera you refactor ONE method (FindFirstFlaggedItem) in place:
// before -> extract inner loops + return early -> LINQ coda. The three
// distinctly-named variants below exist so the demo can run and confirm they
// return the same item before recording.
public static class FlaggedItemSearch
{
    // BEFORE — three nest ed loops, a `found` flag, break at every level.
    public static LineItem? FindFirstFlaggedItem_Before(
        IEnumerable<Customer> customers, string flag)
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

    // AFTER — extract the inner loops, return early. `return` is the
    // multi-level break; the flag disappears.
    public static LineItem? FindFirstFlaggedItem_After(
        IEnumerable<Customer> customers, string flag)
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

    // CODA — the version most teams would ship for a read-only query.
    public static LineItem? FindFirstFlaggedItem_Linq(
        IEnumerable<Customer> customers, string flag) =>
        customers
            .SelectMany(c => c.Orders)
            .SelectMany(o => o.LineItems)
            .FirstOrDefault(i => i.Flags.Contains(flag));
}
