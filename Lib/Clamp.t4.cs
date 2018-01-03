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
// Autogenerated from T4 Text Template :	Clamp.tt
//											file:\C:\git\proto.temp\Visyn.Mathematics\Lib\Clamp.tt
// Autogeneration date/time:				8/13/2017 9:07:09 PM
#endregion
using System;
using Visyn.Mathematics;


namespace Visyn.Mathematics
{ 
	public static partial class Numbers
	{
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Int32 Clamp (this Int32 value, Int32 min, Int32 max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Int64 Clamp (this Int64 value, Int64 min, Int64 max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Int16 Clamp (this Int16 value, Int16 min, Int16 max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Char Clamp (this Char value, Char min, Char max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static UInt32 Clamp (this UInt32 value, UInt32 min, UInt32 max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static UInt64 Clamp (this UInt64 value, UInt64 min, UInt64 max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static UInt16 Clamp (this UInt16 value, UInt16 min, UInt16 max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Byte Clamp (this Byte value, Byte min, Byte max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Double Clamp (this Double value, Double min, Double max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Single Clamp (this Single value, Single min, Single max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}
        /// <summary>
        /// Clamp value to range [min,max] (inclusive)
        /// </summary>
        /// <param name="value">Value to clamp</param>
        /// <param name="min">Minimum allowed value</param>
        /// <param name="max">Maximum allowed value</param>
        /// <returns>Value in range [min,max]</returns>
		public static Decimal Clamp (this Decimal value, Decimal min, Decimal max) 
		{
			if(max < min) 
			{
				var temp = min;
				min = max;
				max = temp;
			}
            return value < min ? min : (value > max) ? max : value;
		}

		public static Int32 ClampInt32 (decimal value) 
		{
			if(value < Int32.MinValue) return Int32.MinValue;		// Signed implementation
			if(value > Int32.MaxValue) return Int32.MaxValue;
			return (Int32) value;
		}

		public static Int32 ClampInt32 (Int64 value) 
		{
			if(value < Int32.MinValue) return Int32.MinValue;	// signed implementation
			if(value > Int32.MaxValue) return Int32.MaxValue;
			return (Int32) value;
		}

		public static Int64 ClampInt64 (decimal value) 
		{
			if(value < Int64.MinValue) return Int64.MinValue;		// Signed implementation
			if(value > Int64.MaxValue) return Int64.MaxValue;
			return (Int64) value;
		}

		public static Int64 ClampInt64 (Int64 value) 
		{
			return (Int64) value;
		}

		public static Int16 ClampInt16 (decimal value) 
		{
			if(value < Int16.MinValue) return Int16.MinValue;		// Signed implementation
			if(value > Int16.MaxValue) return Int16.MaxValue;
			return (Int16) value;
		}

		public static Int16 ClampInt16 (Int64 value) 
		{
			if(value < Int16.MinValue) return Int16.MinValue;	// signed implementation
			if(value > Int16.MaxValue) return Int16.MaxValue;
			return (Int16) value;
		}

		public static Char ClampChar (decimal value) 
		{
			if(value < Char.MinValue) return Char.MinValue;		// Signed implementation
			if(value > Char.MaxValue) return Char.MaxValue;
			return (Char) value;
		}

		public static Char ClampChar (Int64 value) 
		{
			if(value < Char.MinValue) return Char.MinValue;	// signed implementation
			if(value > Char.MaxValue) return Char.MaxValue;
			return (Char) value;
		}

		public static UInt32 ClampUInt32 (decimal value) 
		{
			if(value < 0) return (UInt32)0;		// Unsigned implementation
			if(value > UInt32.MaxValue) return UInt32.MaxValue;
			return (UInt32) value;
		}

		public static UInt32 ClampUInt32 (Int64 value) 
		{
			if(value < 0) return (UInt32)0;	// Unsigned implementation
			if(value > UInt32.MaxValue) return UInt32.MaxValue;
			return (UInt32) value;
		}

		public static UInt64 ClampUInt64 (decimal value) 
		{
			if(value < 0) return (UInt64)0;		// Unsigned implementation
			if(value > UInt64.MaxValue) return UInt64.MaxValue;
			return (UInt64) value;
		}

		public static UInt64 ClampUInt64 (Int64 value) 
		{
			if(value < 0) return (UInt64)0;	// Unsigned implementation
			return (UInt64) value;
		}

		public static UInt16 ClampUInt16 (decimal value) 
		{
			if(value < 0) return (UInt16)0;		// Unsigned implementation
			if(value > UInt16.MaxValue) return UInt16.MaxValue;
			return (UInt16) value;
		}

		public static UInt16 ClampUInt16 (Int64 value) 
		{
			if(value < 0) return (UInt16)0;	// Unsigned implementation
			if(value > UInt16.MaxValue) return UInt16.MaxValue;
			return (UInt16) value;
		}

		public static Byte ClampByte (decimal value) 
		{
			if(value < 0) return (Byte)0;		// Unsigned implementation
			if(value > Byte.MaxValue) return Byte.MaxValue;
			return (Byte) value;
		}

		public static Byte ClampByte (Int64 value) 
		{
			if(value < 0) return (Byte)0;	// Unsigned implementation
			if(value > Byte.MaxValue) return Byte.MaxValue;
			return (Byte) value;
		}

		public static Double ClampDouble (decimal value) 
		{
			return (Double) value;
		}

		public static Double ClampDouble (Int64 value) 
		{
			return (Double) value;
		}

		public static Single ClampSingle (decimal value) 
		{
			return (Single) value;
		}

		public static Single ClampSingle (Int64 value) 
		{
			return (Single) value;
		}

		public static Decimal ClampDecimal (decimal value) 
		{
			return (Decimal) value;
		}

		public static Decimal ClampDecimal (Int64 value) 
		{
			if(value < Decimal.MinValue) return Decimal.MinValue;	// signed implementation
			if(value > Decimal.MaxValue) return Decimal.MaxValue;
			return (Decimal) value;
		}
	}
}