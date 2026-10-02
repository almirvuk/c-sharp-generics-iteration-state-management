namespace Module2.Resilience;

public static class RetryHelper
{
    public static Order RetryDbCall(Func<Order> operation)
    {
        for (var attempt = 1; ; attempt++)
        {
            try
            {
                return operation();
            }
            catch (SqlException) when (attempt < 3)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(200 * attempt));
            }
        }
    }

    public static HttpResponse RetryHttpCall(Func<HttpResponse> operation)
    {
        for (var attempt = 1; ; attempt++)
        {
            try
            {
                return operation();
            }
            catch (HttpRequestException) when (attempt < 3)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(200 * attempt));
            }
        }
    }

    public static bool RetryPublish(Func<bool> operation)
    {
        for (var attempt = 1; ; attempt++)
        {
            try
            {
                return operation();
            }
            catch (BrokerException) when (attempt < 3)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(200 * attempt));
            }
        }
    }
}
