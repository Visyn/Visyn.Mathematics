using System;
using Visyn.Mathematics.Redzen.Numerics;

namespace Visyn.Mathematics.Rand
{
    public class Ziggurat : RandomBase, IRandomGaussian
    {
        private ZigguratGaussianSampler _ziggurt = new ZigguratGaussianSampler(Environment.TickCount);
        #region Overrides of RandomBase

        protected override IRandom Rng => this;

        #endregion

        #region Implementation of IRandom

        /// <summary>
        /// ReSeed random number generator using new seed
        /// </summary>
        /// <param name="seed">Array of ulong to re-initialize</param>
        public void ReSeed(ulong[] seed)
        {
            _ziggurt = new ZigguratGaussianSampler((int)seed[0]);
        }

        /// <summary>
        /// Return a gaussian distributed random integer in the range [min,max]
        /// Note: standard deviation will be (max-min/6.0)
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public int Inclusive(int min, int max)
        {
            var range = max - min;
            var mean = (max + min) / 2.0;
            var sigma = range / 3.0;
            var gaussian = (int)_ziggurt.NextSample(mean, sigma);
            if (gaussian > max) return max;
            if (gaussian < min) return min;
            return gaussian;
        }

        /// <summary>
        /// Return a gaussian distributed random integer in the range (min,max)
        /// Note: standard deviation will be (max-min/6.0)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public int Exclusive(int min, int max)
        {
            var range = max - min;
            var mean = (max + min) / 2.0;
            var sigma = range / 3.0;
            var gaussian = (int)_ziggurt.NextSample(mean, sigma);
            if (gaussian > max) return max-1;
            if (gaussian < min) return min+1;
            return gaussian;
        }

        /// <summary>
        /// Return a gaussian distributed random double in the range [min,max]
        /// Note: standard deviation will be (max-min/6.0)
        /// </summary>
        /// <param name="min">Inclusive minimum of range</param>
        /// <param name="max">Inclusive maximum of range</param>
        /// <returns></returns>
        public double Inclusive(double min, double max)
        {
            var range = max - min;
            var mean = (max + min) / 2.0;
            var sigma = range / 3.0;
            var gaussian = _ziggurt.NextSample(mean, sigma);
            if (gaussian > max) return max;
            if (gaussian < min) return min;
            return gaussian;
        }

        /// <summary>
        /// Return a gaussian distributed random double in the range (min,max)
        /// Note: standard deviation will be (max-min/6.0)
        /// </summary>
        /// <param name="min">Exclusive minimum of range</param>
        /// <param name="max">Exclusive maximum of range</param>
        /// <returns></returns>
        public double Exclusive(double min, double max)
        {
            var range = max - min;
            var mean = (max + min)/2.0;
            var sigma = range/6.0;          // Range is defaulted to +/- 3-sigma   
            var gaussian = _ziggurt.NextSample(mean, sigma);
            // if result is beyond 3 sigma, wrap result
            if (gaussian >= max)
                gaussian = Math.Min(gaussian - sigma, max - sigma/10.0);
            if (gaussian <= min)
                gaussian = Math.Max(gaussian + sigma, min + sigma/10.0);
            return gaussian;
        }

        /// <summary>
        /// Return a random number with gaussian distribution specified
        /// by mean and sigma
        /// </summary>
        /// <param name="mean">Mean of distribution</param>
        /// <param name="sigma">Standard deviation of distribution</param>
        /// <returns></returns>
        public double Gaussian(double mean, double sigma)
        {
            return _ziggurt.NextSample(mean,sigma);
        }

        #endregion
    }
}