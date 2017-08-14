using System;

namespace Visyn.Mathematics.Rand
{
    public class RandomDecimal : IRandom<Decimal>
    {
        private readonly IRandom Rng;

        public RandomDecimal(IRandom rng)
        {
            Rng = rng;
        }

        #region Implementation of IRandom<Decimal>

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        Decimal IRandom<Decimal>.Inclusive(Decimal min, Decimal max) => Inclusive(Rng, min, max);

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        Decimal IRandom<Decimal>.Exclusive(Decimal min, Decimal max) => Exclusive(Rng, min, max);

        #endregion

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public static Decimal Exclusive(IRandom rng, Decimal min, Decimal max) => (Decimal)rng.Exclusive((double)min, (double)max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static Decimal Inclusive(IRandom rng, Decimal min, Decimal max) => (Decimal)rng.Inclusive((double)min, (double)max);
    }
    
}