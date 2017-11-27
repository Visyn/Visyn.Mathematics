#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
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
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;
using Visyn.Exceptions;

namespace Visyn.Mathematics.Rand
{
    //   using Visyn.Util.Events;

    /// <summary>
    /// Generic random number generator factory.
    /// Creates and stores instance of the specified type random number generator
    /// </summary>
    /// <typeparam name="TRandom">The type of the random number generator.</typeparam>
    public class Rng<TRandom> where TRandom : IRandom
    {
        /// <summary>
        /// The thread safe random number generators static storage
        /// </summary>
        private static readonly Lazy<ConcurrentDictionary<int, TRandom>> _generators = new Lazy<ConcurrentDictionary<int, TRandom>>();

        /// <summary>
        /// Creates or returns shared thread safe random number generator
        /// </summary>
        /// <returns>Random number generator implementing IRandom</returns>
        /// <exception cref="Exception">May throw exception if exception handler not assigned.</exception>
        public static IRandom ThreadSafeRandom(IExceptionHandler handler, int threadID)
        {
            try
            {
                if (threadID == 0)
                {
                    var dispatcher = Dispatcher.CurrentDispatcher;
                    
                    var id = Task.CurrentId;
                    if (id != null) threadID = id.Value;
                }
                //var dispatcher2 = Dispatcher.CurrentDispatcher;
                //var iddd2 = dispatcher2.ThreadIddd();

                Debug.Assert(threadID > 0);
                var context = threadID;
                if (_generators.IsValueCreated && _generators.Value.ContainsKey(context))
                {
                    return _generators.Value[context];
                }
                var rng = (TRandom)Activator.CreateInstance(typeof(TRandom));

                _generators.Value.GetOrAdd(context, rng);

                return rng;
            }
            catch (Exception exc)
            {
                var exception = new Exception( $"{nameof(Rng<TRandom>)}.{nameof(ThreadSafeRandom)} could not create random number generator!", exc);
                if (handler == null || !handler.HandleException( $"{nameof(Rng<TRandom>)}.{nameof(ThreadSafeRandom)} ",exception))
                    throw exception;
            }
            return null;
        }

        /// <summary>
        /// Returns thread safe typed random number generator instance
        /// </summary>
        /// <typeparam name="T">Random number generator data type</typeparam>
        /// <returns>Thread safe random number generator implementing IRandom&lt;T&gt;.</returns>
        /// <exception cref="Exception">May throw exception if exception handler not assigned.</exception>
        public static IRandom<T> ThreadSafeRandom<T>(IExceptionHandler handler, int threadID) where T : IComparable => (IRandom<T>)ThreadSafeRandom(handler, threadID);

        /// <summary>
        /// Returns a unique typed random number generator instance.
        /// </summary>
        /// <typeparam name="T">Random number generator data type</typeparam>
        /// <returns>Unique random number generator implementing IRandom&lt;T&gt;.</returns>
        /// <exception cref="Exception">May throw exception if exception handler not assigned.</exception>
        public static IRandom<T> Unique<T>(IExceptionHandler handler, int threadID) where T : IComparable => (IRandom<T>)Unique(handler);

        /// <summary>
        /// Returns a unique typed random number generator instance.
        /// </summary>
        /// <typeparam name="T">Random number generator data type</typeparam>
        /// <returns>Unique random number generator implementing IRandom&lt;T&gt;.</returns>
        /// <exception cref="Exception">May throw exception if exception handler not assigned.</exception>
        public static IRandom Unique(IExceptionHandler handler)
        {
            try
            {
                var rng = (TRandom)Activator.CreateInstance(typeof(TRandom));

                ((TRandom) rng).ReSeed(new[] { (ulong)(new SystemRandom().Exclusive((double) ulong.MinValue, (double) ulong.MaxValue)) });
                return rng;
            }
            catch (Exception exc)
            {
                var exception = new Exception($"{nameof(Rng<TRandom>)}.{nameof(ThreadSafeRandom)} could not create random number generator!", exc);
                if (handler == null || !handler.HandleException($"{nameof(Rng<TRandom>)}.{nameof(ThreadSafeRandom)} ", exception))
                    throw exception;
            }
            return null;
        }
    }
}
