namespace Module2.Resilience;

public static class Operations
{
    public static Order LoadOrder(Guid id) => new(id, 42.00m);
    public static HttpResponse CallApi(string url) => new(200, "OK");
    public static bool Publish(string message) => true;
}
