using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlPatch
{
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
        }
    }
}
