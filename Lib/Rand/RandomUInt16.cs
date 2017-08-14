using System;

namespace Visyn.Mathematics.Rand
{
    public class RandomUInt16 : IRandom<UInt16>
    {
        private readonly IRandom Rng;

        public RandomUInt16(IRandom rng)
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
        UInt16 IRandom<UInt16>.Inclusive(UInt16 min, UInt16 max)
        {
            return Inclusive(Rng, min, max);
        }

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        UInt16 IRandom<UInt16>.Exclusive(UInt16 min, UInt16 max)
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
        public static UInt16 Exclusive(IRandom rng, UInt16 min, UInt16 max)
        {
            return Convert.ToUInt16(rng.Exclusive(Convert.ToInt32(min), Convert.ToInt32(max)));
        }
        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static UInt16 Inclusive(IRandom rng, UInt16 min, UInt16 max)
        {
            return Convert.ToUInt16(rng.Exclusive(Convert.ToInt32(min), Convert.ToInt32(max)));
        }
    }
    
}