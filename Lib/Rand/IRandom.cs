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

namespace Visyn.Mathematics.Rand
{
    public interface IRandom<T>
    {
        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        T Exclusive(T min, T max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        T Inclusive(T min, T max);
    }

    /// <summary>
    /// Interface IRandom for random number generators
    /// </summary>
    public interface IRandom : IRandom<int>, IRandom<double>, IRandom<Int16>, IRandom<Int64>, IRandom<UInt16>, IRandom<UInt32>, IRandom<UInt64>, IRandom<Single>, IRandom<Decimal>, IRandom<byte>
    {
        /// <summary>
        /// ReSeed random number generator using new seed
        /// </summary>
        /// <param name="seed">Array of ulong to re-initialize</param>
        void ReSeed(ulong[] seed);
    }

    public interface IRandomGaussian : IRandom
    {
        /// <summary>
        /// Return a random number with gaussian distribution specified
        /// by mean and sigma
        /// </summary>
        /// <param name="mean">Mean of distribution</param>
        /// <param name="sigma">Standard deviation of distribution</param>
        /// <returns></returns>
        double Gaussian(double mean, double sigma);
    }
}
