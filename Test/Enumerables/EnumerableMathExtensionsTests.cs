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
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Visyn.Mathematics.Comparison;
using Visyn.Mathematics.Enumerables;

namespace Visyn.Mathematics.Test.Enumerables
{
    [TestFixture]
    class EnumerableMathExtensionsTests
    {
        public const double EPSILON = 1e-12;
        private double[] A = { 1.0, 2.0, 3.0 };
        private double[] B = { 2.0, 3.0, 4.0 };
        private double[] C = {-1.0, 5.0, 7.0, -10.0};
        private double[] A4 = { 1.0, 2.0, 3.0 ,4.0};
        private double[] AplusTiny = { 1.0+EPSILON/4.0, 2.0 + EPSILON / 4.0, 3.0 + EPSILON / 4.0 };

        private int[] Aints => A.Select(a => (int) a).ToArray();
        private int[] Bints => B.Select(b => (int)b).ToArray();
        private int[] Cints => C.Select(c => (int)c).ToArray();
        private int[] A4ints => A4.Select(a4 => (int)a4).ToArray();

        [Test]
        public void AddTests()
        {
            VerifyAddition(3, A, B, A.Add(B), "A plus B");
            VerifyAddition(3, B, A, B.Add(A), "B plus A");
            VerifyAddition(4, A, C, A.Add(C), "A plus C");
            VerifyAddition(4, C, B, C.Add(B), "C plus B");
        }

        [Test]
        public void AddConstantTests()
        {
            VerifyAddition(3, A, 7, A.Add(7), "A plus 7");
            VerifyAddition(3, B, 9.2, B.Add(9.2), "B plus 9.2");
            VerifyAddition(3, A, -88, A.Add(-88), "A plus -88");
            VerifyAddition(4, C, 17.26, C.Add(17.26), "C plus 17.26");
        }


        [Test]
        public void SubtractTests()
        {
            VerifySubtraction(3, A, B, A.Subtract(B), "A minus B");
            VerifySubtraction(3, B, A, B.Subtract(A), "B minus A");
            VerifySubtraction(4, A, C, A.Subtract(C), "A minus C");
            VerifySubtraction(4, C, B, C.Subtract(B), "C minus B");
        }
        [Test]
        public void SubtractConstantTests()
        {
            VerifySubtraction(3, A, 7, A.Subtract(7), "A minus 7");
            VerifySubtraction(3, B, 9.2, B.Subtract(9.2), "B minus 9.2");
            VerifySubtraction(3, A, -88, A.Subtract(-88), "A minus -88");
            VerifySubtraction(4, C, 17.26, C.Subtract(17.26), "C minus 17.26");
        }

        [Test]
        public void MultiplyIntsTests()
        {
            VerifyMultiplication(3, Aints, Bints, Aints.Multiply(Bints), "A * B");
            VerifyMultiplication(3, Bints, Aints, Bints.Multiply(Aints), "B * A");
            VerifyMultiplication(4, Aints, Cints, Aints.Multiply(Cints), "A * C");
            VerifyMultiplication(4, Cints, Bints, Cints.Multiply(Bints), "C * B");
        }

        [Test]
        public void MultiplyConstantIntsTests()
        {
            VerifyMultiplication(3, Aints, 7, Aints.Multiply(7), "A * 7");
            VerifyMultiplication(3, Bints, 9, Bints.Multiply(9), "B * 9.2");
            VerifyMultiplication(3, Aints, -88, Aints.Multiply(-88), "A * -88");
            VerifyMultiplication(4, Cints, (int)17.26, Cints.Multiply(17), "C * 17.26");
        }
        [Test]
        public void MultiplyIntsByDoubleTests()
        {
            VerifyMultiplication(3, Aints, Bints, Aints.Multiply(Bints), "A * B");
            VerifyMultiplication(3, Bints, Aints, Bints.Multiply(Aints), "B * A");
            VerifyMultiplication(4, Aints, Cints, Aints.Multiply(Cints), "A * C");
            VerifyMultiplication(4, Cints, Bints, Cints.Multiply(Bints), "C * B");
        }

        [Test]
        public void MultiplyIntsByConstantDoubleTests()
        {
            VerifyMultiplication(3, A, 7.0, Aints.Multiply(7.0), "A * 7");
            VerifyMultiplication(3, B, 9.7, Bints.Multiply(9.7), "B * 9.2");
            VerifyMultiplication(3, A, -88.0, Aints.Multiply(-88.0), "A * -88");
            VerifyMultiplication(4, C, 17.26, Cints.Multiply(17.26), "C * 17.26");
        }

