namespace AoCUtils
{
    static class Extensions
    {
        public static T Pop<T>(this ICollection<T> items)
        {
            T? item = items.FirstOrDefault();
            if (item != null)
            {
                items.Remove(item);
            }
            return item ?? throw new ArgumentNullException(nameof(item), message: "item is null");
        }
    }
}