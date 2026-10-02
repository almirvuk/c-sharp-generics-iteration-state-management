namespace Module2.Resilience;
using static Operations;

public static class GenericRetryDemo
{
    public static void Run()
    {
        var id = Guid.NewGuid();
        var url = "https://api.example.com";
        var message = "order-created";

        var order = Retry.Run(() => LoadOrder(id), RetryOptions.For<SqlException>());
        var response = Retry.Run(() => CallApi(url), RetryOptions.For<HttpRequestException>());
        var published = Retry.Run(() => Publish(message), RetryOptions.For<BrokerException>(maxAttempts: 5));
    }
}