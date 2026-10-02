using Module6.NullObject;
using Module6.Options;

namespace Module6;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.FirstOrDefault())
        {
            case "nullobject":
                NullObjectDemo.Run();
                break;
            case "option":
                OptionDemo.Run();
                break;
            default:
                Console.WriteLine("Usage: dotnet run -- [nullobject|option]");
                break;
        }
    }
}
