using System;

namespace Visyn.Mathematics
{
    public static partial class Numbers
    {
        public static void Swap<T>(ref T value1, ref T value2)
        {
            T temp = value2;
            value2 = value1;
            value1 = temp;
        }

        public static void SwapIfLess<T>(ref T reference, ref T other) where T : IComparable<T>
        {
            if(reference.CompareTo(other) < 0) Swap(ref reference,ref other);
        }
        public static void SwapIfGreater<T>(ref T reference, ref T other) where T : IComparable<T>
        {
            if (reference.CompareTo(other) > 0) Swap(ref reference, ref other);
        }
    }
}