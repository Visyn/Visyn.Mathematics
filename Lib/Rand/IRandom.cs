using System;

namespace Visyn.Mathematics.Rand
{
    public interface IRandom<T>
    {
        /// <summary>
        /// Return a random integer in the range (min,max)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        T Exclusive(T min, T max);

        /// <summary>
        /// Return a random value in the range [min,max] 
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        T Inclusive(T min, T max);
    }

    /// <summary>
    /// Interface IRandom for random number generators
    /// </summary>
    public interface IRandom : IRandom<int>, IRandom<double>, IRandom<Int16>, IRandom<Int64>, IRandom<UInt16>, IRandom<UInt32>, IRandom<UInt64>, IRandom<Single>, IRandom<Decimal>, IRandom<byte>
    {
        /// <summary>
        /// ReSeed random number generator using new seed
        /// </summary>
        /// <param name="seed">Array of ulong to re-initialize</param>
        void ReSeed(ulong[] seed);
    }

    public interface IRandomGaussian : IRandom
    {
        /// <summary>
        /// Return a random number with gaussian distribution specified
        /// by mean and sigma
        /// </summary>
        /// <param name="mean">Mean of distribution</param>
        /// <param name="sigma">Standard deviation of distribution</param>
        /// <returns></returns>
        double Gaussian(double mean, double sigma);
    }
}