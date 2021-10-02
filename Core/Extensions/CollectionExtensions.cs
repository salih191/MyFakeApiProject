using System;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if(target==null)
                throw new ArgumentNullException(nameof(target));
            if(source==null)
                return;
            foreach(var element in source)
                target.Add(element);
        }
    }
}