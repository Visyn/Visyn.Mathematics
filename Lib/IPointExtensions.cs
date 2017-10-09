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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Visyn.Geometry;
using Visyn.Mathematics.Trigonometry;

namespace Visyn.Mathematics
{
    public static class IPointExtensions
    {
        private const double TOLERANCE = 1e-12;

        public static IPoint Average(this IEnumerable<IPoint> points)
        {
            var x = 0.0;
            var y = 0.0;
            var count = 0;
            foreach (var point in points)
            {
                x += point.X;
                y += point.Y;
                count++;
            }

            return new PointXY(x / count, y / count);
        }

        public static bool ContainsValue<TPoint>(this IEnumerable<TPoint> points, TPoint value, double tolerance=TOLERANCE) where TPoint : IPoint
        {
            foreach (var point in points)
            {
                var distance = point.Distance(value);
                if (distance < tolerance)
                    return true;
            }
            return false;
        }

        public static int AddUniquePoints<TPoint>(this IList<TPoint> points, IEnumerable<TPoint> pointsToAdd, double tolerance = TOLERANCE) where TPoint : IPoint
        {
            var count = 0;
            foreach(var point in pointsToAdd)
            {
                if (points.ContainsValue(point, tolerance)) continue;
                points.Add(point);
                count++;
            }
            return count;
        }

        public static IList<TPoint> UniquePoints<TPoint>(this IEnumerable<TPoint> points, double tolerance = TOLERANCE) where TPoint : IPoint
        {
            var result = new List<TPoint>();

            foreach(var point in points)
            {
                if(!result.ContainsValue(point,tolerance))    result.Add(point);
            }
            return result;
        }



        public static ValueRange<PointXY> Limits<TPoint>(this IEnumerable<TPoint> points) where TPoint : IPoint
        {
            var xMin = double.MaxValue;
            var yMin = double.MaxValue;
            var yMax = double.MinValue;
            var xMax = double.MinValue;

            foreach (var point in points)
            {
                if (point.X > xMax) xMax = point.X;
                if (point.Y > yMax) yMax = point.Y;
                if (point.X < xMin) xMin = point.X;
                if (point.Y < yMin) yMin = point.Y;
            }
            ;
            var min = new PointXY(xMin, yMin);
            var max = new PointXY(xMax, yMax);
            var range = new ValueRange<PointXY>(min, max);
            return range;
        }

        public static IPoint PointOnSegment(this IPoint pT, IPoint a, IPoint b)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (a.X == b.X)
            {
                if (pT.Y < a.Y && pT.Y < b.Y && a.Y < b.Y)      return new PointXY(a.X, a.Y);
                if (pT.Y < a.Y && pT.Y < b.Y && a.Y >= b.Y)     return new PointXY(b.X, b.Y);
                if (pT.Y > a.Y && pT.Y > b.Y && a.Y >= b.Y)     return new PointXY(a.X, a.Y);
                if (pT.Y > a.Y && pT.Y > b.Y && a.Y < b.Y)      return new PointXY(b.X, b.Y);
                return new PointXY(a.X, pT.Y);
            }

            if (a.Y == b.Y)
            {
                if (pT.X < a.X && pT.X < b.X && a.X < b.X)      return new PointXY(a.X, a.Y);
                if (pT.X < a.X && pT.X < b.X && a.X >= b.X)     return new PointXY(b.X, b.Y);
                if (pT.X > a.X && pT.X > b.X && a.X >= b.X)     return new PointXY(a.X, a.Y);
                if (pT.X > a.X && pT.X > b.X && a.X < b.X)      return new PointXY(b.X, b.Y);
                return new PointXY(pT.X, a.Y);
            }
            // ReSharper restore CompareOfFloatsByEqualityOperator

            #region Normal Case

            // Calculate the slope of the segment
            var aN = (a.Y - b.Y)/(a.X - b.X);
            // Calculate the offset for the line including the segment
            var bN = a.Y - aN*a.X;
            // Calculate the perpendicular slope to the segment slope
            var aP = -1.0/aN;
            // Calculate the offset for the perpendicular line containing point pT
            var bP = pT.Y - aP*pT.X;// Calculate crossing point
            var sX = (bN - bP)/(aP - aN);
            var sY = aN*sX + bN;

            // X must be between p0.X and p1.X.  If it is not than use point with closest x
            if (sX < a.X && sX < b.X && a.X < b.X)      return new PointXY(a.X, a.Y);
            if (sX < a.X && sX < b.X && a.X >= b.X)     return new PointXY(b.X, b.Y);
            if (sX > a.X && sX > b.X && a.X < b.X)      return new PointXY(b.X, b.Y);
            if (sX > a.X && sX > b.X && a.X >= b.X)     return new PointXY(a.X, a.Y);
            return new PointXY(sX, sY);

