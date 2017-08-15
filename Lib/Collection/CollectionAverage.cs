using System;
using System.Collections;
using System.Collections.Generic;
using Visyn.Types.Time;

namespace Visyn.Mathematics.Collection
{
    public static class CollectionAverage
    {
        public static double Average<T>(this ICollection collection) where T : IComparable, IComparable<T>, IEquatable<T>
        {
            if (typeof(T).IsNumeric()) return collection.AverageDouble();
            //if (typeof(T) == typeof(DateTime)) return collection.AverageDateTime();
            throw new NotImplementedException($"Average not implemented for Type={typeof(T)}");
        }

        public static double AverageDouble(this ICollection collection)
        {
            var average = 0.0;
            var count = 0;
            foreach (var item in collection)
            {
                if (item == null) continue;
                if (item is double)
                {
                    average += (double)item;
                    count++;
                    continue;
                }
                average += Convert.ToDouble(item);
                count++;
            }
            if (count > 0) return average / count;
            return 0.0;
        }

        public static IDictionary<TKey, object> AverageDouble<TKey>(this ICollection<IDictionary<TKey, object>> list, Func<TKey, TKey> minKeyRenamer=null , Func<TKey, TKey> maxKeyRenamer=null) //where TValue : class , IComparable
        {
            IDictionary<TKey, double> min;
            IDictionary<TKey, double> max;
            var result = AverageDouble(list, out min, out max);
            if (minKeyRenamer != null)
            {
                foreach (var item in min)
                {
                    var rename = minKeyRenamer(item.Key);
                    if (rename != null) result.Add(rename,item.Value); 
                }
            }
            if (maxKeyRenamer != null)
            {
                foreach (var item in max)
                {
                    var rename = maxKeyRenamer(item.Key);
                    if (rename != null) result.Add(rename, item.Value);
                }
            }
            return result;
            ////var average = 0.0;
            ////int count = 0;
            //var ave = new Dictionary<TKey, double>();
            //var count = new Dictionary<TKey, int>();
            //var result = new Dictionary<TKey, object>();
            //foreach (var dict in list)
            //{
            //    foreach (var item in dict)
            //    {
            //        if (item.Value == null) continue;
            //        if (item.Value.IsNumeric())
            //        {
            //            var d = Convert.ToDouble(item);
            //            if (!count.ContainsKey(item.Key))
            //            {
            //                count.Add(item.Key, 1);
            //                ave.Add(item.Key, d);
            //            }
            //            else
            //            {
            //                count[item.Key]++;
            //                ave[item.Key] += d;
            //            }
            //        }
            //        else
            //        {
            //            if (!result.ContainsKey(item.Key)) result.Add(item.Key, item.Value);
            //        }
            //    }
            //}
            //foreach (var item in count)
            //{
            //    if (item.Value > 0) result.Add(item.Key, ave[item.Key] / count[item.Key]);
            //    else result.Add(item.Key, 0.0);
            //}
            //return result;
        }

        public static IDictionary<TKey, object> AverageDouble<TKey>(this ICollection<IDictionary<TKey,object>> list, out IDictionary<TKey,double> min, out IDictionary<TKey,double> max) //where TValue : class , IComparable
        {
            var ave = new Dictionary<TKey, double>();
            var count = new Dictionary<TKey, int>();
            var result = new Dictionary<TKey, object>();
            min = new Dictionary<TKey, double>();
            max = new Dictionary<TKey, double>();
            foreach (var dict in list)
            {
                foreach(var item in dict)
                {
                    if (item.Value == null) continue;
                    if (item.Value.IsNumeric())
                    {
                        var d = Convert.ToDouble(item.Value);
                        if(!count.ContainsKey(item.Key))
                        {
                            count.Add(item.Key,1);
                            ave.Add(item.Key,d);
                            min.Add(item.Key,d);
                            max.Add(item.Key, d);
                        }
                        else
                        {
                            count[item.Key]++;
                            ave[item.Key] += d;
                            if (d < (double)min[item.Key]) min[item.Key] = d;
                            if (d > (double)max[item.Key]) max[item.Key] = d;
                        }
                    }
                    else
                    {
                        if (!result.ContainsKey(item.Key))
                        {
                            result.Add(item.Key, item.Value);
                            //min.Add(item.Key,item.Value);
                            //max.Add(item.Key, item.Value);
                        }
                    }
                }
            }
            foreach(var item in count)
            {
                if(item.Value > 0) result.Add(item.Key,ave[item.Key]/count[item.Key]);
                else result.Add(item.Key,0.0);
            }
            return result;
        }


        public static DateTime AverageDateTime(this ICollection collection, bool skipNonDateTime = false)
        {
            var sum = 0.0;
            int count = 0;
            foreach (var item in collection)
            {
                if (item == null) continue;
                if (item is DateTime)
                {
                    sum += ((DateTime)item).MillisecondsSince1970();
                    count++;
                    continue;
                }
                if (skipNonDateTime) continue;
                throw new ArgumentException($"Average: Item is not Type {typeof(DateTime).Name}.  Actual type: {item.GetType().Name}");
            }
            if (count > 0)
            {
                var ave = sum / count;
                return ave.FromMillisecondsSince1970();
            }
            return new DateTime();
        }

        public static TimeSpan AverageTimeSpan(this ICollection collection, bool skipNonTimeSpan = false)
        {
            var sum = 0.0;
            int count = 0;
            foreach (var item in collection)
            {
                if (item == null) continue;
                if (item is TimeSpan)
                {
                    sum += ((TimeSpan)item).Ticks;//TotalMilliseconds;
                    count++;
                    continue;
                }
                if (skipNonTimeSpan) continue;
                throw new ArgumentException($"Average: Item is not Type {typeof(TimeSpan).Name}.  Actual type: {item.GetType().Name}");
            }
            if (count > 0)
            {
                var ave = sum / count;
                return TimeSpan.FromMilliseconds(ave);
            }
            return new TimeSpan();
        }
    }
}