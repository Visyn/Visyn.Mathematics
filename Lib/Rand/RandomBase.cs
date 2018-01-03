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

namespace Visyn.Mathematics.Rand
{
    public abstract class RandomBase : IRandom<Int16>, IRandom<Int64>, IRandom<UInt16>,IRandom<UInt32>,IRandom<UInt64>, IRandom<Single>, IRandom<decimal>, IRandom<byte>
    {
        protected abstract IRandom Rng { get; }


        #region Implementation of IRandom<short>

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public virtual short Exclusive(short min, short max) => RandomInt16.Exclusive(Rng,min,max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public virtual short Inclusive(short min, short max) => RandomInt16.Inclusive(Rng, min, max);

        #endregion

        #region Implementation of IRandom<uint>

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public virtual uint Exclusive(uint min, uint max) => RandomUInt32.Exclusive(Rng,min,max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public virtual uint Inclusive(uint min, uint max) => RandomUInt32.Inclusive(Rng,min,max);

        #endregion

        #region Implementation of IRandom<long>

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public virtual long Exclusive(long min, long max)
        {
            return RandomInt64.Exclusive(Rng,min,max);
        }

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public virtual long Inclusive(long min, long max)
        {
            return RandomInt64.Inclusive(Rng, min, max);
        }

        #endregion

        #region Implementation of IRandom<ushort>

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public virtual ushort Exclusive(ushort min, ushort max) => RandomUInt16.Exclusive(Rng,min,max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public virtual ushort Inclusive(ushort min, ushort max)
        {
            return RandomUInt16.Inclusive(Rng, min, max);
        }

        #endregion

        #region Implementation of IRandom<ulong>

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public virtual ulong Exclusive(ulong min, ulong max) => RandomUInt64.Exclusive(Rng, min, max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public virtual ulong Inclusive(ulong min, ulong max)
        {
            return RandomUInt64.Inclusive(Rng, min, max); ;
        }

        #endregion

        #region Implementation of IRandom<float>

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public virtual float Exclusive(float min, float max) => RandomSingle.Exclusive(Rng, min, max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public virtual float Inclusive(float min, float max) => RandomSingle.Inclusive(Rng, min, max);

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public decimal Exclusive(decimal min, decimal max) => RandomDecimal.Exclusive(Rng,min,max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns>random value in the range [min,max] </returns>
        public decimal Inclusive(decimal min, decimal max) => RandomDecimal.Inclusive(Rng, min, max);

        /// <summary>
        /// Return a random byte in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public byte Exclusive(byte min, byte max) => RandomByte.Exclusive(Rng,min,max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns>random value in the range [min,max] </returns>
        public byte Inclusive(byte min, byte max) => RandomByte.Inclusive(Rng, min, max);

        #endregion
    }
}