        [Test]
        public void MultiplyDoublesTests()
        {
            VerifyMultiplication(3, A, B, A.Multiply(B), "A * B");
            VerifyMultiplication(3, B, A, B.Multiply(A), "B * A");
            VerifyMultiplication(4, A, C, A.Multiply(C), "A * C");
            VerifyMultiplication(4, C, B, C.Multiply(B), "C * B");
        }

        [Test]
        public void MultiplyConstantDoublesTests()
        {
            VerifyMultiplication(3, A, 7, A.Multiply(7), "A * 7");
            VerifyMultiplication(3, B, 9.2, B.Multiply(9.2), "B * 9.2");
            VerifyMultiplication(3, A, -88, A.Multiply(-88), "A * -88");
            VerifyMultiplication(4, C, 17.26, C.Multiply(17.26), "C * 17.26");
        }

        [Test]
        public void SquareIntsTests()
        {
            VerifyMultiplication(3, A, A, Aints.Squared<int>(), "A.Squared");
            VerifyMultiplication(3, B, B, Bints.Squared(), "B.Squared");
            VerifyMultiplication(4, C, C, Cints.Squared(), "C.Squared");
            VerifyMultiplication(4, A4, A4, A4ints.Squared(), "A4.Squared");
        }

        [Test]
        public void SquareDoublesTests()
        {
            VerifyMultiplication(3, A, A, A.Squared(), "A.Squared");
            VerifyMultiplication(3, B, B, B.Squared(), "B.Squared");
            VerifyMultiplication(4, C, C, C.Squared(), "C.Squared");
            VerifyMultiplication(4, A4, A4, A4.Squared(), "A4.Squared");
        }


        [Test]
        public void DivideTests()
        {
            VerifyDivision(3, A, B, A.Divide(B), "A / B");
            VerifyDivision(3, B, A, B.Divide(A), "B / A");
            VerifyDivision(4, A, C, A.Divide(C), "A / C");
            VerifyDivision(4, C, B, C.Divide(B), "C / B");
        }

        [Test]
        public void DivideConstantTests()
        {
            VerifyDivision(3, A, 7, A.Divide(7), "A / 7");
            VerifyDivision(3, B, 9.2, B.Divide(9.2), "B / 9.2");
            VerifyDivision(3, A, -88, A.Divide(-88), "A / -88");
            VerifyDivision(4, C, 17.26, C.Divide(17.26), "C / 17.26");
            VerifyDivision(4, C, 0, C.Divide(0.0), "C / 0");
        }

        [Test]
        public void ModTests()
        {
            VerifyModulus(3, A, B, A.Mod(B), "A % B");
            VerifyModulus(3, B, A, B.Mod(A), "B % A");
            VerifyModulus(4, A, C, A.Mod(C), "A % C");
            VerifyModulus(4, C, B, C.Mod(B), "C % B");
        }
        [Test]
        public void ModConstantTests()
        {
            VerifyModulus(3, A, 7, A.Mod(7), "A % 7");
            VerifyModulus(3, B, 9.2, B.Mod(9.2), "B % 9.2");
            VerifyModulus(3, A, -88, A.Mod(-88), "A % -88");
            VerifyModulus(4, C, 17.26, C.Mod(17.26), "C % 17.26");
            VerifyModulus(4, C, 0.0, C.Mod(0), "C % 0.0");
        }

        [Test]
        public void AbsTests()
        {
            VerifyAbsoluteValue(3, A, A.Abs(), "Abs(A)");
            VerifyAbsoluteValue(3, B, B.Abs(), "Abs(B)");
            VerifyAbsoluteValue(4, C, C.Abs(), "Abs(C)");
        }

        [Test]
        public void InvertTests()
        {
            VerifyInvertValue(3, A, A.Invert(), "Invert(A)");
            VerifyInvertValue(3, B, B.Invert(), "Invert(B)");
            VerifyInvertValue(4, C, C.Invert(), "Invert(C)");
        }



