using System;
using Visyn.Public.Types;
using Visyn.Public.Types.Time;

namespace Visyn.Mathematics.Comparison
{
    public partial class NumberConverter : INumberConverterExtended<Boolean>
    {
        #region Implementation of INumberConverter<Boolean>

        public byte ToByte(Boolean a) => a ? (byte)1 : (byte)0;

        public char ToChar(Boolean a) => a ? (char)1 : (char)0;

        public decimal ToDecimal(Boolean a) => a ? 1 : 0;

        public double ToDouble(Boolean a) => a ? 1.0 : 0.0;

        public short ToInt16(Boolean a) => a ? (Int16)1 : (Int16)0;

        public int ToInt32(Boolean a) => a ? 1 : 0;

        public long ToInt64(Boolean a) => a ? 1 : 0;

        public float ToSingle(Boolean a) => a ? 1 : 0;

        public ushort ToUInt16(Boolean a) => a ? (UInt16)1 : (UInt16)0;

        public uint ToUInt32(Boolean a) => a ? (UInt32)1 : (UInt32)0;

        public ulong ToUInt64(Boolean a) => a ? (UInt64)1 : (UInt64)0;

        #endregion

        public Boolean ToBoolean(object a)
        {
            if (a == null) return default(bool);
            Type type;
            if (a is IValue)
            {
                type = ((IValue)a).Type;
                a = ((IValue)a).ValueAsObject();
            }
            else type = a.GetNumericType();
            if (type == typeof(Int32)) return (Int32)a > 0;
            if (type == typeof(Int64)) return (Int64)a > 0;
            if (type == typeof(Int16)) return (Int16)a > 0;
            if (type == typeof(Char)) return (Char)a > 0;
            if (type == typeof(UInt32)) return (UInt32)a > 0;
            if (type == typeof(UInt64)) return (UInt64)a > 0;
            if (type == typeof(UInt16)) return (UInt16)a > 0;
            if (type == typeof(Byte)) return (Byte)a > 0;
            if (type == typeof(Double)) return (Double)a > 0.5;
            if (type == typeof(Single)) return (Single)a > 0.5f;
            if (type == typeof(Decimal)) return (Decimal)a > (Decimal)0.5;
            return Convert.ToBoolean(a);
        }

        #region Implementation of INumberConverterExtended<bool>

        public DateTime ToDateTime(bool a) => a ? DateTimeExtensions.FromSecondsSince1970(1.0) : DateTimeExtensions.DateTime1970();

        public TimeSpan ToTimeSpan(bool a) => a ? TimeSpan.FromSeconds(1.0) : TimeSpan.Zero;

        public Boolean ToBoolean(Boolean a) => a;

        #region Implementation of INumberConverterExtended<int>

        public bool ToBoolean(int a) => a > 0;

        #endregion

        #region Implementation of INumberConverterExtended<long>

        public bool ToBoolean(long a) => a > 0;

        #endregion

        #region Implementation of INumberConverterExtended<short>

        public bool ToBoolean(short a) => a > 0;

        #endregion

        #region Implementation of INumberConverterExtended<char>

        public bool ToBoolean(char a) => a > 0;

        #endregion

        #region Implementation of INumberConverterExtended<double>

        public bool ToBoolean(double a) => a > 0.5;

        #endregion

        #region Implementation of INumberConverterExtended<float>

        public bool ToBoolean(float a) => a > 0.5f;

        #endregion

        #region Implementation of INumberConverterExtended<decimal>

        public bool ToBoolean(decimal a) => a > (decimal)0.5;

        #endregion

        #region Implementation of INumberConverterExtended<uint>

        public bool ToBoolean(uint a) => a > 0;

        #endregion

        #region Implementation of INumberConverterExtended<ulong>

        public bool ToBoolean(ulong a) => a > 0;

        #endregion

        #region Implementation of INumberConverterExtended<ushort>

        public bool ToBoolean(ushort a) => a > 0;

        #endregion

        #region Implementation of INumberConverterExtended<byte>

        public bool ToBoolean(byte a) => a > 0;

        #endregion

        #endregion
    }
}
