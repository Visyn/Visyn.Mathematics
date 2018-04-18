#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
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
using System.Linq;

namespace Visyn.Mathematics
{
    /// <summary>
    /// Class RollingStatistics.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Visyn.Mathematics.IStatistics" />
    public class RollingStatistics<T> : IStatistics where T : IComparable, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Gets the mean.
        /// </summary>
        /// <value>The mean.</value>
        public double Mean { get; private set; } = 0;
        /// <summary>
        /// Gets the variance.
        /// </summary>
        /// <value>The variance.</value>
        public double Variance { get; private set; } = double.NaN;
        /// <summary>
        /// Gets the standard deviation.
        /// </summary>
        /// <value>The standard deviation.</value>
        public double StandardDeviation => Math.Sqrt(Variance);
        /// <summary>
        /// Gets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public double Min { get; private set; } = double.MaxValue;
        /// <summary>
        /// Gets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public double Max { get; private set; } = double.MinValue;
        /// <summary>
        /// Gets the size of the sample.
        /// </summary>
        /// <value>The size of the sample.</value>
        public int SampleSize { get; private set; } = 0;
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count => SampleSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingStatistics{T}"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public RollingStatistics(IEnumerable<T> values) : this(values?.ToDoubles<int>().ToList())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingStatistics{T}"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <exception cref="System.NullReferenceException">T</exception>
        public RollingStatistics(IList<double> values)
        {
            if (values == null)
                throw new NullReferenceException($"{nameof(RollingStatistics<T>)}({nameof(values)}) can not be null!");
            Add(values);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingStatistics{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public RollingStatistics(double value)
        {
            Add(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingStatistics{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public RollingStatistics(T value)
        {
            Add(Convert.ToDouble(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RollingStatistics{T}"/> class.
        /// </summary>
        public RollingStatistics()
        {
        }


        /// <summary>
        /// The m2
        /// </summary>
        private double M2 = 0;

        /// <summary>
        /// Adds the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void Add(IEnumerable<double> values)
        {
            foreach (var x in values)
            {
                SampleSize += 1;
                var delta = (double) x - Mean;
                Mean += delta / SampleSize;
                var delta2 = (double) x - Mean;
                M2 += delta * delta2;

                if (x < Min) Min = x;
                if (x > Max) Max = x;
            }
            Variance = SampleSize < 2 ? double.NaN : M2 / (SampleSize - 1);
        }


        /// <summary>
        /// Adds the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        public void Add(double x)
        {
            SampleSize += 1;
            var delta = (double) x - Mean;
            Mean += delta / SampleSize;
            var delta2 = (double) x - Mean;
            M2 += delta * delta2;

            if (x < Min) Min = x;
            if (x > Max) Max = x;
            Variance = SampleSize < 2 ? double.NaN : M2 / (SampleSize - 1);
        }

        /// <summary>
        /// Adds the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void Add(IEnumerable<T> values)
        {
            Add(values?.ToDoubles<T>());
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(T value)
        {
            Add(Convert.ToDouble(value));
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            M2 = 0;
            Mean = 0;
            Variance  = double.NaN;
            Min = double.MaxValue;
            Max  = double.MinValue;
            SampleSize  = 0;
        }
    }
}