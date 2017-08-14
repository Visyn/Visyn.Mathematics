using System;

namespace Visyn.Mathematics.Rand
{
    public class RandomInt16 : IRandom<Int16>
    {
        private readonly IRandom Rng;

        public RandomInt16(IRandom rng)
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
        short IRandom<short>.Inclusive(short min, short max) => Inclusive(Rng, min, max);

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        short IRandom<short>.Exclusive(short min, short max) => Exclusive(Rng, min, max);

        #endregion

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public static short Exclusive(IRandom rng, short min, short max) => (short)rng.Exclusive((double)min, (double)max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static short Inclusive(IRandom rng, short min, short max) => (short)rng.Inclusive((double)min, (double)max);
    }
    
}