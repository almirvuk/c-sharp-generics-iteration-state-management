namespace Module1.Pagination;

// Module 1, Clip 4 — Building a Lazy Pagination Cursor.
// The payoff is the fetch counts: the SAME iterator does very different amounts
// of work depending on how the caller consumes it.
public static class LazyPaginationDemo
{
    public static void Run()
    {
        // Fake source: 150 registrations, served in pages of 50 (3 pages).
        var allRegistrations = Enumerable.Range(1, 150)
            .Select(i => new Registration(
                Id: Guid.NewGuid(),
                Name: $"Attendee {i}",
                Tier: i % 5 == 0 ? Tier.Vip : Tier.Standard,   // first VIP is Attendee 5
                IsCheckedIn: i % 3 == 0))
            .ToList();

        var fetchCount = 0;

        RegistrationPage FetchPage(Guid eventId, string? cursor)
        {
            fetchCount++;
            const int pageSize = 50;
            var skip = cursor is null ? 0 : int.Parse(cursor);
            var items = allRegistrations.Skip(skip).Take(pageSize).ToList();
            var next = skip + pageSize < allRegistrations.Count
                ? (skip + pageSize).ToString()
                : null;
            return new RegistrationPage(items, next);
        }

        var api = new RegistrationsApi(FetchPage);
        var eventId = Guid.NewGuid();

        fetchCount = 0;
        var all = api.GetAllForEvent(eventId).ToList();
        Console.WriteLine($"ToList()            -> {fetchCount} page(s), {all.Count} items");

        fetchCount = 0;
        var firstForty = api.GetAllForEvent(eventId).Take(40).ToList();
        Console.WriteLine($"Take(40).ToList()   -> {fetchCount} page(s), {firstForty.Count} items");

        fetchCount = 0;
        var firstVip = api.GetAllForEvent(eventId).FirstOrDefault(r => r.Tier == Tier.Vip);
        Console.WriteLine($"FirstOrDefault(VIP) -> {fetchCount} page(s), found '{firstVip?.Name}'");
    }
}
