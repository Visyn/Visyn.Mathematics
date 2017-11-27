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
using Visyn.Comparison;
using Visyn.Geometry;

namespace Visyn.Mathematics.Geometry
{
    public class Polygon
    {

        /// <summary>
        /// Refrence: https://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
        /// </summary>
        /// <param name="polygonPoints">Points of polygon</param>
        /// <param name="test">Point to test for inside/outside</param>
        /// <returns>true is inside, false if outside</returns>
        [Obsolete("Unreliable.  Needs to be fixed...")]
        public static bool InsidePolygon(IList<IPoint> polygonPoints, IPoint test)
        {
            int i, j;
            var c = false;
            for (i = 0, j = polygonPoints.Count - 1; i < polygonPoints.Count; j = i++)
            {
                if (((polygonPoints[i].Y > test.Y) != (polygonPoints[j].Y > test.Y)) &&
                 (test.X < (polygonPoints[j].X - polygonPoints[i].X) * (test.X - polygonPoints[i].Y) / (polygonPoints[j].Y - polygonPoints[i].Y) + polygonPoints[i].X))
                    c = !c;
            }
            return c;
        }


        public static IList<KeyValuePair<double, IPoint>> OrderPoints(IList<IPoint> contenders, IPoint test)
        {
            var orderedPoints = new List<KeyValuePair<double, IPoint>>(contenders.Count);
            foreach (var point in contenders)
            {
                var distance = Math.Sqrt(Math.Pow(point.X - test.X, 2) + Math.Pow(point.Y - test.Y, 2));
                orderedPoints.Add(new KeyValuePair<double, IPoint>(distance, point));
            }

            orderedPoints.Sort(KeyComparer < double, IPoint > ());
            return orderedPoints;
        }


        private static IComparer<KeyValuePair<TKey, TValue>> KeyComparer<TKey, TValue>()
        {
            return new CustomComparer<KeyValuePair<TKey, TValue>>((kvp1, kvp2)
                => Comparer<TKey>.Default.Compare(kvp1.Key, kvp2.Key));
        }


#if false
        /// <summary>
        /// Refrence: https://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
        /// </summary>
        /// <param name="polygonPoints">Points of polygon</param>
        /// <param name="test">Point to test for inside/outside</param>
        /// <returns>true is inside, false if outside</returns>
        public static bool InsidePolygon(IList<Point> polygonPoints, Point test)
        {
            int i, j;
            var c = false;
            for (i = 0, j = polygonPoints.Count - 1; i < polygonPoints.Count; j = i++)
            {
                if (((polygonPoints[i].Y > test.Y) != (polygonPoints[j].Y > test.Y)) &&
                 (test.X < (polygonPoints[j].X - polygonPoints[i].X) * (test.X - polygonPoints[i].Y) / (polygonPoints[j].Y - polygonPoints[i].Y) + polygonPoints[i].X))
                    c = !c;
            }
            return c;
        }

        public static IEnumerable<Point> NearestPoints(IList<Point> contenders, Point test, int desired)
        {
            return OrderPoints(contenders, test).Values().Take(desired);
        }

        public static IList<KeyValuePair<double, Point>> OrderPoints(IList<Point> contenders, Point test)
        {
            //var orderedPoints = new SortedList<double, Point>();
            var orderedPoints = new List<KeyValuePair<double, Point>>(contenders.Count);
            foreach (var point in contenders)
            {
                var distance = Math.Sqrt(Math.Pow(point.X - test.X, 2) + Math.Pow(point.Y - test.Y, 2));
                orderedPoints.Add(new KeyValuePair<double, Point>(distance, point));
            }
            //var keyComparer = new  CustomComparer<KeyValuePair<double, Point>>(new Func<KeyValuePair<double, Point>, KeyValuePair<double, Point>, int>((kvp1, kvp2) => { return Comparer<double>.Default.Compare(kvp1.Key, kvp2.Key); }));
            orderedPoints.Sort(orderedPoints.KeyComparer());
            return orderedPoints;
            //return null;
        }
#endif
    }
}
