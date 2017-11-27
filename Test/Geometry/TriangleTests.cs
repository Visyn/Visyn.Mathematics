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
using System.Windows;
using NUnit.Framework;
using Visyn.Geometry;
using Visyn.Mathematics.Geometry;
using Visyn.Mathematics.Rand;

namespace Visyn.Mathematics.Test.Geometry
{
    [TestFixture()]
    public class TriangleTests
    {
        [Test()]
        public void AreaTest()
        {
            rightTriangleAreaTest(new PointXY(0, 0), 10.0, 10.0);
            rightTriangleAreaTest(new PointXY(0, 0), -10.0, 10.0);
            rightTriangleAreaTest(new PointXY(0, 0), 10.0, -10.0);
            rightTriangleAreaTest(new PointXY(0, 0), 0.1, 0.52);
            rightTriangleAreaTest(new PointXY(0.756, -0.3994), 0.1, 0.52);

            for(int i=0;i<20;i++) rngTriangleAreaTest();
        }

        [Test()]
        public void InsideTriangleTest()
        {
            var rng = new FastRng();

            for (int i = 2; i < 100; i++)
            {
                var a = new PointXY(rng.Exclusive(-100.0, 100.0), rng.Exclusive(-100.0, 100.0));
                var b = new PointXY(rng.Exclusive(-100.0, 100.0), rng.Exclusive(-100.0, 100.0));
                var c = new PointXY(rng.Exclusive(-100.0, 100.0), rng.Exclusive(-100.0, 100.0));
                var midpointBC = new PointXY((b.X + c.X)/(2.0), (b.Y + c.Y)/(2.0));
                var insidePoint = new PointXY((midpointBC.X + a.X)/(2.0), (midpointBC.Y + a.Y)/(2.0));
                Assert.IsTrue(insidePoint.InsideTriangle( a, b, c));

                var dx = midpointBC.X - a.X;
                var dy = midpointBC.Y - a.Y;

                // outside point, past BC on a->midpointBC vector.
                // As i increases, point is closer to to midpoint of BC
                var outsidePoint = new PointXY((midpointBC.X + dx/i), (midpointBC.Y + dy/i));
                Assert.IsFalse(outsidePoint.InsideTriangle(a,b,c));

                var dxy = new Vector(midpointBC.X - a.X, midpointBC.Y - a.Y);
                var outside = new PointXY(midpointBC + dxy/i);
                Assert.IsFalse(outside.InsideTriangle(a,b,c));

                // inside point, inside BC on a->midpointBC vector
                // As i increases, point is closer to midpoint of BC
                insidePoint = new PointXY((midpointBC.X - dx / i), (midpointBC.Y - dy / i));
                Assert.IsTrue(insidePoint.InsideTriangle(a, b, c));

                var inside = new PointXY(midpointBC - dxy/i);
                Assert.IsTrue(inside.InsideTriangle(c,b,a));
            }
        }

        private static void rightTriangleAreaTest(IPoint origin,double height, double width)
        {
            var a = new PointXY(origin.X + width, origin.Y);
            var b = new PointXY(origin.X, origin.Y);
            var c = new PointXY(origin.X, origin.Y + height);
            Assert.Less(Math.Abs(Triangle.Area(a, b, c) - Math.Abs(height*width)/2.0), 1e-12);
            Assert.Less(Math.Abs(Triangle.Area((Point) a, (Point) b, (Point) c) - Math.Abs(height * width) /2.0), 1e-12);
        }

        private static void rngTriangleAreaTest()
        {
            var rng = new FastRng();
            var a = new PointXY(rng.Exclusive(-1.0,1.0),rng.Exclusive(0.0, 1.0));
            var b = new PointXY(rng.Exclusive(1.0, 2.0), rng.Exclusive(-2.0, -1.0));
            var c = new PointXY(rng.Exclusive(-2.0, -1.0), rng.Exclusive(-2.0, -1.0));
            var bc = new PointXY(new[] {b,c}.Average());
            var inside = new PointXY(new [] {a,bc}.Average());

            Assert.IsTrue(inside.InsideTriangle(new List<IPoint>(new [] {a,b,c}) ));

            var areaA = Triangle.Area(b, c, inside);
            Assert.Less(Math.Abs(areaA-Triangle.Area(c,b,inside)),1e-12);
            Assert.Less(Math.Abs(areaA - Triangle.Area(inside, b, c)), 1e-12);
            Assert.Less(Math.Abs(areaA - Triangle.Area(c, inside, b)), 1e-12);
            var areaB = Triangle.Area(a, c, inside);
            Assert.Less(Math.Abs(areaB - Triangle.Area(c, a, inside)), 1e-12);
            Assert.Less(Math.Abs(areaB - Triangle.Area(inside, a, c)), 1e-12);
            Assert.Less(Math.Abs(areaB - Triangle.Area(c, inside, a)), 1e-12);
            var areaC = Triangle.Area(a, b, inside);
            Assert.Less(Math.Abs(areaC - Triangle.Area(b, a, inside)), 1e-12);
            Assert.Less(Math.Abs(areaC - Triangle.Area(inside, a, b)), 1e-12);
            Assert.Less(Math.Abs(areaC - Triangle.Area(b, inside, a)), 1e-12);
            var area = Triangle.Area(a, b, c);
            Assert.Less(Math.Abs(area - Triangle.Area(b, a, c)), 1e-12);
            Assert.Less(Math.Abs(area - Triangle.Area(c, a, b)), 1e-12);
            Assert.Less(Math.Abs(area - Triangle.Area(b, c, a)), 1e-12);

            Assert.Less(Math.Abs(area - (areaA+areaB+areaC)),1e-12);
        }
    }
}
