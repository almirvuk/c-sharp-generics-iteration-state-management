using Module2.Resilience;
using Module2.Builders;

namespace Module2;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.FirstOrDefault())
        {
            case "retry":
                GenericRetryDemo.Run();
                break;
            case "builder":
                GenericBuilderDemo.Run();
                break;
            default:
                Console.WriteLine("Usage: dotnet run -- [retry|builder]");
                break;
        }
    }
}
