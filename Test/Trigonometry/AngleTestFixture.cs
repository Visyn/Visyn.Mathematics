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
using NUnit.Framework;
using Visyn.Mathematics.Trigonometry;

namespace Visyn.Mathematics.Test.Trigonometry
{
    [TestFixture]
    public class AngleTestFixture
    {
        public const double Tolerance = 1e-12;
        [Test]
        public void TestDegreesToRadians()
        {
            // Note 
            // PI/180 = 1
            //  PI/4 = 45
            //  PI/2 = 90
            //  PI = 180
            // 2*PI = 360
            //
            Assert.AreEqual(2.0*Math.PI,Angle.ToRadians(360.0),Tolerance);
            Assert.AreEqual(Math.PI / 4.0, Angle.ToRadians(45));
            Assert.AreEqual(Math.PI / 2.0, Angle.ToRadians(90));
            Assert.AreEqual(Math.PI, Angle.ToRadians(180));

            Assert.AreEqual(-2 * Math.PI, Angle.ToRadians(-360));
            Assert.AreEqual(-Math.PI / 4.0, Angle.ToRadians(-45));
            Assert.AreEqual(-Math.PI / 2.0, Angle.ToRadians(-90));
            Assert.AreEqual(-Math.PI, Angle.ToRadians(-180));
        }

        public void TestRadiansToDegrees()
        {
            // Note 
            // PI/180 = 1
            //  PI/4 = 45
            //  PI/2 = 90
            //  PI = 180
            // 2*PI = 360
            Assert.AreEqual(360.0, Angle.ToDegrees(2.0 * Math.PI));
            Assert.AreEqual(45.0, Angle.ToDegrees(Math.PI / 4.0),Tolerance);
            Assert.AreEqual(90.0, Angle.ToDegrees(Math.PI/2.0), Tolerance);
            Assert.AreEqual(180.0, Angle.ToDegrees(Math.PI), Tolerance);

            Assert.AreEqual(-360.0, Angle.ToDegrees(-2.0 * Math.PI));
            Assert.AreEqual(-45.0, Angle.ToDegrees(-Math.PI / 4.0), Tolerance);
            Assert.AreEqual(-90.0, Angle.ToDegrees(-Math.PI / 2.0), Tolerance);
            Assert.AreEqual(-180.0, Angle.ToDegrees(-Math.PI), Tolerance);
        }
    }
}
