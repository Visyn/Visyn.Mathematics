using System;

namespace Visyn.Mathematics
{
    public interface IRange<T> where T : IComparable<T>
    {
        /// <summary>Minimum value of the range.</summary>
        T Minimum { get; }

        /// <summary>Maximum value of the range.</summary>
        T Maximum { get; }

        ValueRange<T> ToValueRange();
        bool ContainsValue(T value);
    }

    public class ValueRange<T> : IRange<T> where T : IComparable<T>
    {

        #region Implementation of IRange<T>

        /// <summary>Minimum value of the range.</summary>
        public T Minimum { get; }

        /// <summary>Maximum value of the range.</summary>
        public T Maximum { get; }

        public ValueRange<T> ToValueRange() => this;
        public bool ContainsValue(T value) => (Minimum.CompareTo(value) <= 0) && (Maximum.CompareTo(value) <= 0);

        #endregion

        public ValueRange(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }
}