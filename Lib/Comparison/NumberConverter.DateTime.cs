using System;
using Visyn.Public.Types;
using Visyn.Public.Types.Time;

namespace Visyn.Mathematics.Comparison
{
    public partial class NumberConverter : INumberConverterExtended<DateTime>
    {
        #region Implementation of INumberConverter<TimeSpan>

        public byte ToByte(DateTime a) => ToByte(a.SecondsSince1970().Clamp(byte.MinValue,byte.MaxValue));

        public char ToChar(DateTime a) =>ToChar(a.SecondsSince1970().Clamp(char.MinValue, char.MaxValue));

        public decimal ToDecimal(DateTime a) => ToDecimal(a.SecondsSince1970());

        public double ToDouble(DateTime a) =>a.SecondsSince1970();

        public short ToInt16(DateTime a) => ToInt16(a.SecondsSince1970().Clamp(Int16.MinValue, Int16.MaxValue));

        public int ToInt32(DateTime a) => ToInt32(a.SecondsSince1970().Clamp(Int32.MinValue, Int32.MaxValue));

        public long ToInt64(DateTime a) => ToInt64(a.SecondsSince1970().Clamp(Int64.MinValue, Int64.MaxValue));
        
        public float ToSingle(DateTime a) => ToSingle(a.SecondsSince1970());
        
        public ushort ToUInt16(DateTime a) => ToUInt16(a.SecondsSince1970().Clamp(UInt16.MinValue, UInt16.MaxValue));

        public uint ToUInt32(DateTime a) => ToUInt32(a.SecondsSince1970().Clamp(UInt32.MinValue, UInt32.MaxValue));

        public ulong ToUInt64(DateTime a) => ToUInt64(a.SecondsSince1970().Clamp(UInt64.MinValue, UInt64.MaxValue));

        #endregion


        public DateTime ToDateTime(object a)
        {
            if (a == null) return default(DateTime);
            Type type;
            if (a is IValue)
            {
                type = ((IValue)a).Type;
                a = ((IValue)a).ValueAsObject();
            }
            else type = a.GetNumericType();
            if (type == typeof(Int32)) return DateTimeExtensions.FromSecondsSince1970((Int32)a);
            if (type == typeof(Int64)) return DateTimeExtensions.FromSecondsSince1970((Int64)a);
            if (type == typeof(Int16)) return DateTimeExtensions.FromSecondsSince1970((Int16)a);
            if (type == typeof(Char)) return DateTimeExtensions.FromSecondsSince1970((Char)a);
            if (type == typeof(UInt32)) return DateTimeExtensions.FromSecondsSince1970((UInt32)a);
            if (type == typeof(UInt64)) return DateTimeExtensions.FromSecondsSince1970((UInt64)a);
            if (type == typeof(UInt16)) return DateTimeExtensions.FromSecondsSince1970((UInt16)a);
            if (type == typeof(Byte)) return DateTimeExtensions.FromSecondsSince1970((Byte)a);
            if (type == typeof(Double)) return DateTimeExtensions.FromSecondsSince1970((Double)a);
            if (type == typeof(Single)) return DateTimeExtensions.FromSecondsSince1970((Single)a);
            if (type == typeof(Decimal)) return DateTimeExtensions.FromSecondsSince1970(ToDouble((Decimal)a));
            return Convert.ToDateTime(a);
        }

        #region Implementation of INumberConverterExtended<DateTime>

        public DateTime ToDateTime(DateTime a) => a;

        public TimeSpan ToTimeSpan(DateTime a) => a.TimeSince1970();

        public bool ToBoolean(DateTime a) => a > DateTimeExtensions.DateTime1970();

        #region Implementation of INumberConverterExtended<int>

        public DateTime ToDateTime(int a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<long>

        public DateTime ToDateTime(long a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<short>

        public DateTime ToDateTime(short a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<char>

        public DateTime ToDateTime(char a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<double>

        public DateTime ToDateTime(double a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<float>

        public DateTime ToDateTime(float a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<decimal>

        public DateTime ToDateTime(decimal a) => DateTimeExtensions.FromSecondsSince1970((double)a);

        #endregion

        #region Implementation of INumberConverterExtended<uint>

        public DateTime ToDateTime(uint a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<ulong>

        public DateTime ToDateTime(ulong a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<ushort>

        public DateTime ToDateTime(ushort a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #region Implementation of INumberConverterExtended<byte>

        public DateTime ToDateTime(byte a) => DateTimeExtensions.FromSecondsSince1970(a);

        #endregion

        #endregion
    }
}
