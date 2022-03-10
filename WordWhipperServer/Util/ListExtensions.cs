using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordWhipperServer.Util
{
    /// <summary>
    /// Helper class for lists
    /// </summary>
    public static class ListExtensions
    {
        private static Random rng = new Random();

        /// <summary>
        /// Shufles a list
        /// </summary>
        /// <typeparam name="T">the type of list</typeparam>
        /// <param name="list">the actual list</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static bool ContainsAllItems<T>(this List<T> a, List<T> b)
        {
            return !b.Except(a).Any();
        }
    }
}
