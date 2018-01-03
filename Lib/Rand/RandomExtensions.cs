#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;

namespace Visyn.Mathematics.Rand
{
    public static class RandomExtensions
    {
        public static List<T> ExclusiveList<T>(this IRandom<T> rng, T min, T max, int size)
        {
            return CreateListWithMethod(rng.Exclusive, min, max, size);
        }

        /// <summary>
        /// Inclusives the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rng">The RNG.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="size">The size.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> InclusiveList<T>(this IRandom<T> rng, T min, T max, int size)
        {
            return CreateListWithMethod(rng.Inclusive, min, max, size);
        }

        #region Extensions of IRandom

        ///// <summary>
        ///// Return a list of random integers in the range [min,max] 
        ///// </summary>
        ///// <param name="random">Random numbr generator</param>
        ///// <param name="min">Inclusive minimum of range</param>
        ///// <param name="max">Inclusive maximum of range</param>
        ///// <param name="size">Size of desired list</param>
        ///// <returns></returns>
        //public static List<int> InclusiveList(this IRandom random, int min, int max, int size)
        //{
        //    return CreateListWithMethod(random.Inclusive, min, max, size);
        //}

        ///// <summary>
        ///// Return a list of random integers in the range (min,max)
        ///// </summary>
        ///// <param name="random">Random numbr generator</param>
        ///// <param name="min">Exclusive minimum of range</param>
        ///// <param name="max">Exclusive maximum of range</param>
        ///// <param name="size">Size of desired list</param>
        ///// <returns></returns>
        //public static List<int> ExclusiveList(this IRandom random, int min, int max, int size)
        //{
        //    return CreateListWithMethod(random.Exclusive, min, max, size);
        //}

        ///// <summary>
        ///// Return a list of random doubles in the range [min,max] 
        ///// </summary>
        ///// <param name="random">Random numbr generator</param>
        ///// <param name="min">Inclusive minimum of range</param>
        ///// <param name="max">Inclusive maximum of range</param>
        ///// <param name="size">Size of desired list</param>
        ///// <returns></returns>
        //public static List<double> InclusiveList(this IRandom random, double min, double max, int size)
        //{
        //    return CreateListWithMethod(random.Inclusive, min, max, size);
        //}

        ///// <summary>
        ///// Return a list of random double in the range (min,max)
        ///// </summary>
        ///// <param name="random">Random numbr generator</param>
        ///// <param name="min">Exclusive minimum of range</param>
        ///// <param name="max">Exclusive maximum of range</param>
        ///// <param name="size">Size of list to generate</param>
        ///// <returns></returns>
        //public static List<double> ExclusiveList(this IRandom random, double min, double max, int size)
        //{
        //    return CreateListWithMethod(random.Exclusive, min, max, size);
        //}


        /// <summary>
        /// Return a list of random numbers with gaussian distribution specified
        /// by mean and sigma
        /// </summary>
        /// <param name="gaussian">Gaussian rng</param>
        /// <param name="mean">Mean of distribution</param>
        /// <param name="sigma">Standard deviation of distribution</param>
        /// <param name="size">Size of desired list</param>
        /// <returns></returns>
        public static List<double> GaussianList(this IRandomGaussian gaussian, double mean, double sigma, int size)
        {
            return CreateListWithMethod(gaussian.Gaussian, mean, sigma,size);
        }



        /// <summary>
        /// Create a list of <list type="T"> using supplied method</list>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">Method used to create list item</param>
        /// <param name="param1">Method parameter 1</param>
        /// <param name="param2">Method parameter 2</param>
        /// <param name="size">Size of list</param>
        /// <returns></returns>
        private static List<T> CreateListWithMethod<T>(Func<T, T, T> method, T param1, T param2, int size)
        {
            var list = new List<T>(size);

            for (var i = 0; i < size; i++)
            {
                list.Add(method(param1, param2));
            }
            return list;
        }

        #endregion
    }
}
