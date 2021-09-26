using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            while (count > source.Count() &&source.Any())
            {
                source = source.Concat(source);
            }

            return source.Shuffle().NewTake(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static IEnumerable<T> NewTake<T>(this IEnumerable<T> source, int count)
        {
            if (count <= source.Count())
            {
                return source.Take(count);
            }

            IEnumerable<T> result = Array.Empty<T>();
            int _count = count;
            while (_count > 0)
            {
                result = result.Concat(source.Take(_count));
                _count = count-result.Count();
            }

            return result;
        }
    }
}