        [Test]
        public void ElementsAreEqualTests()
        {
            VerifyEquals(true, A, A, A.ElementsAreEqual(A,EPSILON), "A == A");
            VerifyEquals(false, A, B, A.ElementsAreEqual(B, EPSILON), "A != B");
            VerifyEquals(false, B, A, A.ElementsAreEqual(B, EPSILON), "B != A");
            VerifyEquals(false, A, A4, A.ElementsAreEqual(A4, EPSILON), "A[3] != A[4]");
            VerifyEquals(false, A4, A, A4.ElementsAreEqual(A, EPSILON), "a[4] != A[3]");
            VerifyEquals(true, A, AplusTiny, A.ElementsAreEqual(AplusTiny, EPSILON), "A[3] == A[3]+EPSILON/4");
            VerifyEquals(true, AplusTiny, A, AplusTiny.ElementsAreEqual(A, EPSILON), "A[3]+EPSILON/4 == A[3]");
        }

        [Test]
        public void ElementsAreEqual_AsIntsTests()
        {
            VerifyEquals(true, A, A, A.Select(i=>(int)i).ElementsAreEqual(A.Select(i=>(int)i), EPSILON), "A == A");
            VerifyEquals(false, A, B, A.Select(i => (int)i).ElementsAreEqual(B.Select(i => (int)i), EPSILON), "A != B");
            VerifyEquals(false, B, A, A.Select(i => (int)i).ElementsAreEqual(B.Select(i => (int)i), EPSILON), "B != A");
            VerifyEquals(false, A, A4, A.Select(i => (int)i).ElementsAreEqual(A4.Select(i => (int)i), EPSILON), "A[3] != A[4]");
            VerifyEquals(false, A4, A, A4.Select(i => (int)i).ElementsAreEqual(A.Select(i => (int)i), EPSILON), "a[4] != A[3]");
            VerifyEquals(true, A, AplusTiny, A.Select(i => (int)i).ElementsAreEqual(AplusTiny.Select(i => (int)i), EPSILON), "A[3] == A[3]+EPSILON/4");
            VerifyEquals(true, AplusTiny, A, AplusTiny.Select(i => (int)i).ElementsAreEqual(A.Select(i => (int)i), EPSILON), "A[3]+EPSILON/4 == A[3]");
        }

        [Test]
        public void ElementsAreEqual_AsStringsTests()
        {
            VerifyEquals(true, A, A, A.Select(AsStrings).ElementsAreEqual(A.Select(AsStrings), double.Parse, EPSILON), "A == A");
            VerifyEquals(false, A, B, A.Select(AsStrings).ElementsAreEqual(B.Select(AsStrings), double.Parse, EPSILON), "A != B");
            VerifyEquals(false, B, A, A.Select(AsStrings).ElementsAreEqual(B.Select(AsStrings), double.Parse, EPSILON), "B != A");
            VerifyEquals(false, A, A4, A.Select(AsStrings).ElementsAreEqual(A4.Select(AsStrings), double.Parse, EPSILON), "A[3] != A[4]");
            VerifyEquals(false, A4, A, A4.Select(AsStrings).ElementsAreEqual(A.Select(AsStrings), double.Parse, EPSILON), "a[4] != A[3]");
            VerifyEquals(true, A, AplusTiny, A.Select(AsStrings).ElementsAreEqual(AplusTiny.Select(AsStrings), double.Parse, EPSILON), "A[3] == A[3]+EPSILON/4");
            VerifyEquals(true, AplusTiny, A, AplusTiny.Select(AsStrings).ElementsAreEqual(A.Select(AsStrings), double.Parse, EPSILON), "A[3]+EPSILON/4 == A[3]");
        }

        private readonly Func<double,string> AsStrings = (d)=>d.ToString(CultureInfo.InvariantCulture);



        private void VerifyEquals(bool expected, double[] a, double[] b, bool result, string message)
        {
            if(a.Length != b.Length)  Assert.AreEqual(false,result);

            if(expected) CollectionAssert.AreEqual(a,b, CompareNumbers.Toleranced(EPSILON));
            else CollectionAssert.AreNotEqual(a,b, CompareNumbers.Toleranced(EPSILON));
            Assert.AreEqual(expected,result);
        }

        private void VerifyAddition(int expectedCount, double[] a, double[] b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);

            for (var i = 0; i < Math.Max(a.Length,b.Length); i++)
            {
                var da = i < a.Length ? a[i] : 0.0;
                var db = i < b.Length ? b[i] : 0.0;
                Assert.AreEqual(da + db, list[i], EPSILON, $"{message} [{i}] : {da}+{db}={list[i]}");
            }
        }
        private void VerifyAddition(int expectedCount, double[] a, double b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);

