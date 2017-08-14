using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Visyn.Mathematics.Comparison;
using Visyn.Public.Reflection;

//using Visyn.Core.Comparison;
//using Visyn.Core.Reflection;

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
