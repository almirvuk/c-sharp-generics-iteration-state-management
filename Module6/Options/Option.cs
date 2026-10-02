namespace Module6.Options;

public abstract record Option<T>;
public sealed record Some<T>(T Value) : Option<T>;
public sealed record None<T> : Option<T>;
