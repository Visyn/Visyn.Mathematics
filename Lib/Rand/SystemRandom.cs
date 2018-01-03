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
using System.Threading;
using Visyn.JetBrains;

namespace Visyn.Mathematics.Rand
{
    /// <summary>
    /// Threadsave wrapper for System.Random implementing IRandom interface
    /// </summary>
    public class SystemRandom : RandomBase, IRandom
    {
        #region Overrides of RandomBase

        protected override IRandom Rng => this;

        #endregion
        /// <summary>
        /// The rng _seed
        /// </summary>
        private static int _seed = Environment.TickCount;

        /// <summary>
        /// Create threadsafe System.Random instance
        /// </summary>
        private static readonly ThreadLocal<Random> Random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        #region Implementation of IRandom

        /// <summary>
        /// ReSeed random number generator using new seed
        /// </summary>
        /// <param name="seed">Array of ulong to re-initialize</param>
        public void ReSeed(ulong[] seed)
        {
            Random.Value = new Random((int)seed[0]);
        }

        /// <summary>
        /// Return a random integer in the range [min,max]
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Inclusive([NotNull]int min,[NotNull] int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException($"Min must be < Max");
            // min is inclusive, max is exclusive
            return Random.Value.Next(min, max+1);
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
            if (max - min < 2) throw new ArgumentOutOfRangeException($"Max-min must be > 2");
            // System.Random.Next(min,max) return random number in range [0,1)
            // min is inclusive, max is exclusive
            return Random.Value.Next(min+1, max);
        }

        /// <summary>
        /// Return a random double in the range [min,max]
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns>System.Double.</returns>
        public double Inclusive(double min, double max)
        {
            // System.Random.NextDouble(min,max) return random number in range [0,1)
            // min is inclusive, max is exclusive
            // The actual upper bound of the random number returned by this method is 0.99999999999999978
            // https://msdn.microsoft.com/en-us/library/system.random.nextdouble(v=vs.110).aspx
            return Random.Value.NextDouble()* (1.0/0.99999999999999978) * (max - min) + min;
        }


        /// <summary>
        /// Return a random double in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns>System.Double.</returns>
        public double Exclusive(double min, double max)
        {
            // System.Random.NextDouble(min,max) return random number in range [0,1)
            // min is inclusive, max is exclusive
            // The actual upper bound of the random number returned by this method is 0.99999999999999978
            // https://msdn.microsoft.com/en-us/library/system.random.nextdouble(v=vs.110).aspx

            var random1 = Random.Value.NextDouble();            // random1 is in range [0,1]
            var random2 = Random.Value.NextDouble()*-1.0 + 1.0; // random2 is in range (0,1]
            // random1 + random2 is in range (0,2), divide by 2 (multiply by 0.5) for range (0,1)
            return (random1 + random2 * (max - min)*(0.5)) + min;
        }

        #endregion
    }
}
