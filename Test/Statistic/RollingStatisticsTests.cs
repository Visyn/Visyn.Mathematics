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
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Visyn.Mathematics.Rand;
// ReSharper disable PossibleMultipleEnumeration

namespace Visyn.Mathematics.Test.Statistic
{
    [TestFixture]
    public class RollingStatisticsTests
    {
        private const double EPSILON = 1e-14;
        const int Count = 100;
        private readonly IRandom _rng;

        private IRandom rng => _rng ?? Rng<BoxMuller>.ThreadSafeRandom(null, Thread.CurrentThread.ManagedThreadId);
#if true
        [Test]
        public void MeanTest()
        {
            {
                // test doubles
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
                Assert.NotNull(doubleList, $"MeanTest: DoubleList is null!");
                Assert.AreEqual(doubleList.Count, Count, $"doubleList.Count {doubleList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.ArrayStatistics.Mean(doubleList.ToArray());
                var referenceStreaming = MathNet.Numerics.Statistics.StreamingStatistics.Mean(doubleList);
                Assert.AreEqual(reference / referenceStreaming, 1.0, EPSILON);

                var stats = new RollingStatistics<double>(doubleList);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.Mean {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
                stats = new RollingStatistics<double>(doubleList[0]);
                for(int i=1;i<doubleList.Count;i++) stats.Add(doubleList[i]);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.Mean {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MaxValue / 10, int.MinValue / 10, Count);
                var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null));
                Assert.NotNull(intList, $"MeanTest: intList is null!");
                Assert.AreEqual(intList.Count, 100, $"intList.Count {intList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.ArrayStatistics.Mean(doubleList.ToArray());
                var referenceStreaming = MathNet.Numerics.Statistics.StreamingStatistics.Mean(doubleList);
                Assert.AreEqual(reference / referenceStreaming, 1.0, EPSILON);

                var stats = new RollingStatistics<int>(intList);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.Mean {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
                stats = new RollingStatistics<int>(intList[0]);
                for (int i = 1; i < intList.Count; i++) stats.Add(intList[i]);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.Mean {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
            }
        }

        [Test]
        public void MeanOfIEnumerableTest()
        {
            {
                // test doubles
                var threadId = Thread.CurrentThread.ManagedThreadId;
                var doubleList = rng.InclusiveList(-1e12, 1e12, Count).ToList();
                Assert.NotNull(doubleList, $"MeanTest: DoubleList is null!");
                //          Assert.AreEqual(doubleList.Count, count, $"doubleList.Count {doubleList.Count} != {count}");
                var reference = MathNet.Numerics.Statistics.ArrayStatistics.Mean(doubleList.ToArray());
                var referenceStreaming = MathNet.Numerics.Statistics.StreamingStatistics.Mean(doubleList);
                Assert.AreEqual(reference / referenceStreaming, 1.0, EPSILON);

                var stats = new RollingStatistics<double>(doubleList);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double> {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
                stats = new RollingStatistics<double>();
                stats.Add(doubleList);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double> {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MaxValue / 10, int.MinValue / 10, Count).ToList();
                ;
                var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null));
                Assert.NotNull(intList, $"MeanTest: intList is null!");
                //               Assert.AreEqual(intList.Count, 100, $"intList.Count {intList.Count} != {count}");
                var reference = MathNet.Numerics.Statistics.ArrayStatistics.Mean(doubleList.ToArray());
                var referenceStreaming = MathNet.Numerics.Statistics.StreamingStatistics.Mean(doubleList);
                Assert.AreEqual(reference / referenceStreaming, 1.0, EPSILON);

                var stats = new RollingStatistics<int>(intList);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double> {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
                stats = new RollingStatistics<int>();
                stats.Add(intList);
                Assert.That(stats.Mean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double> {stats.Mean} != MathNet Mean {reference} Difference: {stats.Mean - reference}");
            }
        }

        [Test]
        public void StdDevTest()
        {
            {
                // test doubles
                var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
                Assert.NotNull(doubleList, $"StdDevTest: DoubleList is null!");
                Assert.AreEqual(doubleList.Count, 100, $"doubleList.Count {doubleList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);

                var stats = new RollingStatistics<double>(doubleList);
                Assert.That(reference, Is.EqualTo(stats.StandardDeviation).Within(reference * EPSILON),
                    $"RollingStatistics<double>.StdDev {stats.StandardDeviation} != MathNet StdDev {reference}");
                stats = new RollingStatistics<double>(doubleList[0]);
                for (int i = 1; i < doubleList.Count; i++) stats.Add(doubleList[i]);
                Assert.That(reference, Is.EqualTo(stats.StandardDeviation).Within(reference * EPSILON),
                    $"RollingStatistics<double>.StdDev {stats.StandardDeviation} != MathNet StdDev {reference}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
                var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null));
                Assert.NotNull(intList, $"MeanTest: intList is null!");
                Assert.AreEqual(intList.Count, Count, $"intList.Count {intList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);

                var stats = new RollingStatistics<int>(intList);
                Assert.That(reference, Is.EqualTo(stats.StandardDeviation).Within(reference * EPSILON),
                    $"RollingStatistics<int>.StdDev {stats.StandardDeviation} != MathNet StdDev {reference}");
                stats = new RollingStatistics<int>(intList[0]);
                for (int i = 1; i < intList.Count; i++) stats.Add(intList[i]);
                Assert.That(reference, Is.EqualTo(stats.StandardDeviation).Within(reference * EPSILON),
                    $"RollingStatistics<int>.StdDev {stats.StandardDeviation} != MathNet StdDev {reference}");
            }
        }
#endif

        [Test]
        public void StatisticsOfDoubleListTest()
        {
            // test doubles
            var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
            Assert.NotNull(doubleList, $"VarianceTest: DoubleList is null!");
            Assert.AreEqual(doubleList.Count, 100, $"doubleList.Count {doubleList.Count} != {Count}");
            var statistics = new RollingStatistics<double>(doubleList);

            Assert.AreEqual(Count, statistics.SampleSize);
            {
                // verify mean calculation
                var referrenceMean = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                Assert.That(statistics.Mean / referrenceMean, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.Mean {statistics.Mean} != MathNet Variance {referrenceMean}");
            }
            {
                // Verify Variance calculation
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                Assert.That(statistics.Variance / referenceVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.Variance {statistics.Variance} != MathNet Variance {referenceVariance}");
            }
            {
                // Verify StdDev calculation
                var refereneStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                Assert.That(statistics.StandardDeviation / refereneStdDev, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.StandardDeviation {statistics.StandardDeviation} != MathNet StandardDeviation {refereneStdDev}");
            }
            {
                // Verify Min calculation
                var referenceMinimum = MathNet.Numerics.Statistics.Statistics.Minimum(doubleList);
                Assert.That(statistics.Min / referenceMinimum, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.Min {statistics.StandardDeviation} != MathNet StandardDeviation {referenceMinimum}");
            }
            {
                // Verify Max calculation
                var referenceMaximum = MathNet.Numerics.Statistics.Statistics.Maximum(doubleList);
                Assert.That(statistics.Max / referenceMaximum, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.Max {statistics.StandardDeviation} != MathNet StandardDeviation {referenceMaximum}");
            }
            {
                // Verify SampleSize calculation
                var referenceCount = doubleList.Count;
                Assert.That(statistics.SampleSize / referenceCount, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<double>.SampleSize {statistics.StandardDeviation} != MathNet StandardDeviation {referenceCount}");
            }
        }
        [Test]
        public void StatisticsOfIntListTest()
        {
            // test ints
            var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
            var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null)).ToList();
            Assert.NotNull(intList, $"MeanTest: intList is null!");
            var statistics = new RollingStatistics<int>( intList);
            Assert.AreEqual(Count, statistics.SampleSize);
            {
                // verify mean calculation
                var referrenceMean = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                Assert.That(statistics.Mean / referrenceMean, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.Mean {statistics.Mean} != MathNet Variance {referrenceMean}");
            }
            {
                // Verify Variance calculation
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                Assert.That(statistics.Variance / referenceVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.Variance {statistics.Variance} != MathNet Variance {referenceVariance}");
            }
            {
                // Verify StdDev calculation
                var refereneStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                Assert.That(statistics.StandardDeviation / refereneStdDev, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.StandardDeviation {statistics.StandardDeviation} != MathNet StandardDeviation {refereneStdDev}");
            }
            {   // Verify Min calculation
                var referenceMinimum = MathNet.Numerics.Statistics.Statistics.Minimum(doubleList);
                Assert.That(statistics.Min / referenceMinimum, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.Min {statistics.StandardDeviation} != MathNet StandardDeviation {referenceMinimum}");
            }
            {   // Verify Max calculation
                var referenceMaximum = MathNet.Numerics.Statistics.Statistics.Maximum(doubleList);
                Assert.That(statistics.Max / referenceMaximum, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.Max {statistics.StandardDeviation} != MathNet StandardDeviation {referenceMaximum}");
            }
            {   // Verify SampleSize calculation
                var referenceCount = doubleList.Count;
                Assert.That(statistics.SampleSize / referenceCount, Is.EqualTo(1.0).Within(EPSILON),
                    $"RollingStatistics<int>.SampleSize {statistics.StandardDeviation} != MathNet StandardDeviation {referenceCount}");
            }
        }

        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<NullReferenceException>(() => { new RollingStatistics<double>(null); });
            Assert.Throws<NullReferenceException>(() => { new RollingStatistics<int>(null as IEnumerable<int>); });
        }

        [Test]
        public void InitializationDoubleZeroTests()
        {
            var double0 = new RollingStatistics<double>();

            Assert.AreEqual(0,double0.SampleSize);
            Assert.AreEqual(0,double0.Mean);
            Assert.AreEqual(double.MaxValue,double0.Min);
            Assert.AreEqual(double.MinValue,double0.Max);
            Assert.AreEqual(0,double0.Mean);
            Assert.AreEqual(double.NaN,double0.Variance);
            Assert.AreEqual(double.NaN,double0.StandardDeviation);
        }

        [Test]
        public void InitializationDoubleOneTests()
        {
            var double0 = new RollingStatistics<double>(7);

            Assert.AreEqual(1, double0.SampleSize);
            Assert.AreEqual(7, double0.Mean);
            Assert.AreEqual(7, double0.Min);
            Assert.AreEqual(7, double0.Max);
            Assert.AreEqual(7, double0.Mean);
            Assert.AreEqual(double.NaN, double0.Variance);
            Assert.AreEqual(double.NaN, double0.StandardDeviation);
        }

        [Test]
        public void InitializationIntZeroTests()
        {
            var int0 = new RollingStatistics<int>();

            Assert.AreEqual(0, int0.SampleSize);
            Assert.AreEqual(0, int0.Mean);
            Assert.AreEqual(double.MaxValue, int0.Min);
            Assert.AreEqual(double.MinValue, int0.Max);
            Assert.AreEqual(0, int0.Mean);
            Assert.AreEqual(double.NaN, int0.Variance);
            Assert.AreEqual(double.NaN, int0.StandardDeviation);
        }

        [Test]
        public void InitializationIntOneTests()
        {
            var int0 = new RollingStatistics<double>(7);

            Assert.AreEqual(1, int0.SampleSize);
            Assert.AreEqual(7, int0.Mean);
            Assert.AreEqual(7, int0.Min);
            Assert.AreEqual(7, int0.Max);
            Assert.AreEqual(7, int0.Mean);
            Assert.AreEqual(double.NaN, int0.Variance);
            Assert.AreEqual(double.NaN, int0.StandardDeviation);
        }
    }

}
