using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Find<T>(this IEnumerable<T> source, Func<T, bool> isMatch)
        {
            /* yield here is an iterator
             * In this case yield will return the matches as they are found
             * Another approach would be to create a holding list, add items as they are found
             * then return the list, but this approach is cleaner and returns items as they are found?
             */
            foreach(var item in source)
            {
                if(isMatch(item))
                {
                    yield return item;
                }
            }
        }
    }
}
