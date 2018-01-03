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
// Autogenerated from T4 Text Template :	NumberComparer.tt
//											file:\C:\git\proto.temp\Visyn.Mathematics\Lib\Comparison\NumberComparer.tt
// Autogeneration date/time:				8/14/2017 7:36:40 PM
#endregion
using System;
using Visyn.Mathematics;

using System.Diagnostics;
using Visyn.Comparison;
using Visyn.Exceptions;

namespace Visyn.Mathematics.Comparison 
{ 
	public static class CompareNumbers
	{
	    public static CustomComparer<T> Toleranced<T>(T tolerance) where T : IComparable, IComparable<T>, IEquatable<T>
        { 
			if (tolerance.IsNumeric())
            {
				// Int32 
				if (typeof(T) == typeof(Int32 ))  return (new CustomComparer<Int32>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToInt32(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToInt32(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// Int64 
				if (typeof(T) == typeof(Int64 ))  return (new CustomComparer<Int64>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToInt64(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToInt64(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// Int16 
				if (typeof(T) == typeof(Int16 ))  return (new CustomComparer<Int16>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToInt16(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToInt16(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// Char 
				if (typeof(T) == typeof(Char ))  return (new CustomComparer<Char>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToChar(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToChar(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// UInt32 
				if (typeof(T) == typeof(UInt32 ))  return (new CustomComparer<UInt32>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToUInt32(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToUInt32(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// UInt64 
				if (typeof(T) == typeof(UInt64 ))  return (new CustomComparer<UInt64>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToUInt64(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToUInt64(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// UInt16 
				if (typeof(T) == typeof(UInt16 ))  return (new CustomComparer<UInt16>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToUInt16(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToUInt16(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// Byte 
				if (typeof(T) == typeof(Byte ))  return (new CustomComparer<Byte>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToByte(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToByte(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// Double 
				if (typeof(T) == typeof(Double ))  return (new CustomComparer<Double>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToDouble(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToDouble(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// Single 
				if (typeof(T) == typeof(Single ))  return (new CustomComparer<Single>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToSingle(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToSingle(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
				// Decimal 
				if (typeof(T) == typeof(Decimal ))  return (new CustomComparer<Decimal>((a, b) => 
                                (a > b) ? (((a - b) <= Convert.ToDecimal(tolerance) ? 0 : 1)) : ((b - a) <= Convert.ToDecimal(tolerance) ? 0 : -1)
                            )) as CustomComparer<T>;
            }
            throw new NotImplementedException($"CustomComparer<T>.{nameof(Toleranced)} not implemented for Type={typeof(T)}");
        }

		#region numbericTypes

		// Note The following type conversions do not work [using Convert.ToXxx(...)]
		// char-> double, single,>decimal
		// double->char

		// Int32
		public static CustomComparer<T> Toleranced<T>(Int32 tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToInt32() for every comparison
			if(typeof(T) == typeof(Int32))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        Int32 x = Convert.ToInt32(a);
		        Int32 y = Convert.ToInt32(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Int32 tolerance, Func<T,Int32> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// Int64
		public static CustomComparer<T> Toleranced<T>(Int64 tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToInt64() for every comparison
			if(typeof(T) == typeof(Int64))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        Int64 x = Convert.ToInt64(a);
		        Int64 y = Convert.ToInt64(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Int64 tolerance, Func<T,Int64> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// Int16
		public static CustomComparer<T> Toleranced<T>(Int16 tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToInt16() for every comparison
			if(typeof(T) == typeof(Int16))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        Int16 x = Convert.ToInt16(a);
		        Int16 y = Convert.ToInt16(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Int16 tolerance, Func<T,Int16> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// Char
		public static CustomComparer<T> Toleranced<T>(Char tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToChar() for every comparison
			if(typeof(T) == typeof(Char))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
				var x = Convert.ToInt32(a);
				var y = Convert.ToInt32(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Char tolerance, Func<T,Char> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// UInt32
		public static CustomComparer<T> Toleranced<T>(UInt32 tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToUInt32() for every comparison
			if(typeof(T) == typeof(UInt32))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        UInt32 x = Convert.ToUInt32(a);
		        UInt32 y = Convert.ToUInt32(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(UInt32 tolerance, Func<T,UInt32> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// UInt64
		public static CustomComparer<T> Toleranced<T>(UInt64 tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToUInt64() for every comparison
			if(typeof(T) == typeof(UInt64))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        UInt64 x = Convert.ToUInt64(a);
		        UInt64 y = Convert.ToUInt64(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(UInt64 tolerance, Func<T,UInt64> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// UInt16
		public static CustomComparer<T> Toleranced<T>(UInt16 tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToUInt16() for every comparison
			if(typeof(T) == typeof(UInt16))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        UInt16 x = Convert.ToUInt16(a);
		        UInt16 y = Convert.ToUInt16(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(UInt16 tolerance, Func<T,UInt16> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// Byte
		public static CustomComparer<T> Toleranced<T>(Byte tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToByte() for every comparison
			if(typeof(T) == typeof(Byte))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        Byte x = Convert.ToByte(a);
		        Byte y = Convert.ToByte(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Byte tolerance, Func<T,Byte> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// Double
		public static CustomComparer<T> Toleranced<T>(Double tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToDouble() for every comparison
			if(typeof(T) == typeof(Double))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        Double x = a is char ? Convert.ToInt32(a) : Convert.ToDouble(a);
                Double y = b is char ? Convert.ToInt32(b) : Convert.ToDouble(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Double tolerance, Func<T,Double> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// Single
		public static CustomComparer<T> Toleranced<T>(Single tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToSingle() for every comparison
			if(typeof(T) == typeof(Single))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        Single x = a is char ? Convert.ToInt32(a) : Convert.ToSingle(a);
                Single y = b is char ? Convert.ToInt32(b) : Convert.ToSingle(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Single tolerance, Func<T,Single> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		// Decimal
		public static CustomComparer<T> Toleranced<T>(Decimal tolerance) 
		{
			// If tolerance type matches parameter type, use function without call to Convert.ToDecimal() for every comparison
			if(typeof(T) == typeof(Decimal))  return Toleranced(tolerance) as CustomComparer<T>;

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        Decimal x = a is char ? Convert.ToInt32(a) : Convert.ToDecimal(a);
                Decimal y = b is char ? Convert.ToInt32(b) : Convert.ToDecimal(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }

		public static CustomComparer<T> Toleranced<T>(Decimal tolerance, Func<T,Decimal> converter)
		{
		    if (converter == null) throw new ArgumentNullException($"{nameof(converter)} can not be null!");

		    return  new CustomComparer<T>(new Func<T, T, int>((a, b) =>
		    {
		        var x = converter(a);
		        var y = converter(b);
				// Calculate difference so that the result is positive (works for both unsigned and signed types)
		        if (x > y) return (x - y) <= tolerance ? 0 : 1;
		        return (y - x) <= tolerance ? 0 : -1;
		    }));
        }
		#endregion
	}
}