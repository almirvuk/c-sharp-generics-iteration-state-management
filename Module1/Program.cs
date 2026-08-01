using Module1.NestedLoops;
using Module1.Pagination;

namespace Module1;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.FirstOrDefault())
        {
            case "nested":
                NestedLoopSearchDemo.Run();
                break;
            case "pagination":
                LazyPaginationDemo.Run();
                break;
            default:
                Console.WriteLine("Usage: dotnet run -- [nested|pagination]");
                break;
        }
    }
}
