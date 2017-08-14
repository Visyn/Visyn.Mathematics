namespace Visyn.Mathematics.Rand
{
    public class RandomByte : IRandom<byte>
    {
        private readonly IRandom Rng;

        public RandomByte(IRandom rng)
        {
            Rng = rng;
        }

        #region Implementation of IRandom<byte>

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        byte IRandom<byte>.Inclusive(byte min, byte max) => (byte)Inclusive(Rng, min, max);

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        byte IRandom<byte>.Exclusive(byte min, byte max) => (byte)Exclusive(Rng, min, max);

        #endregion

        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public static byte Exclusive(IRandom rng, byte min, byte max) => (byte)rng.Exclusive((int)min, (int)max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public static byte Inclusive(IRandom rng, byte min, byte max) => (byte)rng.Inclusive((int)min, (int)max);
    }
    
}