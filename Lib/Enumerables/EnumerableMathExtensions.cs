using System;
using System.Collections.Generic;
using System.Linq;

namespace Visyn.Mathematics.Enumerables
{
    public static class EnumerableMathExtensions
    {
        public static IEnumerable<double> Subtract(this IEnumerable<double> a, IEnumerable<double> b)
        {
            using (var enumA = a.GetEnumerator())
            {
                using (var enumB = b.GetEnumerator())
                {
                    var aNext = enumA.MoveNext();
                    var bNext = enumB.MoveNext();
                    while (aNext && bNext)
                    {
                        yield return enumA.Current - enumB.Current;
                        aNext = enumA.MoveNext();
                        bNext = enumB.MoveNext();
                    }
                    if (aNext == bNext) yield break;
                    // Lengths are different
                    if (aNext)
                        do { yield return enumA.Current; }
                        while (enumA.MoveNext());  // a-0.0
                    else
                        do { yield return 0.0-enumB.Current; }
                        while (enumB.MoveNext());   // 0.0-b
                }
            }
        }
        public static IEnumerable<double> Subtract(this IEnumerable<double> a, double b) => a.Select(i => i - b);

        public static IEnumerable<double> Add(this IEnumerable<double> a, IEnumerable<double> b)
        {
            using (var enumA = a.GetEnumerator())
            {
                using (var enumB = b.GetEnumerator())
                {
                    var aNext = enumA.MoveNext();
                    var bNext = enumB.MoveNext();
                    while (aNext && bNext)
                    {
                        yield return enumA.Current + enumB.Current;
                        aNext = enumA.MoveNext();
                        bNext = enumB.MoveNext();
                    }
                    if (aNext == bNext) yield break;
                    // Lengths are different
                    if (aNext)
                        do { yield return enumA.Current; }
                        while (enumA.MoveNext()); // a + 0.0
                    else
                        do { yield return enumB.Current; }
                        while (enumB.MoveNext()) ; // a + 0.0 // b + 0.0
                }
            }
        }
        public static IEnumerable<double> Add(this IEnumerable<double> a, double b) => a.Select(i => i + b);

        public static IEnumerable<double> Multiply(this IEnumerable<double> a, IEnumerable<double> b)
        {
            using (var enumA = a.GetEnumerator())
            {
                using (var enumB = b.GetEnumerator())
                {
                    var aNext = enumA.MoveNext();
                    var bNext = enumB.MoveNext();
                    while (aNext && bNext)
                    {
                        yield return enumA.Current * enumB.Current;
                        aNext = enumA.MoveNext();
                        bNext = enumB.MoveNext();
                    }
                    if (aNext == bNext) yield break;
                    // Lengths are different
                    if (aNext)
                        do { yield return 0.0; }
                        while (enumA.MoveNext()); // a * 0.0
                    else
                        do { yield return 0.0; }
                        while (enumB.MoveNext());// 0.0 * b
                }
            }
        }

        public static IEnumerable<double> Multiply(this IEnumerable<double> a, double b) => a.Select(i => i * b);
        public static IEnumerable<double> Multiply(this IEnumerable<int> a, double b) => a.Select(i => i * b);

        public static IEnumerable<double> Divide(this IEnumerable<double> a, IEnumerable<double> b)
        {
            using (var enumA = a.GetEnumerator())
            {
                using (var enumB = b.GetEnumerator())
                {
                    var aNext = enumA.MoveNext();
                    var bNext = enumB.MoveNext();
                    while (aNext && bNext)
                    {
                        yield return enumA.Current / enumB.Current;
                        aNext = enumA.MoveNext();
                        bNext = enumB.MoveNext();
                    }
                    if (aNext == bNext) yield break;
                    // Lengths are different
                    if (aNext)
                        do
                        {
                            yield return ((enumA.Current > 0) ? double.PositiveInfinity : double.NegativeInfinity);
                        }
                        while (enumA.MoveNext());   // a / 0.0
                    else
                        do { yield return 0.0; }    // 0.0/b
                        while (enumB.MoveNext());
                }
            }
        }
        public static IEnumerable<double> Divide(this IEnumerable<double> a, double b) => a.Select(i => i / b);

        public static IEnumerable<double> Invert(this IEnumerable<double> a) => a.Select(i => 1.0 / i);

        public static IEnumerable<double> Mod(this IEnumerable<double> a, IEnumerable<double> b)
        {
            using (var enumA = a.GetEnumerator())
            {
                using (var enumB = b.GetEnumerator())
                {
                    var aNext = enumA.MoveNext();
                    var bNext = enumB.MoveNext();
                    while (aNext && bNext)
                    {
                        yield return enumA.Current % enumB.Current;
                        aNext = enumA.MoveNext();
                        bNext = enumB.MoveNext();
                    }
                    if (aNext == bNext) yield break;
                    // Lengths are different
                    if (aNext)
                        do
                        {
                            yield return double.NaN;
                        }
                        while (enumA.MoveNext());   // a / 0.0
                    else
                        do { yield return 0.0; }    // 0.0/b
                        while (enumB.MoveNext());
                }
            }
        }
        public static IEnumerable<double> Mod(this IEnumerable<double> a, double b) => a.Select(i => i % b);

        public static IEnumerable<double> Abs(this IEnumerable<double> a)
        {
            using (var enumA = a.GetEnumerator())
            {
                while (enumA.MoveNext())
                {
                    yield return Math.Abs(enumA.Current);
                }
            }
        }

        public static bool ElementsAreEqual(this IEnumerable<double> a, IEnumerable<double> b, double tolerance)
        {
            using (var enumA = a.GetEnumerator())
            {
                using (var enumB = b.GetEnumerator())
                {
                    var aNext = enumA.MoveNext();
                    var bNext = enumB.MoveNext();
                    while (aNext && bNext)
                    {
                        if (Math.Abs(enumA.Current - enumB.Current) > tolerance) return false;
                        aNext = enumA.MoveNext();
                        bNext = enumB.MoveNext();
                    }
                    if (aNext != bNext) // Lengths differ...
                        return false;
                }
            }
            return true;
        }

        public static bool ElementsAreEqual<T>(this IEnumerable<T> a, IEnumerable<T> b, double tolerance) where T : IComparable, IComparable<T>, IEquatable<T>
        {
            return (typeof(T) == typeof(double)) ?
                    ((IEnumerable<double>)a).ElementsAreEqual((IEnumerable<double>)b, tolerance) :
                    a.ToDoubles<T>().ElementsAreEqual(b.ToDoubles<T>(), tolerance);
        }

        public static bool ElementsAreEqual<T>(this IEnumerable<T> a, IEnumerable<T> b, Func<T, double> toDoubleFunction, double tolerance )
        {
            return a.Select(toDoubleFunction).ElementsAreEqual(b.Select(toDoubleFunction), tolerance);
        }
    }
}
