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
using Visyn.Mathematics.Redzen.Numerics;

namespace Visyn.Mathematics.Rand
{
    public class FastRng : RandomBase, IRandom
    {
        private readonly XorShiftRandom _fastRandom = new XorShiftRandom(Environment.TickCount);
        #region Overrides of RandomBase

        protected override IRandom Rng => this;

        #endregion
        #region Implementation of IRandom

        /// <summary>
        /// ReSeed random number generator using new seed
        /// </summary>
        /// <param name="seed">Array of ulong to re-initialize</param>
        public void ReSeed(ulong[] seed)
        {
            _fastRandom.Reinitialise((int)seed[0]);
        }

        /// <summary>
        /// Return a random integer in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public int Inclusive(int min, int max)
        {
            if (max < int.MaxValue) max++;
            return _fastRandom.Next(min, max);
        }

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public int Exclusive(int min, int max)
        {
            return _fastRandom.Next(min + 1, max);
        }

        /// <summary>
        /// Return a random double in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public double Inclusive(double min, double max)
        {
            if (min >= max) throw new ArgumentOutOfRangeException($"Min [{min}] must be < Max [{max}]!");
            return _fastRandom.NextDoubleNonZero() * (max+double.Epsilon - min) + min;


            //if (min >= max) throw new ArgumentException($"Min [{min}] must be < Max [{max}]!");
            //var random1 = _fastRandom.NextDouble();            // random1 is in range [0,1)
            //var random2 = _fastRandom.NextDouble() * -1.0 + 1.0; // random2 is in range (0,1]
            //// random1 + random2 is in range (0,2), divide by 2 (multiply by 0.5) for range (0,1)
            //return (random1 + random2) * (max - min) * (0.5) + min;
        }

        /// <summary>
        /// Return a random double in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public double Exclusive(double min, double max)
        {
            if (min >= max) throw new ArgumentOutOfRangeException($"Min [{min}] must be < Max [{max}]!");
            return _fastRandom.NextDoubleNonZero()*(max-min) + min;
        }

        //public decimal Inclusive(decimal min, decimal max)
        //{
        //    return (decimal)Inclusive((double) min, (double) max);
        //}

        //public decimal Exclusive(decimal min, decimal max)
        //{
        //    if (min >= max) throw new ArgumentOutOfRangeException($"Min [{min}] must be < Max [{max}]!");
        //    return (decimal)(_fastRandom.NextDoubleNonZero() * (double)(max - min)) + min;
        //}



        //public byte Exclusive(byte min, byte max)
        //{
        //    return (byte)_fastRandom.Next(min, max);
        //}

        //public byte Inclusive(byte min, byte max)
        //{
        //    if (max < byte.MaxValue) max++;
        //    return (byte)_fastRandom.Next(min, max);
        //}

        #endregion


    }
}
