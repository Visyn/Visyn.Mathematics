#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Visyn.Geometry;

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
