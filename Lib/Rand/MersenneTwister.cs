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
    /// <summary>
    /// Class MersenneTwister.  Wrapper class for MT19937.cs (ported from c)
    /// http://www.math.keio.ac.jp/matumoto/emt.html
    /// </summary>
    public class MersenneTwister : RandomBase, IRandom
    {
        #region Overrides of RandomBase

        protected override IRandom Rng => this;

        #endregion
        /// <summary>
        /// The MT19937 instance
        /// </summary>
        private readonly MT19937 _mt19937;
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister"/> class.
        /// </summary>
        public MersenneTwister()
        {
            _mt19937 = new MT19937();
        }

        #region Implementation of IRandom

        /// <summary>
        /// ReSeed random number generator using new seed
        /// </summary>
        /// <param name="seed">Array of ulong to re-initialize</param>
        public void ReSeed(ulong[] seed)
        {
            _mt19937.init_by_array(seed,(ulong)seed.Length);
        }

        /// <summary>
        /// Return a random integer in the range [min,max]
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Inclusive(int min, int max)
        {
            if(min >= max) throw new ArgumentOutOfRangeException($"Min must be < Max");
            return _mt19937.RandomRange(min, max);
        }

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Exclusive(int min, int max)
        {
            if(max-min <=2) throw new ArgumentOutOfRangeException($"Max-min must be > 2");
            return _mt19937.RandomRange(min+1, max-1);
        }


        /// <summary>
        /// Return a random double in the range [min,max]
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns>System.Double.</returns>
        public double Inclusive(double min, double max)
        {
            var rand = _mt19937.genrand_real1();    // range [0,1]
            if(min < max) return rand*(max - min) + min;
            return rand * (min - max) + max;
        }

        /// <summary>
        /// Return a random double in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns>System.Double.</returns>
        public double Exclusive(double min, double max)
        {
            var rand = _mt19937.genrand_real3();    // range (0,1)
            if (min < max) return rand * (max - min) + min;
            return rand * (min - max) + max;
        }

        #endregion
    }
}
