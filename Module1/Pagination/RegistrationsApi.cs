namespace Module1.Pagination;

// The page-fetcher is injected as a delegate so there's no HTTP wiring in the
// demo; it stands in for an HttpClient call or a database query.
public sealed class RegistrationsApi(Func<Guid, string?, RegistrationPage> fetchPage)
{
    public IEnumerable<Registration> GetAllForEvent(Guid eventId)
    {
        string? cursor = null;

        do
        {
            var page = fetchPage(eventId, cursor);

            foreach (var registration in page.Items)
                yield return registration;

            cursor = page.NextCursor;
        }
        while (cursor is not null);
    }
}
