using System;

namespace Visyn.Mathematics.Rand
{
    public class RandomUInt32 : IRandom<UInt32>
    {
        private readonly IRandom Rng;

        public RandomUInt32(IRandom rng)
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
        UInt32 IRandom<UInt32>.Inclusive(UInt32 min, UInt32 max) => Inclusive(Rng, min, max);

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        UInt32 IRandom<UInt32>.Exclusive(UInt32 min, UInt32 max) => Exclusive(Rng, min, max);

        #endregion

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public static UInt32 Exclusive(IRandom rng, UInt32 min, UInt32 max)
        {
            if (max < Int32.MaxValue) return (UInt32)rng.Exclusive((int)min, (int)max);
            // reduce min & max (int.MinValue is a negative number) -> calculate random Int32 -> increase result by int.MinVale
            return (UInt32)(rng.Exclusive((int)min+int.MinValue, (int)max + int.MinValue) - int.MinValue);
            throw new ArgumentOutOfRangeException("Max > Int32.MaxValue not supported at this time...");
        }

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static UInt32 Inclusive(IRandom rng, UInt32 min, UInt32 max)
        {
            if(max < Int32.MaxValue) return (UInt32)rng.Inclusive((int)min, (int)max);
            throw new  ArgumentOutOfRangeException("Max > Int32.MaxValue not supported at this time...");
        }
    }
    
}