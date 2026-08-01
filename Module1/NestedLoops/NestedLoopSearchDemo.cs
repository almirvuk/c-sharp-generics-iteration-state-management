namespace Module1.NestedLoops;

public static class NestedLoopSearchDemo
{
    public static void Run()
    {
        var customers = SampleData.Customers;
        const string flag = "priority";

        var before = FlaggedItemSearch.FindFirstFlaggedItem_Before(customers, flag);
        var after  = FlaggedItemSearch.FindFirstFlaggedItem_After(customers, flag);
        var linq   = FlaggedItemSearch.FindFirstFlaggedItem_Linq(customers, flag);

        Console.WriteLine($"Before: {before?.Sku ?? "none"}");
        Console.WriteLine($"After:  {after?.Sku ?? "none"}");
        Console.WriteLine($"LINQ:   {linq?.Sku ?? "none"}");

        var allAgree = before?.Id == after?.Id && after?.Id == linq?.Id;
        Console.WriteLine(allAgree ? "All three agree." : "MISMATCH.");
    }
}
