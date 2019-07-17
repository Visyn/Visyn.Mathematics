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
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Visyn.Mathematics.Rand;

namespace Visyn.Mathematics.Test.Statistic
{
    [TestFixture]
    public class StatisticsTests
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
                var listMean = MathExtensions.Mean(doubleList);
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
                listMean = doubleList.Mean();
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"Extension method MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
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
                var listMean = MathExtensions.Mean(intList);
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
                listMean = intList.Mean();
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"Extension method MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
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
                var listMean = MathExtensions.Mean(doubleList);
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
                listMean = doubleList.Mean();
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"Extension method MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
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
                var listMean = MathExtensions.Mean(intList);
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
                listMean = intList.Mean();
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"Extension method MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
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
                var listMean = MathExtensions.StdDev(doubleList);
                Assert.That(reference, Is.EqualTo(listMean).Within(reference * EPSILON),
                    $"MathExtensions.StdDev {listMean} != MathNet Mean {reference}");
                listMean = doubleList.StdDev();
                Assert.That(reference, Is.EqualTo(listMean).Within(reference * EPSILON),
                    $"Extension method MathExtensions.Mean {listMean} != MathNet Mean {reference}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
                var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null));
                Assert.NotNull(intList, $"MeanTest: intList is null!");
                Assert.AreEqual(intList.Count, Count, $"intList.Count {intList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                var listMean = MathExtensions.StdDev(intList);
                Assert.That(reference, Is.EqualTo(listMean).Within(0.1),
                    $"MathExtensions.Mean {listMean} != MathNet Mean {reference}");
                listMean = intList.StdDev();
                Assert.That(reference, Is.EqualTo(listMean).Within(0.1),
                    $"Extension method MathExtensions.Mean {listMean} != MathNet Mean {reference}");
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
            var statistics = Statistics.Calculate(nameof(StatisticsOfDoubleListTest), doubleList);
  //          var statistics = new Statistics(nameof(StatisticsOfDoubleListTest), doubleList);

            Assert.AreEqual(Count, statistics.SampleSize);
            {
                // verify mean calculation
                var referrenceMean = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                Assert.That(statistics.Mean / referrenceMean, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Mean {statistics.Mean} != MathNet Variance {referrenceMean}");
            }
            {
                // Verify Variance calculation
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                Assert.That(statistics.Variance / referenceVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Variance {statistics.Variance} != MathNet Variance {referenceVariance}");
            }
            {
                // Verify StdDev calculation
                var refereneStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                Assert.That(statistics.StandardDeviation / refereneStdDev, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.StandardDeviation {statistics.StandardDeviation} != MathNet StandardDeviation {refereneStdDev}");
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
        }
        [Test]
        public void StatisticsOfIntListTest()
        {
            // test ints
            var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
            var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null)).ToList();
            Assert.NotNull(intList, $"MeanTest: intList is null!");
            var statistics = Statistics.Calculate(nameof(StatisticsOfIntListTest), intList);
            Assert.AreEqual(Count, statistics.SampleSize);
            {
                // verify mean calculation
                var referrenceMean = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                Assert.That(statistics.Mean / referrenceMean, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Mean {statistics.Mean} != MathNet Variance {referrenceMean}");
            }
            {
                // Verify Variance calculation
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                Assert.That(statistics.Variance / referenceVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Variance {statistics.Variance} != MathNet Variance {referenceVariance}");
            }
            {
                // Verify StdDev calculation
                var refereneStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                Assert.That(statistics.StandardDeviation / refereneStdDev, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.StandardDeviation {statistics.StandardDeviation} != MathNet StandardDeviation {refereneStdDev}");
            }
			{
                // Verify Min calculation
                var referenceMinimum = MathNet.Numerics.Statistics.Statistics.Minimum(doubleList);
                Assert.That(statistics.Min / referenceMinimum, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Min {statistics.StandardDeviation} != MathNet StandardDeviation {referenceMinimum}");
            }
            {
                // Verify Max calculation
                var referenceMaximum = MathNet.Numerics.Statistics.Statistics.Maximum(doubleList);
                Assert.That(statistics.Max / referenceMaximum, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Max {statistics.StandardDeviation} != MathNet StandardDeviation {referenceMaximum}");
            }
        }


        [Test]
        public void StatisticsOfDoublesAsStringsListTest()
        {   // test doubles
            var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
            var stringList = doubleList.Select(d => d.ToString()).ToList();
            Assert.NotNull(doubleList, $"VarianceTest: DoubleList is null!");
            Assert.AreEqual(stringList.Count, 100, $"doubleList.Count {stringList.Count} != {Count}");
            //var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
            //var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
            var statistics = Statistics.Calculate(stringList);
            Assert.AreEqual(Count, statistics.SampleSize);
            {
                // verify mean calculation
                var referrenceMean = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                Assert.That(statistics.Mean / referrenceMean, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Mean {statistics.Mean} != MathNet Variance {referrenceMean}");
            }
            {
                // Verify Variance calculation
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                Assert.That(statistics.Variance / referenceVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Variance {statistics.Variance} != MathNet Variance {referenceVariance}");
            }
            {
                // Verify StdDev calculation
                var refereneStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                Assert.That(statistics.StandardDeviation / refereneStdDev, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.StandardDeviation {statistics.StandardDeviation} != MathNet StandardDeviation {refereneStdDev}");
            }
        }

        [Test]
        public void StatisticsOfIntsAsStringsListTest()
        {   // test ints
            var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
            var stringList = intList.Select((i) => i.ToString()).ToList();
            var doubleList = intList.Select(i => ((IConvertible)i).ToDouble(null));
            Assert.NotNull(stringList, $"MeanTest: stringList is null!");
            Assert.AreEqual(stringList.Count, Count, $"intList.Count {stringList.Count} != {Count}");
            // var reference = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
            // var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
            var statistics = Statistics.Calculate(stringList);
            Assert.AreEqual(Count, statistics.SampleSize);
            {
                // verify mean calculation
                var referrenceMean = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                Assert.That(statistics.Mean / referrenceMean, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Mean {statistics.Mean} != MathNet Variance {referrenceMean}");
            }
            {
                // Verify Variance calculation
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                Assert.That(statistics.Variance / referenceVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Variance {statistics.Variance} != MathNet Variance {referenceVariance}");
            }
            {
                // Verify StdDev calculation
                var refereneStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                Assert.That(statistics.StandardDeviation / refereneStdDev, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.StandardDeviation {statistics.StandardDeviation} != MathNet StandardDeviation {refereneStdDev}");
            }
        }
       

        [Test]
        public void StatisticWithConverterTest()
        {   // test doubles
            int funcCallCount = 0;
            Func<string, double> func = new Func<string, double>((s) => { funcCallCount++; return Convert.ToDouble(s); });

            var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
            var stringList = doubleList.Select(d => d.ToString()).ToList();
            Assert.NotNull(doubleList, $"VarianceTest: DoubleList is null!");
            Assert.AreEqual(stringList.Count, 100, $"doubleList.Count {stringList.Count} != {Count}");
            //var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
            //var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
            var statistics = Statistics.Calculate(stringList,func);
            
            Assert.AreEqual(Count, statistics.SampleSize);
            Assert.AreEqual(Count, funcCallCount);
            {
                // verify mean calculation
                var referrenceMean = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                Assert.That(statistics.Mean / referrenceMean, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Mean {statistics.Mean} != MathNet Variance {referrenceMean}");
            }
            {
                // Verify Variance calculation
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                Assert.That(statistics.Variance / referenceVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.Variance {statistics.Variance} != MathNet Variance {referenceVariance}");
            }
            {
                // Verify StdDev calculation
                var refereneStdDev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(doubleList);
                Assert.That(statistics.StandardDeviation / refereneStdDev, Is.EqualTo(1.0).Within(EPSILON),
                    $"Visyn.Mathematics.Statistics.StandardDeviation {statistics.StandardDeviation} != MathNet StandardDeviation {refereneStdDev}");
            }
        }
    }
}
