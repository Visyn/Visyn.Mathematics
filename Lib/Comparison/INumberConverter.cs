using System;

namespace Visyn.Mathematics.Comparison
{
    public interface INumberConverter<T>
    {
        byte ToByte(T a);
        char ToChar(T a);
        decimal ToDecimal(T a);
        double ToDouble(T a);
        short ToInt16(T a);
        int ToInt32(T a);
        long ToInt64(T a);
        float ToSingle(T a);
        ushort ToUInt16(T a);
        uint ToUInt32(T a);
        ulong ToUInt64(T a);
    }

    public interface INumberConverterExtended<T> : INumberConverter<T>
    {
        DateTime ToDateTime(T a);
        TimeSpan ToTimeSpan(T a);
        Boolean ToBoolean(T a);
    }

    public interface INumberConverter : INumberConverter<object>
    {
    }
}