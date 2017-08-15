using System;
using System.Collections.Generic;
using System.Linq;
using Visyn.Io;

namespace Visyn.Mathematics.Geometry
{
    /// <summary>The Range class.</summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    public class Range<T> : IRange<T>, IDelimitedString, IDelimitedData where T : IComparable<T>
    {
        /// <summary>Minimum value of the range.</summary>
        public T Minimum { get; }

        /// <summary>Maximum value of the range.</summary>
        public T Maximum { get; }

        public ValueRange<T> ToValueRange() => new ValueRange<T>(Minimum, Maximum);

        private readonly Func<T, T, int> Comparer;

        /// <summary>
        /// Create range class
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public Range(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
            if(!IsValid()) throw new ArgumentOutOfRangeException($"Range error!  {nameof(minimum)} must be less than or equal {nameof(maximum)}.  {minimum}>{maximum}");
        }

        /// <summary>
        /// Create range class
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public Range(T minimum, T maximum, Func<T, T, int> comparer)
        {
            Minimum = minimum;
            Maximum = maximum;
            Comparer = comparer;
            if (!IsValid()) throw new ArgumentOutOfRangeException($"Range error!  {nameof(minimum)} must be less than or equal {nameof(maximum)}.  {minimum}>{maximum}");
        }

        public Range(IEnumerable<T> items, Func<T, T, int> comparer=null)
        {
            Comparer = comparer;

            T maximum;
            var minimum = maximum = items.First();

            foreach (var item in items)
            {
                if (CompareTo(minimum, item) > 0) minimum = item;
                if (CompareTo(item, maximum) > 0) maximum = item;
            }
            Minimum = minimum;
            Maximum = maximum;
        }

        public List<T> ToList() => new List<T>(new [] {Minimum,Maximum});

        /// <summary>Determines if the range is valid.</summary>
        /// <returns>True if range is valid, else false</returns>
        public bool IsValid() => CompareTo(Minimum, Maximum) <= 0;

        /// <summary>Determines if the provided value is inside the range.</summary>
        /// <param name="value">The value to test</param>
        /// <returns>True if the value if in range [Minimum,Maximum], else false</returns>
        public bool ContainsValue(T value) => (CompareTo(Minimum, value) <= 0) && (CompareTo(value, Maximum) <= 0);

        /// <summary>Determines if this Range is inside the bounds of another range.</summary>
        /// <param name="Range">The parent range to test on</param>
        /// <returns>True if range is inclusive, else false</returns>
        public bool IsInsideRange(Range<T> range) => IsValid() && range.IsValid() && range.ContainsValue(Minimum) && range.ContainsValue(Maximum);

        /// <summary>Determines if another range is inside the bounds of this range.</summary>
        /// <param name="Range">The child range to test</param>
        /// <returns>True if range is inside, else false</returns>
        public bool ContainsRange(Range<T> range) => IsValid() && range.IsValid() && ContainsValue(range.Minimum) && ContainsValue(range.Maximum);


        /// <summary>Compares the current object with another object of the same type.</summary>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />. </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(T first, T second) => Comparer?.Invoke(first, second) ?? first.CompareTo(second);

        /// <summary>Presents the Range in readable format.</summary>
        /// <returns>String representation of the Range</returns>
        public override string ToString() => $"[{Minimum} - {Maximum}]";

        #region Implementation of IDelimitedString

        public string ToDelimitedString(string delimiter) => string.Join(delimiter,ToStringArray());

        public string DelimitedHeader(string delimiter) => string.Join(delimiter, ToHeaderArray());

        #endregion

        #region Implementation of IDelimitedData

        public IEnumerable<string> ToStringArray() => new[] {Minimum.ToString(), Maximum.ToString()};

        public IEnumerable<string> ToHeaderArray() => new[] {nameof(Minimum), nameof(Maximum)};

        #endregion

    }
}
