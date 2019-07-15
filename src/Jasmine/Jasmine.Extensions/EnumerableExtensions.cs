using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Extensions
{

    /// <summary>
    /// thead unsafe converts
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// cast <see cref="IEnumerable{T}"/>to<see cref="IEnumerable{TTarget}"/>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget">a converter,convert<see cref="TSource"/>to<see cref="TTarget"/></typeparam>
        /// <param name="source"></param>
        /// <param name="caster"></param>
        /// <returns></returns>
        public static IEnumerable<TTarget>  Cast<TSource,TTarget>(this IEnumerable<TSource> source,Func<TSource,TTarget> caster)
        {
            foreach (var item in source)
            {
                yield return caster(item);
            }
        }

        public static IEnumerable<Tuple<T1,T2,T3>> ToTuple<TSource,T1,T2,T3>(this IEnumerable<TSource> source,
                                                                                  Func<TSource, Tuple<T1, T2, T3>> generator)
        {
            foreach (var item in source)
            {
                yield return generator(item);
            }
        }
        public static IEnumerable<Tuple<T1, T2, T3,T4>> ToTuple<TSource, T1, T2, T3,T4>(this IEnumerable<TSource> source, 
                                                                                             Func<TSource, Tuple<T1, T2, T3,T4>> generator)
        {
            foreach (var item in source)
            {
                yield return generator(item);
            }
        }
        public static IEnumerable<Tuple<T1, T2, T3, T4,T5>> ToTuple<TSource, T1, T2, T3, T4,T5>(this IEnumerable<TSource> source, 
                                                                                                     Func<TSource, Tuple<T1, T2, T3, T4,T5>> generator)
        {
            foreach (var item in source)
            {
                yield return generator(item);
            }
        }
        public static IEnumerable<Tuple<T1, T2, T3, T4, T5,T6>> ToTuple<TSource, T1, T2, T3, T4,T5, T6>(this IEnumerable<TSource> source, Func<TSource, 
                                                                                                             Tuple<T1, T2, T3, T4, T5,T6>> generator)
        {
            foreach (var item in source)
            {
                yield return generator(item);
            }
        }

        public static HashSet<T> ToSet<T>(this IEnumerable<T> source)
        {
            if (source is HashSet<T> set)
                return set;

             set = new HashSet<T>();

            foreach (var item in source)
            {
                //without check ,let it throw
                set.Add(item);
            }

            return set;
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> source)
        {
            if (source is Stack<T> stack)
                return stack;

            stack = new Stack<T>();

            foreach (var item in source)
            {
                stack.Push(item);
            }

            return stack;
        }
        public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
        {
            if (source is Queue<T> queue)
                return queue;

            queue = new Queue<T>();

            foreach (var item in source)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        public static T[] GetRange<T>(this T[] source,int start,int count)
        {
            var array = new T[count];

            Array.Copy(source, start, array, 0, count);

            return array;
        }
        /// <summary>
        /// group and cout
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="sources"></param>
        /// <param name="grouper">group element</param>
        /// <param name="asc">rank mode</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<Tkey,int>> Count<TSource,Tkey>(this IEnumerable<TSource> sources,Func<TSource,Tkey> grouper,bool asc=true)
        {
            var map = new Dictionary<Tkey, int>();

            foreach (var item in sources)
            {
                var key = grouper(item);

                if (!map.ContainsKey(key))
                    map.Add(key, 1);
                else
                    map[key]++;
            }

            var ls = map.ToList();

            if (asc)
                ls.Sort((x, y) => x.Value.CompareTo(y.Value));
            else
                ls.Sort((x, y) => y.Value.CompareTo(x.Value));

            return ls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sources"></param>
        /// <param name="keySelector"></param>
        /// <param name="sorter"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, List<TSource>>> Group<TSource, TKey>(this IEnumerable<TSource> sources, 
                                                                                          Func<TSource, TKey> keySelector,
                                                                                          Comparison< KeyValuePair<TKey,List<TSource>>> sorter=null)
        {
            var map = new Dictionary<TKey, List<TSource>>();

            foreach (var item in sources)
            {
                var key = keySelector(item);

                if (!map.ContainsKey(key))
                    map.Add(key, new List<TSource>());

                map[key].Add(item);
            }


            var ls = map.ToList();

            if (sorter == null)
                ls.Sort((x, y) => x.Value.Count.CompareTo(y.Value.Count));
            else
                ls.Sort(sorter);

            return ls;
        }
        public static T[] Reverse0<T>(this T[] source)
        {
            Array.Reverse(source);

            return source;
        }
        public static bool Equel<T>(this T[] source,T[] other, Func<T,T,bool> compare )
        {
            if (source.Length != other.Length)
                return false;

            for (int i = 0; i < source.Length; i++)
            {
                if (!compare(source[i], other[i]))
                    return false;
            }

            return true;
        }

        public static bool Equel<T>(this List<T> source, List<T> other, Func<T, T, bool> compare)
        {
            if (source.Count != other.Count)
                return false;

            for (int i = 0; i < source.Count; i++)
            {
                if (!compare(source[i], other[i]))
                    return false;
            }

            return true;
        }

        public static List<T> Sort1<T>(this List<T> source, Comparison<T> compare)
        {
            source.Sort(compare);

            return source;
        }



        // can not  control  the lock
        //public static ConcurrentDictionary<TKey,TSource> ToConcurrentDictionary<TSource,TKey>(this IEnumerable<TSource> source,Func<TSource,TKey>keySelector )
        //{
        //    var map = new ConcurrentDictionary<TKey, TSource>();

        //    foreach (var item in source)
        //    {
        //        map.TryAdd(keySelector(item), item);
        //    }

        //    return map;
        //}

        //public static ConcurrentQueue<TSource> ToConcurrentQueue<TSource>(this IEnumerable<TSource> source)
        //{
        //    if (source is ConcurrentQueue<TSource> queue)
        //        return queue;


        //}

    }
}
