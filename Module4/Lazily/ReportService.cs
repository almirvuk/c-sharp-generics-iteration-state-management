namespace Module4.Lazily;

public sealed class ReportService
{
    private readonly Lazy<Config> _config = new(() => Config.LoadFromDisk());

    public Config Config => _config.Value;

    public string Region
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    } = string.Empty;
}
