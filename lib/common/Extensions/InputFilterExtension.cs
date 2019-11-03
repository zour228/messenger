using System.Linq;
using Common.Util;

namespace Common.Extensions
{
    public static class InputFilterExtension
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, IInputFilter<T> filter)
        {
            return filter.Process(query);
        }
    }
}