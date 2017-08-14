﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Visyn.Mathematics
{
    public class Statistics : IStatistics
    {
        public string Name { get; }
        #region Implementation of IStatistics

        public double Mean { get; }
        public double Variance { get; }
        public double StandardDeviation { get; }
        public double Min { get; }
        public double Max { get; }
        public int SampleSize { get; }
        #endregion

        [Obsolete("For Deserialization only!")]
        public Statistics() { }
    

        public Statistics(string name, double mean, double variance, int sampleSize)
            : this(name, mean, variance, Math.Sqrt(variance), double.MaxValue, double.MinValue, sampleSize) { }

        public Statistics(string name, double mean, double variance, double standardDeviation, int sampleSize) 
            : this(name,mean,variance,standardDeviation,double.MaxValue,double.MinValue,sampleSize) { }

        public Statistics(string name, double mean, double variance, double standardDeviation, double min, double max, int sampleSize)
        {
            Name = name;
            Mean = mean;
            Variance = variance;
            StandardDeviation = standardDeviation;
            SampleSize = sampleSize;
            Min = min;
            Max = max;
        }

        [Obsolete("Use static method for efficience: Statistics.Calculate(..)")]
        public Statistics(string name, IEnumerable<double>  values) : this(name,Calculate(values))
        {
        }

        public Statistics(string name, IStatistics stats)
        {
            Name = name;
            Mean = stats.Mean;
            Variance = stats.Variance;
            StandardDeviation = stats.StandardDeviation;
            SampleSize = stats.SampleSize;
            Min = stats.Min;
            Max = stats.Max;
        }

        #region Overrides of Object

        public override string ToString() => $"{Name} [{SampleSize}] μ={Mean} σ={StandardDeviation} [{Min},{Max}]";

        #endregion

        #region static methods
        public static Statistics Calculate<T>( IEnumerable<T> values) where T : IComparable, IComparable<T>, IEquatable<T> => values.Statistics<T>();

        public static Statistics Calculate<T>( IEnumerable<T> values, Func<T, double> convert) => values.Select(convert).Statistics<double>();

        public static Statistics Calculate<T>(string name, IEnumerable<T> values) where T : IComparable, IComparable<T>, IEquatable<T> => values.Statistics<T>(name);

        public static Statistics Calculate<T>(string name, IEnumerable<T> values, Func<T,double> convert ) => values.Select(convert).Statistics<double>(name);

        public static Dictionary<string, Statistics> ForPropertyValues<T>(IEnumerable<T> values) where T : IComparable, IComparable<T>, IEquatable<T> 
            => values.StatisticsFromPropertyValues();

        public static Dictionary<string, Statistics> ForPropertyValues<T>(string name, IEnumerable<T> values) where T : IComparable, IComparable<T>, IEquatable<T> 
            => values.StatisticsFromPropertyValues(name);
        #endregion
    }
}