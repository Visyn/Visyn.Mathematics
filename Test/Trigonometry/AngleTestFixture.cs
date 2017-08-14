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