            for (var i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(a[i]+b, list[i], EPSILON, $"{message} [{i}] : {a[i]}-{b}={list[i]}");
            }
        }

        private void VerifySubtraction(int expectedCount,double[] a,double[] b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount,list.Count, message);
            for (var i = 0; i < Math.Max(a.Length, b.Length); i++)
            {
                var da = i < a.Length ? a[i] : 0.0;
                var db = i < b.Length ? b[i] : 0.0;
                Assert.AreEqual(da - db, list[i], EPSILON, $"{message} [{i}] : {da}-{db}={list[i]}");
            }
        }
        private void VerifySubtraction(int expectedCount, double[] a, double b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);

            for (var i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(a[i] - b, list[i], EPSILON, $"{message} [{i}] : {a[i]}-{b}={list[i]}");
            }
        }
        private void VerifyMultiplication(int expectedCount, int[] a, int[] b, IEnumerable<int> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);
            for (var i = 0; i < Math.Max(a.Length, b.Length); i++)
            {
                var da = i < a.Length ? a[i] : 0.0;
                var db = i < b.Length ? b[i] : 0.0;
                Assert.AreEqual(da * db, list[i], EPSILON, $"{message} [{i}] : {da}*{db}={list[i]}");
            }
        }

        private void VerifyMultiplication(int expectedCount, int[] a, int b, IEnumerable<int> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);

            for (var i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(a[i] * b, list[i], EPSILON, $"{message} [{i}] : {a[i]}*{b}={list[i]}");
            }
        }

        private void VerifyMultiplication(int expectedCount, double[] a, double[] b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);
            for (var i = 0; i < Math.Max(a.Length, b.Length); i++)
            {
                var da = i < a.Length ? a[i] : 0.0;
                var db = i < b.Length ? b[i] : 0.0;
                Assert.AreEqual(da * db, list[i], EPSILON, $"{message} [{i}] : {da}*{db}={list[i]}");
            }
        }

        private void VerifyMultiplication(int expectedCount, double[] a, double b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);

            for (var i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(a[i] * b, list[i], EPSILON, $"{message} [{i}] : {a[i]}*{b}={list[i]}");
            }
        }
        private void VerifyDivision(int expectedCount, double[] a, double[] b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);
            for (var i = 0; i < Math.Max(a.Length, b.Length); i++)
            {
                var da = i < a.Length ? a[i] : 0.0;
                var db = i < b.Length ? b[i] : 0.0;
                Assert.AreEqual(da / db, list[i], EPSILON, $"{message} [{i}] : {da}/{db}={list[i]}");
            }
        }
        private void VerifyDivision(int expectedCount, double[] a, double b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);

            for (var i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(a[i] / b, list[i], EPSILON, $"{message} [{i}] : {a[i]}/{b}={list[i]}");
            }
        }
        private void VerifyModulus(int expectedCount, double[] a, double[] b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);
            for (var i = 0; i < Math.Max(a.Length, b.Length); i++)
            {
                var da = i < a.Length ? a[i] : 0.0;
                var db = i < b.Length ? b[i] : 0.0;
                Assert.AreEqual(da % db, list[i], EPSILON, $"{message} [{i}] : {da}%{db}={list[i]}");
            }
        }
        private void VerifyModulus(int expectedCount, double[] a, double b, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);

            for (var i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(a[i] % b, list[i], EPSILON, $"{message} [{i}] : {a[i]}%{b}={list[i]}");
            }
        }
        private void VerifyAbsoluteValue(int expectedCount, double[] a, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);
            for (var i = 0; i < 3; i++)
            {
                Assert.AreEqual(Math.Abs(a[i]), list[i], EPSILON, $"{message} [{i}] : Abs({a[i]}={list[i]}");
            }
        }

        private void VerifyInvertValue(int expectedCount, double[] a, IEnumerable<double> result, string message)
        {
            var list = result.ToList();
            Assert.AreEqual(expectedCount, list.Count, message);
            for (var i = 0; i < 3; i++)
            {
                Assert.AreEqual(1.0/(a[i]), list[i], EPSILON, $"{message} [{i}] : Invert({a[i]}={list[i]}");
            }
        }
    }
}
