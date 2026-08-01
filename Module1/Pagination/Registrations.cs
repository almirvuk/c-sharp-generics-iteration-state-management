namespace Module1.Pagination;

public sealed record RegistrationPage(IReadOnlyList<Registration> Items, string? NextCursor);
public sealed record Registration(Guid Id, string Name, Tier Tier, bool IsCheckedIn);
public enum Tier { Standard, Vip }
