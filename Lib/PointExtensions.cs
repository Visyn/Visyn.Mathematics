#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
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
