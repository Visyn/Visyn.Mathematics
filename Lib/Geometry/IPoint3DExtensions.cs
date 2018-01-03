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

using System.Collections.Generic;
using Visyn.Geometry;

namespace Visyn.Mathematics.Geometry
{
    // ReSharper disable once InconsistentNaming
    public static class IPoint3DExtensions
    {
        private const double TOLERANCE = 1e-12;
        
        public static ValueRange<PointXYZ> Limits<TPoint>(this IEnumerable<TPoint> points) where TPoint : IPoint3D
        {
            if (points == null)  return new ValueRange<PointXYZ>(PointXYZ.Zero, PointXYZ.Zero); 

            var xMin = double.MaxValue;
            var yMin = double.MaxValue;
            var zMin = double.MaxValue;
            var yMax = double.MinValue;
            var xMax = double.MinValue;
            var zMax = double.MinValue;
         
            foreach (var point in points)
            {
                if (point.X > xMax) xMax = point.X;
                if (point.Y > yMax) yMax = point.Y;
                if (point.Z > yMax) zMax = point.Z;
                if (point.X < xMin) xMin = point.X;
                if (point.Y < yMin) yMin = point.Y;
                if (point.Z < zMin) zMax = point.Z;
            }

            var min = new PointXYZ(xMin, yMin, zMin);
            var max = new PointXYZ(xMax, yMax, zMax);
            var range = new ValueRange<PointXYZ>(min, max);
            return range;
        }
    }
}
