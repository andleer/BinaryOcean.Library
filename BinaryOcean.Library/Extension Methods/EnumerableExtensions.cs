using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryOcean.Library
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> source)
        {
            return source ?? Enumerable.Empty<TSource>();
        }

        public static T RandomElement<T>(this IEnumerable<T> source)
        {
            return source.ElementAt(new Random().Next(source.Count()));
        }
    }
}