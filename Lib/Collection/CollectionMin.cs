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
using System.Collections.Generic;

namespace Visyn.Mathematics.Collection
{
    public static class CollectionMin
    {
        public static DateTime MinDateTime<TKey>(this IList<IReadOnlyDictionary<TKey, object>> collection, TKey columnKey)
        {
            var min = DateTime.MaxValue;
            foreach(var dict in collection )
            {
                var time = dict.TryGet(columnKey, DateTime.MaxValue);
                if (time < min) min = time;
            }
            return min;
        }

        private static TOut TryGet<TKey, TValue, TOut>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key, TOut defaultValue) where TOut : TValue
        {
            if (dict?.ContainsKey(key) != true) return defaultValue;
            var obj = dict[key];
            if (obj is TOut) return (TOut)obj;
            return defaultValue;
        }
    }
}
