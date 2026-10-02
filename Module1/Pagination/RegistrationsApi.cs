namespace Module1.Pagination;

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
