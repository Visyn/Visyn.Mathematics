/* ****************************************************************************
 * This file is part of the Redzen code library.
 *
 * Copyright 2015 Colin D. Green (colin.green1@gmail.com)
 *
 * This software is issued under the MIT License.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;

namespace Visyn.Mathematics.Redzen.Numerics
{

    /// <summary>
    /// Frequency distribution data from a distribution analysis of some data series.
    /// </summary>
    internal class FrequencyDistributionData
    {
        #region Constructor

        /// <summary>
        /// Construct with the provided frequency distribution data.
        /// </summary>
        /// <param name="min">The minimum value in the data series the distribution represents.</param>
        /// <param name="max">The maximum value in the data series the distribution represents.</param>
        /// <param name="increment">The range of a single category bucket.</param>
        /// <param name="frequencyArr">The array of category frequency counts.</param>
        public FrequencyDistributionData(double min, double max, double increment, int[] frequencyArr)
        {
            this.Min = min;
            this.Max = max;
            this.Increment = increment;
            this.FrequencyArray = frequencyArr;
        }

        #endregion


        #region Properties

        /// <summary>
        /// The minimum value in the data series the distribution represents.
        /// </summary>
        public double Min { get; }

        /// <summary>
        /// The maximum value in the data series the distribution represents.
        /// </summary>
        public double Max { get; }

        /// <summary>
        /// The range of a single category bucket.
        /// </summary>
        public double Increment { get; }

        /// <summary>
        /// The array of category frequency counts.
        /// </summary>
        public int[] FrequencyArray { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the index of the bucket that covers the specified x value. Throws an exception if x is 
        /// outside the range of represented by the distribution buckets.
        /// </summary>
        public int GetBucketIndex(double x)
        {
            if (x < this.Min || x > this.Max)
            {
                throw new RankException("x is outide the range represented by the distribution data.");
            }
            return (int)((x - this.Min) / this.Increment);
        }

        #endregion

        #region Static Methods
        /// <summary>
        /// Calculate a frequency distribution for the provided array of values.
        /// 1) The minimum and maximum values are found.
        /// 2) The resulting value range is divided into equal sized sub-ranges (categoryCount).
        /// 3) The number of values that fall into each category is determined.
        /// </summary>
        public static FrequencyDistributionData CalculateDistribution(double[] valArr, int categoryCount)
        {
            // Determine min/max.
            double min = valArr[0];
            double max = min;

            for (int i = 1; i < valArr.Length; i++)
            {
                double val = valArr[i];
                if (val < min)
                {
                    min = val;
                }
                else if (val > max)
                {
                    max = val;
                }
            }

            double range = max - min;

            // Handle special case where the data series contains a single value.
            if (0.0 == range)
            {
                return new FrequencyDistributionData(min, max, 0.0, new int[] { valArr.Length });
            }

            // Loop values and for each one increment the relevant category's frequency count.
            double incr = range / (categoryCount - 1);
            int[] frequencyArr = new int[categoryCount];
            for (int i = 0; i < valArr.Length; i++)
            {
                frequencyArr[(int)((valArr[i] - min) / incr)]++;
            }
            return new FrequencyDistributionData(min, max, incr, frequencyArr);
        }
#endregion
    }
}