using System;
using Visyn.Public.Types;
using Visyn.Public.Types.Time;

namespace Visyn.Mathematics.Comparison
{
    public partial class NumberConverter : INumberConverterExtended<TimeSpan>
    {
        #region Implementation of INumberConverter<TimeSpan>

        public byte ToByte(TimeSpan a) => ToByte(a.TotalSeconds);

        public char ToChar(TimeSpan a) => ToChar(a.TotalSeconds);

        public decimal ToDecimal(TimeSpan a) => ToDecimal(a.TotalSeconds);

        public double ToDouble(TimeSpan a) => a.TotalSeconds;

        public short ToInt16(TimeSpan a) => ToInt16(a.TotalSeconds);

        public int ToInt32(TimeSpan a) => ToInt32(a.TotalSeconds);

        public long ToInt64(TimeSpan a) => ToInt64(a.TotalSeconds);

        public float ToSingle(TimeSpan a) => ToSingle(a.TotalSeconds);

        public ushort ToUInt16(TimeSpan a) => ToUInt16(a.TotalSeconds);

        public uint ToUInt32(TimeSpan a) => ToUInt32(a.TotalSeconds);

        public ulong ToUInt64(TimeSpan a) => ToUInt64(a.TotalSeconds);

        #endregion

        public TimeSpan ToTimeSpan(object a)
        {
            if (a == null) return default(TimeSpan);
            Type type;
            if (a is IValue)
            {
                type = ((IValue)a).Type;
                a = ((IValue)a).ValueAsObject();
            }
            else type = a.GetNumericType();
            if (type == typeof(Int32)) return TimeSpan.FromSeconds((Int32)a);
            if (type == typeof(Int64)) return TimeSpan.FromSeconds((Int64)a);
            if (type == typeof(Int16)) return TimeSpan.FromSeconds((Int16)a);
            if (type == typeof(Char)) return TimeSpan.FromSeconds((Char)a);
            if (type == typeof(UInt32)) return TimeSpan.FromSeconds((UInt32)a);
            if (type == typeof(UInt64)) return TimeSpan.FromSeconds((UInt64)a);
            if (type == typeof(UInt16)) return TimeSpan.FromSeconds((UInt16)a);
            if (type == typeof(Byte)) return TimeSpan.FromSeconds((Byte)a);
            if (type == typeof(Double)) return TimeSpan.FromSeconds((Double)a);
            if (type == typeof(Single)) return TimeSpan.FromSeconds((Single)a);
            if (type == typeof(Decimal)) return TimeSpan.FromSeconds(ToDouble((Decimal)a));
            if (type == typeof(DateTime)) return DateTimeExtensions.TimeSince1970((DateTime)a);
            return default(TimeSpan);
        }

        #region Implementation of INumberConverterExtended<TimeSpan>

        public DateTime ToDateTime(TimeSpan a) => DateTimeExtensions.DateTime1970() + a;

        public TimeSpan ToTimeSpan(TimeSpan a) => a;

        public bool ToBoolean(TimeSpan a) => a.TotalSeconds > 0.5;

        #endregion

        #region Implementation of INumberConverterExtended<int>

        public TimeSpan ToTimeSpan(int a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<long>

        public TimeSpan ToTimeSpan(long a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<short>

        public TimeSpan ToTimeSpan(short a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<char>

        public TimeSpan ToTimeSpan(char a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<double>

        public TimeSpan ToTimeSpan(double a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<float>

        public TimeSpan ToTimeSpan(float a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<decimal>

        public TimeSpan ToTimeSpan(decimal a) => TimeSpan.FromSeconds((double)a);

        #endregion

        #region Implementation of INumberConverterExtended<uint>

        public TimeSpan ToTimeSpan(uint a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<ulong>

        public TimeSpan ToTimeSpan(ulong a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<ushort>

        public TimeSpan ToTimeSpan(ushort a) => TimeSpan.FromSeconds(a);

        #endregion

        #region Implementation of INumberConverterExtended<byte>

        public TimeSpan ToTimeSpan(byte a) => TimeSpan.FromSeconds(a);

        #endregion

    }
}
