using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinchillada.BehaviourSelections.Utilities
{
    internal static class ListExtensions
    {
        /// <summary>
        /// Tries to find an element that satisfies the <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of elements in the collection.</typeparam>
        /// <param name="collection">The collection we're searching through.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="target">The out variable that we store the found element in.</param>
        /// <returns>True if found, false if not.</returns>
        public static bool TryFind<T>(this IEnumerable<T> collection, Predicate<T> predicate, out T target)
        {
            target = collection.FirstOrDefault(element => predicate(element));
            return target != null;
        }

        /// <summary>
        /// Chooses the best element of the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The collection we want the best of.</param>
        /// <param name="evaluationFunction">The function that evaluates individual elements.</param>
        /// <returns>The element that scores highest with the <paramref name="evaluationFunction"/>.</returns>
        public static T Best<T>(this IEnumerable<T> enumerable, Func<T, float> evaluationFunction)
        {
            float bestScore = float.MinValue;
            T bestElement = default(T);

            foreach (T element in enumerable)
            {
                float score = evaluationFunction(element);
                if(score <= bestScore)
                    continue;

                bestScore = score;
                bestElement = element;
            }

            return bestElement;
        }

        /// <summary>
        /// Returns the last index of the <paramref name="enumerable"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The collection we want the last index of.</param>
        /// <returns>The index of the last element in <paramref name="enumerable"/>.</returns>
        public static int LastIndex<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() - 1;
        }

        /// <summary>
        /// Removes and returns the first element in the <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <paramref name="list"/>.</typeparam>
        /// <param name="list">The list we want to grab the first element from.</param>
        /// <returns>The grabbed element.</returns>
        public static T GrabFirst<T>(this LinkedList<T> list)
        {
            if (!list.Any())
                return default(T);

            T element = list.First.Value;
            list.RemoveFirst();

            return element;
        }
    }
}
