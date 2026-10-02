namespace Module4.Lazily;

public sealed record Config(string ApiBaseUrl, int TimeoutSeconds)
{
    public static Config LoadFromDisk()
    {
        Console.WriteLine("  [Config.LoadFromDisk() ran]");
        return new Config("https://api.example.com", 30);
    }
}
