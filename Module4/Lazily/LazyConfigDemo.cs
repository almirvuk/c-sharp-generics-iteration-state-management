namespace Module4.Lazily;

public static class LazyConfigDemo
{
    public static void Run()
    {
        Console.WriteLine("\nCreating ReportService...");
        var service = new ReportService();
        Console.WriteLine("Service created. (Notice: config has NOT loaded yet.)");

        // The field-backed property works independently of the lazy config.
        service.Region = "EU";
        Console.WriteLine($"\nRegion set to: {service.Region}");

        Console.WriteLine("\nReading Config for the first time...");
        var first = service.Config;         // factory runs here
        Console.WriteLine($"  ApiBaseUrl = {first.ApiBaseUrl}");

        Console.WriteLine("\nReading Config again...");
        var second = service.Config;        // cached — factory does NOT run again
        Console.WriteLine($"Same instance? {ReferenceEquals(first, second)}");

        Console.WriteLine();
    }
}
