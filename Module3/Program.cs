using Module3.Actions;
using Module3.Machine;

namespace Module3;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.FirstOrDefault())
        {
            case "actions":
                NextActionDemo.Run();
                break;
            case "machine":
                StateMachineDemo.Run();
                break;
            default:
                Console.WriteLine("Usage: dotnet run -- [actions|machine]");
                break;
        }
    }
}
