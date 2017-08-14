using System;
using NUnit.Framework;
using Visyn.Mathematics.Geometry;

namespace Visyn.Mathematics.Test.Geometry
{
    [TestFixture()]
    public class RangeTests
    {
        [Test()]
        public void RangeTest()
        {


        }

        [Test()]
        public void RangeTest1()
        {
        }

        [Test()]
        public void IsValidTest()
        {
            Assert.IsTrue(new Range<double>(1.0,2.0).IsValid());
            Assert.IsTrue(new Range<int>(33, 44).IsValid());
            Assert.IsTrue(new Range<int>(-12,77).IsValid());

            Assert.Throws<ArgumentOutOfRangeException>(() => new Range<double>(-1.0, -2.0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Range<int>(33, 4));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Range<int>(12, -77));
        }

        [Test()]
        public void ContainsValueTest()
        {
            Assert.IsTrue(new Range<double>(1.0, 2.0).ContainsValue(1.5));
            Assert.IsTrue(new Range<int>(33, 44).ContainsValue(35));
            Assert.IsTrue(new Range<int>(-12, 77).ContainsValue(77));
            Assert.IsTrue(new Range<int>(-12, 77).ContainsValue(-12));

            Assert.IsFalse(new Range<double>(1.0, 2.0).ContainsValue(-1.5));
            Assert.IsFalse(new Range<int>(33, 44).ContainsValue(5));
            Assert.IsFalse(new Range<int>(-12, 77).ContainsValue(-13));
            Assert.IsFalse(new Range<int>(-12, 77).ContainsValue(78));
        }

        [Test()]
        public void IsInsideRangeTest()
        {
        }

        [Test()]
        public void ContainsRangeTest()
        {
        }

        [Test()]
        public void CompareToTest()
        {
        }

        [Test()]
        public void ToStringTest()
        {
        }

        [Test()]
        public void ToDelimitedStringTest()
        {
        }

        [Test()]
        public void DelimitedHeaderTest()
        {
        }

        public Range<T> create<T>(T min, T max) where T : IComparable<T>
        {
            var range = new Range<T>(min,max);

            Assert.AreEqual(min, range.Minimum);
            Assert.AreEqual(max,range.Maximum);
            //Assert.GreaterOrEqual();
            Assert.GreaterOrEqual(range.Maximum.CompareTo(range.Minimum),0);
            return range;
        }
    }
}