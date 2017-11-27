#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Visyn.Mathematics.Comparison;
using Visyn.Reflection;


namespace Visyn.Mathematics
{
    public static class StatisticsExtensions //: IEqualsToleranced<>
    {
        public static bool Equals<T>(this T stats, T other, double tolerance) where T : IStatistics
        {
            if (!EqualsToleranced.Equals(stats.Mean, other.Mean, tolerance)) return false;
            if (!EqualsToleranced.Equals(stats.Variance, other.Variance, tolerance)) return false;
            if (!EqualsToleranced.Equals(stats.StandardDeviation, other.StandardDeviation, tolerance)) return false;
            if (!EqualsToleranced.Equals(stats.Min, other.Min, tolerance)) return false;
            if (!EqualsToleranced.Equals(stats.Max, other.Max, tolerance)) return false;
            if (!EqualsToleranced.Equals(stats.SampleSize, other.SampleSize, tolerance)) return false;
            return true;
        }

        public static void StatisticsSetValues<T>(this T stats, IStatistics other) where T : IStatistics
        {
            //stats.SetReadOnlyProperty<T>(nameof(IStatistics.Variance), other.Variance);
            var type = stats.GetType().GetTypeInfo();
            type.SetReadOnlyProperty(nameof(IStatistics.Mean),stats, other.Mean);
            type.SetReadOnlyProperty(nameof(IStatistics.Variance), stats, other.Variance);
            type.SetReadOnlyProperty(nameof(IStatistics.StandardDeviation), stats, other.StandardDeviation);
            type.SetReadOnlyProperty(nameof(IStatistics.Min), stats, other.Min);
            type.SetReadOnlyProperty(nameof(IStatistics.Max), stats, other.Max);
            type.SetReadOnlyProperty(nameof(IStatistics.SampleSize), stats, other.SampleSize);

            Debug.Assert(stats.Equals<IStatistics>(other, 1e-12));
        }

        [Obsolete("Use "+ nameof(StatisticsFromPropertyValues) +" instead...")]
        public static Dictionary<string, Statistics> FromPropertyValues<T>(IEnumerable<T> obj)
        {
            return StatisticsFromPropertyValues<T>(obj,null);
        }

        public static Dictionary<string, Statistics> StatisticsFromPropertyValues<T>(this IEnumerable<T> obj, string namePrefix=null)
        {
            var type = typeof(T).GetTypeInfo();
            var props = new Dictionary<string,PropertyInfo>();
            var values = new Dictionary<string, List<double>>();
            foreach (var property in type.DeclaredProperties)
            {
                if (property.PropertyType != typeof(double) && 
                    property.PropertyType != typeof(int) && 
                    property.PropertyType != typeof(bool) && 
                    property.PropertyType != typeof(DateTime) && 
                    property.PropertyType != typeof(TimeSpan)) continue;
                var name = string.IsNullOrEmpty(namePrefix) ? property.GetDescription() :$"{namePrefix} {property.GetDescription()}";
                props.Add(name,property);
                values.Add(name,new List<double>());
            }

            foreach(var item in obj)
            {
                foreach(var pi in props)
                {
                    var value = pi.Value.GetValue(item);
                    values[pi.Key].Add(value as double? ?? 
                        NumberConverter.Instance.ToDouble(value));
                }
            }
            var result = new Dictionary<string, Statistics>(props.Count);
            foreach(var item in values)
            {
                var stats = item.Value.Statistics(item.Key);
                result.Add(item.Key, stats);
            }
            return result;
        }

