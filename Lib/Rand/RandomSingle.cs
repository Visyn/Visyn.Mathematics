using System;

namespace Visyn.Mathematics.Rand
{
    public class RandomSingle : IRandom<Single>
    {
        private readonly IRandom Rng;

        public RandomSingle(IRandom rng)
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
        float IRandom<float>.Inclusive(float min, float max) => Inclusive(Rng, min, max);

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        float IRandom<float>.Exclusive(float min, float max) => Exclusive(Rng, min, max);

        #endregion

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public static float Exclusive(IRandom rng, float min, float max) => (float)rng.Exclusive((double)min,(double) max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static float Inclusive(IRandom rng, float min, float max) => (float)rng.Exclusive((double)min, (double)max);
    }
    
}