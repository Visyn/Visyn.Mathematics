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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Visyn.Mathematics.Comparison;

namespace Visyn.Mathematics
{
    /// <summary>
    /// Class MathExtensions.
    /// </summary>
    public static partial class MathExtensions
    {
        public static double ToDouble<T>(this object number) where T : struct, IComparable, IComparable<T>, IEquatable<T> => NumberConverter.Instance.ToDouble(number);

        public static double ToDouble<T>(this T number, Func<T,double> convert) => convert(number);

        public static IEnumerable<double> ToDoubles<T>(this IEnumerable collection) where T : IComparable, IComparable<T>, IEquatable<T>
        {
            var convert = NumberConverter.Instance;
            foreach (var value in collection)  yield return convert.ToDouble(value);
        }

        public static IEnumerable<double> ToDoubles<T>(this IEnumerable<T> collection, Func<T, double> convert)
        {
            if (convert == null) throw new NullReferenceException( $"{nameof(ToDoubles)}<{typeof(T).Name}> {nameof(convert)} function is null!");
            return collection.Select(convert);
        }
       

        /// <summary>
        /// Calculates standard deviation on a list of doubles
        /// </summary>
        /// <param name="numbers">The doubles to calculate mean of.</param>
        /// <returns>Standard deviation value as System.Double.</returns>
        public static double StdDev(this IEnumerable<double> numbers)
        {
            double ave;
            return Math.Sqrt(Variance(numbers, out ave));
        }

        /// <summary>
        /// Calculates standard deviation on an enumeration of values
        /// </summary>
        /// <typeparam name="T">Type of enumeration, T must implement IConvertible</typeparam>
        /// <param name="numbers">The numbers to calculate mean of.</param>
        /// <returns>Standard deviation as System.Double.</returns>
        public static double StdDev<T>(this IEnumerable<T> numbers) where T : IComparable, IComparable<T>, IEquatable<T>
        {
            double ave;
            var doubles = numbers.ToDoubles<T>();
            return Math.Sqrt(Variance(doubles,out ave));
        }
    }
}
