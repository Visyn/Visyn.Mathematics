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
