using System;

namespace Visyn.Mathematics.Rand
{
    public class RandomUInt64 : IRandom<UInt64>
    {
        private readonly IRandom Rng;

        public RandomUInt64(IRandom rng)
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
        UInt64 IRandom<UInt64>.Inclusive(UInt64 min, UInt64 max)
        {
            return Inclusive(Rng, min, max);
        }

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        UInt64 IRandom<UInt64>.Exclusive(UInt64 min, UInt64 max)
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
        public static UInt64 Exclusive(IRandom rng, UInt64 min, UInt64 max)
        {
            if(max <= int.MaxValue) return Convert.ToUInt64(rng.Exclusive(Convert.ToInt32(min), Convert.ToInt32(max)));
            double range = max - min;
            var doubleResult = rng.Exclusive(0.0, range);
            return Convert.ToUInt64(doubleResult + (double)min);
        }

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static UInt64 Inclusive(IRandom rng, UInt64 min, UInt64 max)
        {
            if (max <= int.MaxValue) return Convert.ToUInt64(rng.Inclusive(Convert.ToInt32(min), Convert.ToInt32(max)));
            double range = max - min;
            var doubleResult = rng.Inclusive(0.0, range);
            return Convert.ToUInt64(doubleResult + (double)min);
        }
    }
    
}