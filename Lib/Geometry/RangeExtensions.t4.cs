﻿#region Copyright (c) 2015-2018 Visyn
//The MIT License(MIT)
//
//Copyright (c) 2015-2018 Visyn
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
#endregion
#region Autogenerated T4 Text Template
// Autogenerated from T4 Text Template :	RangeExtensions.tt
//											file:\C:\git\proto.temp\Visyn.Mathematics\Lib\Geometry\RangeExtensions.tt
// Autogeneration date/time:				8/13/2017 9:25:49 PM
#endregion
using System;
using Visyn.Mathematics;

using System.Collections.Generic;

namespace Visyn.Mathematics.Geometry
{
	public static class RangeExtensions
	{
		public static Range<Int32> Limits(this IEnumerable<IRange<Int32>> collection)
        {
			var min = Int32.MaxValue;
			var max = Int32.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<Int32>(min,max);
		}
		public static Range<Int64> Limits(this IEnumerable<IRange<Int64>> collection)
        {
			var min = Int64.MaxValue;
			var max = Int64.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<Int64>(min,max);
		}
		public static Range<Int16> Limits(this IEnumerable<IRange<Int16>> collection)
        {
			var min = Int16.MaxValue;
			var max = Int16.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<Int16>(min,max);
		}
		public static Range<UInt32> Limits(this IEnumerable<IRange<UInt32>> collection)
        {
			var min = UInt32.MaxValue;
			var max = UInt32.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<UInt32>(min,max);
		}
		public static Range<UInt64> Limits(this IEnumerable<IRange<UInt64>> collection)
        {
			var min = UInt64.MaxValue;
			var max = UInt64.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<UInt64>(min,max);
		}
		public static Range<UInt16> Limits(this IEnumerable<IRange<UInt16>> collection)
        {
			var min = UInt16.MaxValue;
			var max = UInt16.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<UInt16>(min,max);
		}
		public static Range<Double> Limits(this IEnumerable<IRange<Double>> collection)
        {
			var min = Double.MaxValue;
			var max = Double.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<Double>(min,max);
		}
		public static Range<Single> Limits(this IEnumerable<IRange<Single>> collection)
        {
			var min = Single.MaxValue;
			var max = Single.MinValue;

			foreach(var range in collection)
			{
				min = Math.Min(range.Minimum,min);
				max = Math.Max(range.Maximum,max);
			}
			return new Range<Single>(min,max);
		}

	}
}