            #endregion
        }


        public static IList ToList<T>(this IPoint point, Func<double,T> convertFunc) where T : IComparable
        {
            if(convertFunc == null)
            {
                if (typeof(T) == typeof(double)) return (new[] {point.X,point.Y}).ToList();
                if (typeof(T) == typeof(object)) return (new object[] { point.X, point.Y }).ToList();
                if (typeof(T) == typeof(double)) return (new string[] { point.X.ToString(), point.Y.ToString() }).ToList();
                throw new NotSupportedException($"IPoint.ToList() requires convert function to create a list of type '{typeof(T).Name}'");
            }
            var list = new List<T> {convertFunc(point.X), convertFunc(point.Y)};
            return list;
        }

        public static List<double> ToListDouble(this IPoint point) => new List<double>(new[] {point.X, point.Y});
        public static List<object> ToListObject(this IPoint point) => new List<object>(new object [] { point.X, point.Y });

        public static double Distance(this IPoint a, IPoint b) => Math.Sqrt(Math.Pow(a.X - b.X, 2.0) + Math.Pow(a.Y - b.Y, 2.0));


        public static double Slope(this IPoint p1, IPoint p2) => (p1.Y - p2.Y) / (p1.X - p2.X);


        public static IPoint Project(this IPoint p1, IPoint p2, double distance)
        {
            var deltaZ = p1.Distance(p2);
            var ratio = distance / deltaZ;

            return new PointXY(p2.X - ratio * (p1.X - p2.X), p2.Y - ratio * (p1.Y - p2.Y));
        }

        public static double AngleRadians(this IPoint a, IPoint b) => (a.Y - b.Y == 0) ? double.NaN : Math.Atan2((a.Y - b.Y) ,(a.X - b.X));
        public static double AngleDegrees(this IPoint a, IPoint b) => Angle.ToDegrees(AngleRadians (a,b));//* (180.0/Math.PI)));

        // x1* x2 +y1 * y2
        public static double DotProduct(this IPoint p1, IPoint p2) => p1.X*p2.X + p1.Y*p2.Y;

        // x1*y2 - y1*x2
        public static double CrossProduct(this IPoint p1, IPoint p2) => p1.X*p2.Y - p1.Y*p2.X;

        // Reference: https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications/
        //If onEdge is true, use as many points as possible for
        //the convex hull, otherwise as few as possible.
        // convexHull(point[] X, boolean onEdge)
        [Obsolete("Not tested, returns null",true)]
        public static IList<TPoint> ConvexHull<TPoint>(TPoint[] points, bool onEdge) where TPoint : IPoint
        {
            var N = points.Length;
            var p = 0;
            var used = new bool[N];
            //First find the leftmost point
            for (var i = 1; i < N; i++)
            {
                if (points[i].X < points[p].X) p = i;
            }
            var start = p;
            do
            {
                var n = -1;
                double dist = onEdge ? double.PositiveInfinity : 0;
                for (var i = 0; i < N; i++)
                {
                    //X[i] is the X in the discussion

                    //Don't go back to the same point you came from
                    if (i == p) continue;

                    //Don't go to a visited point
                    if (used[i]) continue;

                    //If there is no N yet, set it to X
                    if (n == -1) n = i;

                    //int cross = (X[i] - X[p]) x (X[n] - X[p]);
                    var xi= new PointXY(points[i]);
                    var xp = new PointXY(points[p]);
                    var xn = new PointXY(points[n]);
                    var cross = (xi - xp).CrossProduct(xn - xp);
                    //d is the distance from P to X
                    // d = (X[i] - X[p]) ⋅ (X[i] - X[p]);
                    double d = points[i].Distance(points[p]);
                    if (cross < 0)
                    {   //As described above, set N=X
                        n = i;
                        dist = d;
                    }
                    else if (cross == 0)
                    {
                        //In this case, both N and X are in the
                        //same direction.  If onEdge is true, pick the
                        //closest one, otherwise pick the farthest one.
                        if (onEdge && d < dist)
                        {
                            dist = d;
                            n = i;
                        }
                        else if (!onEdge && d > dist)
                        {
                            dist = d;
                            n = i;
                        }
                    }
                }
                p = n;
                used[p] = true;
            } while (start != p);
            
            return null;
        }
    }
}
