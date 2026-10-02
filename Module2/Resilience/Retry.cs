namespace Module2.Resilience;

public static class Retry
{
    public static T Run<T>(Func<T> operation, RetryOptions options)
    {
        for (var attempt = 1; ; attempt++)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
                when (options.ShouldRetry(ex) && attempt < options.MaxAttempts)
            {
                Thread.Sleep(options.BaseDelay * attempt);
            }
        }
    }
}
