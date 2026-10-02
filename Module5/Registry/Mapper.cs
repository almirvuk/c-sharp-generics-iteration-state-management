namespace Module5.Registry;

public sealed class Mapper
{
    private readonly Dictionary<(Type Source, Type Target), Delegate> _mappers = [];

    public void Register<TSource, TTarget>(Func<TSource, TTarget> map) =>
        _mappers[(typeof(TSource), typeof(TTarget))] = map;

    public TTarget Map<TSource, TTarget>(TSource source)
    {
        if (!_mappers.TryGetValue((typeof(TSource), typeof(TTarget)), out var map))
            throw new InvalidOperationException(
                $"No mapping registered for {typeof(TSource).Name} -> {typeof(TTarget).Name}.");

        return ((Func<TSource, TTarget>)map)(source);
    }
}
