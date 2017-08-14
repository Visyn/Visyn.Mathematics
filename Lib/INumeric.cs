using System;

namespace Visyn.Mathematics
{
    public interface INumeric<T>  : IComparable, IComparable<T>, IEquatable<T>
    {
    }
}