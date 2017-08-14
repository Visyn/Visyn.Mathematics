using System;
using System.Collections.Generic;

namespace Visyn.Mathematics.Collection
{
    public static class CollectionMin
    {
        public static DateTime MinDateTime<TKey>(this IList<IReadOnlyDictionary<TKey, object>> collection, TKey columnKey)
        {
            var min = DateTime.MaxValue;
            foreach(var dict in collection )
            {
                var time = dict.TryGet(columnKey, DateTime.MaxValue);
                if (time < min) min = time;
            }
            return min;
        }

        private static TOut TryGet<TKey, TValue, TOut>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key, TOut defaultValue) where TOut : TValue
        {
            if (dict?.ContainsKey(key) != true) return defaultValue;
            var obj = dict[key];
            if (obj is TOut) return (TOut)obj;
            return defaultValue;
        }
    }
}