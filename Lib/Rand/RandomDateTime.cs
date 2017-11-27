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
using System.Threading.Tasks;

namespace Visyn.Mathematics.Rand
{
    public class RandomDateTime : IRandom<DateTime>
    {
        /// <summary>
        /// The private rng.
        /// </summary>
        private static readonly IRandom<long> Rng = Rng<FastRng>.ThreadSafeRandom<long>(null,Task.CurrentId != null ? Task.CurrentId.Value : 0);

        #region Implementation of IRandom<DateTime>

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        DateTime IRandom<DateTime>.Exclusive(DateTime min, DateTime max)
        {
            return Exclusive(min, max);
        }

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        DateTime IRandom<DateTime>.Inclusive(DateTime min, DateTime max)
        {
            return Inclusive(min, max);
        }

        #endregion

        /// <summary>
        /// Return a random DateTime in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns>Return a random DateTime in the range (min,max)</returns>
        public static DateTime Exclusive(DateTime min, DateTime max)
        {
            var span = max - min;
            return new DateTime(min.Ticks + (long)Rng.Exclusive(0, span.Ticks));
        }

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns>Return a random DateTime in the range [min,max]</returns>
        public static DateTime Inclusive(DateTime min, DateTime max)
        {
            var span = max - min;
            return new DateTime(min.Ticks + (long)Rng.Inclusive(0, span.Ticks));
        }
    }
}
