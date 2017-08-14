using System;

namespace Visyn.Mathematics.Rand
{
    public class RandomInt64 : IRandom<Int64>
    {
        private readonly IRandom Rng;

        public RandomInt64(IRandom rng)
        {
            Rng = rng;
        }

        #region Implementation of IRandom<short>

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        Int64 IRandom<Int64>.Inclusive(Int64 min, Int64 max)
        {
            return Inclusive(Rng, min, max);
        }

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        Int64 IRandom<Int64>.Exclusive(Int64 min, Int64 max)
        {
            return Exclusive(Rng, min, max);
        }

        #endregion

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public static Int64 Exclusive(IRandom rng, Int64 min, Int64 max)
        {
            if(min > Int32.MinValue && max < Int32.MaxValue) return Convert.ToInt64(rng.Exclusive(Convert.ToInt32(min), Convert.ToInt32(max)));

            return Convert.ToInt64(rng.Exclusive(Convert.ToDecimal(min), Convert.ToDecimal(max)));

        }
        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static long Inclusive(IRandom rng, Int64 min, Int64 max)
        {
            if (min > Int32.MinValue && max < Int32.MaxValue) return Convert.ToInt64(rng.Inclusive(Convert.ToInt32(min), Convert.ToInt32(max)));
            return Convert.ToInt64(rng.Inclusive(Convert.ToDecimal(min), Convert.ToDecimal(max)));
        }
    }
    
}