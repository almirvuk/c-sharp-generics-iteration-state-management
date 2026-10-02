using Module5.Mapping;
using Module5.Registry;

namespace Module5;

public static class Program
{
    public static void Main(string[] args)
    {
        switch (args.FirstOrDefault())
        {
            case "mapping":
                ExtensionMappingDemo.Run();
                break;
            case "registry":
                MappingRegistryDemo.Run();
                break;
            default:
                Console.WriteLine("Usage: dotnet run -- [mapping|registry]");
                break;
        }
    }
}
