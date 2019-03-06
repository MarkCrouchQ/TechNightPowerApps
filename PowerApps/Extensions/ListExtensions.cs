using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerApps.Extension
{
    public static class ListExtensions
    {
        public static Random Random = new Random();

        public static T GetRandomItem<T>(this List<T> list)
        {
            if (list.Count == 0)
                return default(T);
            return list[Random.Next(0, list.Count)];
            
        }

        public static IEnumerable<int> RandomLoop(int max, int min = 1)
        {
            var random = Random.Next(min, max);
            for (var i = 0; i < random; i++)
            {
                yield return i;
            }
        }

        public static void AddToFront<T>(this List<T> list, T item)
        {
            // omits validation, etc.
            list.Insert(0, item);
        }
    }
}
