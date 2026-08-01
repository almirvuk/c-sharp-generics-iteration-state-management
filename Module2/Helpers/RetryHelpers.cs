using Module2.Domain;
using Module2.Exceptions;
using Module2.Transport;
using HttpRequestException = Module2.Exceptions.HttpRequestException;

public static class RetryHelpers
{
    // Retry a database operation up to 3 times with linear backoff.
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

    // Retry an HTTP call up to 3 times with linear backoff.
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

    // Retry a broker publish up to 3 times with linear backoff.
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
