namespace StateTaxService.AIAT.Inventory.Extensions;

public static class IEnumerablePaginationsExtension
{
    public static IEnumerable<T> Pagination<T>(this IEnumerable<T> values, int? page = 1, int? pageSize = null)
    {
        if (page.HasValue && pageSize.HasValue)
        {
            return values.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }
        if (pageSize.HasValue)
        {
            return values.Take(pageSize.Value);
        }

        return values;
    }
}
