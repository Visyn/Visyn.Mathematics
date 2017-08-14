using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Visyn.Public.Geometry;

namespace Visyn.Mathematics.Geometry
{
    public static class Triangle
    {
        public static double Area(Point a, Point b, Point c)
        {
            return 0.5*Math.Abs(a.X*(b.Y-c.Y) + b.X*(c.Y -a.Y) + c.X * (a.Y - b.Y) );
        }

        public static double Area(IPoint a, IPoint b, IPoint c)
        {
            return 0.5 * Math.Abs(a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y));
        }

        private static double sign(Point a, Point b, Point c) => (a.X - c.X) * (b.Y - c.Y) - (b.X - c.X) * (a.Y - c.Y);

        public static bool InsideTriangle(this Point pt, Point a, Point b, Point c)
        {
            var b1 = sign(pt, a, b) < 0.0f;
            var b2 = sign(pt, b, c) < 0.0f;
            var b3 = sign(pt, c, a) < 0.0f;

            return ((b1 == b2) && (b2 == b3));
        }

        private static double sign(IPoint a, IPoint b, IPoint c) => (a.X - c.X) * (b.Y - c.Y) - (b.X - c.X) * (a.Y - c.Y);

        public static bool InsideTriangle(this IPoint pt, IPoint a, IPoint b, IPoint c)
        {
            var b1 = sign(pt, a, b) < 0.0f;
            var b2 = sign(pt, b, c) < 0.0f;
            var b3 = sign(pt, c, a) < 0.0f;
            if (b1 != b2) return false;
            if (b2 != b3) return false;
            return true;
//            return ((b1 == b2) && (b2 == b3));
        }

        public static bool InsideTriangle(this IPoint point, IList<IPoint> triPoints)
        {
            Debug.Assert(triPoints.Count == 3);
            return point.InsideTriangle(triPoints[0],triPoints[1],triPoints[2]);
        }

        public static IPoint Inside(this IPoint pT, IPoint a, IPoint b, IPoint c)
        {
            var ab = pT.PointOnSegment(a, b);
            var bc = pT.PointOnSegment(b, c);
            var ac = pT.PointOnSegment(a, c);

            var distanceAb = pT.Distance(ab);
            var distanceBc = pT.Distance(bc);

            if (distanceBc < distanceAb)
            {
                return pT.Distance(ac) < distanceBc ? ac : bc;
            }
            return pT.Distance(ac) < distanceAb ? ac : ab;
        }
    }
}
