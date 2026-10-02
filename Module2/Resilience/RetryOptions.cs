namespace Module2.Resilience;

public sealed record RetryOptions(
    int MaxAttempts,
    TimeSpan BaseDelay,
    Func<Exception, bool> ShouldRetry)
{
    public static RetryOptions For<TException>(int maxAttempts = 3)
        where TException : Exception =>
        new(maxAttempts, TimeSpan.FromMilliseconds(200), ex => ex is TException);
}
