using System;

namespace Visyn.Mathematics
{
    public static class EqualsToleranced
    {
        public static bool Equals(this double item, double other, double tolerance) => Math.Abs(item - other) < tolerance;
        public static bool Equals(this int item, int other, double tolerance) => Math.Abs(item - other) < tolerance;
    }
}