        public static T StatisticsForPropertyValues<T>(this IEnumerable<T> obj, string namePrefix = null)
        {
            Dictionary<string, Statistics> result;
            var props = StatisticsForPropertyInfos(obj, namePrefix, out result);

            T instance = Activator.CreateInstance<T>();
            foreach(var prop in props)
            {
                prop.Value.SetValue(instance, result[prop.Key]);
            }
            return instance;
        }
        public static TOut StatisticsForPropertyValues<TEnum,TOut>(this IEnumerable<TEnum> obj, string namePrefix = null)
        {
            Dictionary<string, Statistics> result;
            var enumProperties = StatisticsForPropertyInfos(obj, namePrefix, out result);
            //var outProperties = typeof(TOut).GetTypeInfo().DeclaredProperties;
            var outProperties = typeof(TOut).GetTypeInfo().PropertyDictionary();
            foreach(var prop in enumProperties)
            {

            }
            TOut instance = Activator.CreateInstance<TOut>();
            foreach (var prop in outProperties)
            {
                if (prop.Value.CanWrite)
                {
                    if (outProperties.ContainsKey(prop.Key))
                    {
                        prop.Value.SetValue(instance, result[prop.Key]);
                    }
                }
                //else
                //{
                //    if (outProperties.ContainsKey(prop.Key))
                //    {
                //        var outProp = outProperties[prop.Key];
                //        outProp.SetValue(instance, result[prop.Key]);
                //    }
                //}

            }
            return instance;
        }

        /// <summary>
        /// Statisticses for property infos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="namePrefix">The name prefix.</param>
        /// <param name="result">The result.</param>
        /// <returns>Dictionary&lt;System.String, PropertyInfo&gt;.</returns>
        private static Dictionary<string, PropertyInfo> StatisticsForPropertyInfos<T>(IEnumerable<T> obj, string namePrefix, out Dictionary<string, Statistics> result)
        {
            var type = typeof(T).GetTypeInfo();
   //         var props = type.PropertyDictionary(namePrefix);  //new Dictionary<string, PropertyInfo>());

   //         foreach (var property in type.DeclaredProperties)
   //         {
   //             if (property.PropertyType != typeof(double) &&
   //                 property.PropertyType != typeof(int) &&
   //                 property.PropertyType != typeof(bool) &&
   //                 property.PropertyType != typeof(DateTime) &&
   //                 property.PropertyType != typeof(TimeSpan)) continue;
   //             var name = string.IsNullOrEmpty(namePrefix)
   //                 ? property.GetDescription()
   //                 : $"{namePrefix} {property.GetDescription()}";
   //             props.Add(name, property);
   ////             values.Add(name, new List<double>());
   //         }
            var props = type.PropertyDictionary(namePrefix);
            var values = props.ToDictionary(prop => prop.Key, prop => new List<double>());
            foreach (var item in obj)
            {
                foreach (var pi in props)
                {
                    var value = pi.Value.GetValue(item);
                    values[pi.Key].Add(value as double? ??
                                       NumberConverter.Instance.ToDouble(value));
                }
            }
            result = new Dictionary<string, Statistics>(props.Count);
            foreach (var item in values)
            {
                var stats = item.Value.Statistics(item.Key);
                result.Add(item.Key, stats);
            }
            return props;
        }

        private static Dictionary<string,PropertyInfo> PropertyDictionary(this TypeInfo type, string namePrefix=null)
        {
            var props = new Dictionary<string, PropertyInfo>();
            //var values = new Dictionary<string, List<double>>();
            foreach (var property in type.DeclaredProperties)
            {
                if (property.PropertyType != typeof(double) &&
                    property.PropertyType != typeof(int) &&
                    property.PropertyType != typeof(bool) &&
                    property.PropertyType != typeof(DateTime) &&
                    property.PropertyType != typeof(TimeSpan)) continue;
                var name = string.IsNullOrEmpty(namePrefix)
                    ? property.GetDescription()
                    : $"{namePrefix} {property.GetDescription()}";
                props.Add(name, property);
              //  values.Add(name, new List<double>());
            }
            return props;
        }


        private static void SetReadOnlyProperty(this TypeInfo type,string propertyName, object obj, object value)
        {
            var meanProp = type.DeclaredFields.First((f) => f.Name.StartsWith($"<{propertyName}>"));
            meanProp.SetValue(obj, value);
        }

        private static void SetReadOnlyProperty<T>(this object obj, string propertyName,  object value)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var meanProp = typeInfo.DeclaredFields.First((f) => f.Name.StartsWith($"<{propertyName}>"));
            meanProp?.SetValue(obj, value);
        }
    }
}
