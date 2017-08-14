using System;
using System.Collections.Generic;
using System.Windows;

namespace Visyn.Mathematics
{
    public static class PointExtensions
    {
        public static Point Average(this IEnumerable<Point> points)
        {
            var x = 0.0;
            var y = 0.0;
            var count = 0;
            foreach(var point in points)
            {
                x += point.X;
                y += point.Y;
                count++;
            }

            return new Point(x/count, y/count);
        }

        public static double Distance(this Point a, Point b) => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

        public static double Slope(this Point p1, Point p2) => (p1.Y - p2.Y) / (p1.X - p2.X);

        public static Point Project(this Point p1, Point p2, double distance)
        {
            var deltaZ = p1.Distance(p2);
            var ratio = distance / deltaZ;

            return new Point(p2.X + ratio * (p1.X - p2.X), p2.Y + ratio * (p1.Y - p2.Y));
        }
    }
}
