namespace Module2.Builders;

public sealed class Builder<T> where T : class, new()
{
    private readonly List<Action<T>> _changes = [];

    public Builder<T> With(Action<T> change)
    {
        _changes.Add(change);
        return this;
    }

    public T Build()
    {
        var instance = new T();

        foreach (var change in _changes)
            change(instance);

        return instance;
    }
}
