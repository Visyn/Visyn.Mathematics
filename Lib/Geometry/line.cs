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
using Visyn.Geometry;

namespace Visyn.Mathematics.Geometry
{
    public class Line
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public IPoint P1 { get; }
        public IPoint P2 { get; }

        readonly double TOLERANCE = 1e-12;

        public Line(IPoint p1, IPoint p2)
        {
            P1 = p1;
            P2 = p2;
            // A = y2 - y1
            A = p2.Y - p1.Y;
            // B = x1 - x2
            B = p1.X - p2.X;
            // C = A * x1 + B * y1
            C = A*p1.X + B*p1.Y;
        }

        public bool Parallel(Line anotherLine)
        {
            var det = A * anotherLine.B - anotherLine.A * B;
            return Math.Abs(det) < TOLERANCE;
        }

        public bool PointOnLineSegment(IPoint point)
        {
            // Check x between p1.x and p2.x
            if (!(Math.Min(P1.X, P2.X) <= point.X)) return false;
            if (!(point.X <= Math.Max(P1.X, P2.X))) return false;
            // check y between p1.y and p2.y
            if (!(Math.Min(P1.Y, P2.Y) <= point.Y)) return false;
            if (!(point.Y <= Math.Max(P1.Y, P2.Y))) return false;
            return true;
        }

        public IPoint Intersection(Line anotherLine)
        {
            var det = A*anotherLine.B - anotherLine.A*B;
            if (Math.Abs(det) < TOLERANCE)
            {   //Lines are parallel
                return null;
            }
            var x = (anotherLine.B *C - B* anotherLine.C)/det;
            var y = (A*anotherLine.C - anotherLine.A*C)/det;
            return new PointXY(x,y);
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{P1} {P2}";

        #endregion
    }
}
