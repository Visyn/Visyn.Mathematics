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