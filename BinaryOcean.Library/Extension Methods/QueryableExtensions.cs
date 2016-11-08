using System;
using System.Linq;
using System.Linq.Expressions;

namespace BinaryOcean.Library
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static T RandomElement<T>(this IQueryable<T> source)
        {
            return source.Skip(new Random().Next(source.Count())).First();
        }
    }
}