namespace Module2.Resilience;

public sealed record Order(Guid Id, decimal Total);

public sealed class SqlException : Exception;
