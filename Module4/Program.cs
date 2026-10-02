using Module4.Lazily;
using Module4.Stats;

namespace Module4;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.FirstOrDefault())
        {
            case "lazy":
                LazyConfigDemo.Run();
                break;
            case "stats":
                ComputeStatsDemo.Run();
                break;
            default:
                Console.WriteLine("Usage: dotnet run -- [lazy|stats]");
                break;
        }
    }
}
