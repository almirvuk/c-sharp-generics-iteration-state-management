namespace Module4.Stats;

public static class ComputeStatsDemo
{
    public static (decimal Min, decimal Max, decimal Avg, int Count) 
        ComputeStats(IEnumerable<decimal> values)
    {

        decimal min = decimal.MaxValue, max = decimal.MinValue, sum = 0;
        var count = 0;

        foreach (var v in values)
        {
            if (v < min) min = v;
            if (v > max) max = v;
            sum += v;
            count++;
        }

        var avg = count == 0 ? 0 : sum / count;

        return (min, max, avg, count);
    }

    public static void Run()
    {
        var prices = new[] { 8.50m, 40.00m, 120.00m, 250.00m, 12.25m };

        // Read the whole tuple by name.
        var stats = ComputeStats(prices);

        Console.WriteLine($"\nCount: {stats.Count}");
        Console.WriteLine($"Range: {stats.Min} to {stats.Max}, avg {stats.Avg:0.00}");

        // Or deconstruct, discarding what this caller doesn't need.
        var (min, max, _, _) = ComputeStats(prices);

        Console.WriteLine($"Just the range: {min} to {max}\n");
    }
}
