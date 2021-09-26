namespace Core.Extensions
{
    public static class ClassExtensions
    {
        public static T New<T>(this T t) where T : class, new()
        {
            return t ??= new T();
        }
    }
}