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
using Visyn.Collection;
using Visyn.Mathematics.Rand;

namespace Visyn.Mathematics.Test.Statistic
{
    [TestFixture]
    public class MathExtensionsStatisticsTests
    {
        private const double EPSILON = 1e-14;
        const int Count = 100;
        private readonly IRandom _rng;

        private IRandom rng => _rng ?? Rng<BoxMuller>.ThreadSafeRandom(null, Thread.CurrentThread.ManagedThreadId);

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
                var doubleEnumerable = rng.InclusiveList(-1e12, 1e12, Count).ToEnumerable<double, double>();
                Assert.NotNull(doubleEnumerable, $"MeanTest: DoubleList is null!");
                //          Assert.AreEqual(doubleList.Count, count, $"doubleList.Count {doubleList.Count} != {count}");
                var reference = MathNet.Numerics.Statistics.ArrayStatistics.Mean(doubleEnumerable.ToArray());
                var referenceStreaming = MathNet.Numerics.Statistics.StreamingStatistics.Mean(doubleEnumerable);
                Assert.AreEqual(reference / referenceStreaming, 1.0, EPSILON);
                var listMean = MathExtensions.Mean(doubleEnumerable);
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
                listMean = doubleEnumerable.Mean();
                Assert.That(listMean / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"Extension method MathExtensions.Mean {listMean} != MathNet Mean {reference} Difference: {listMean - reference}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MaxValue / 10, int.MinValue / 10, Count).ToEnumerable<int, int>();
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

        [Test]
        public void VarianceTest()
        {
            {
                // test doubles
                var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
                Assert.NotNull(doubleList, $"VarianceTest: DoubleList is null!");
                Assert.AreEqual(doubleList.Count, 100, $"doubleList.Count {doubleList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var listVariance = MathExtensions.Variance(doubleList);
                Assert.That(reference / listVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance {listVariance} != MathNet Variance {reference}");
                listVariance = doubleList.Variance<double>();
                Assert.That(reference, Is.EqualTo(listVariance).Within(reference * EPSILON),
                    $"Extension method MathExtensions.Variance {listVariance} != MathNet Variance {reference}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
                var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null));
                Assert.NotNull(intList, $"MeanTest: intList is null!");
                Assert.AreEqual(intList.Count, Count, $"intList.Count {intList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var listVariance = MathExtensions.Variance(intList);
                Assert.That(reference, Is.EqualTo(listVariance).Within(reference * EPSILON),
                    $"MathExtensions.Mean {listVariance} != MathNet Mean {reference}");
                listVariance = intList.Variance<int>();
                Assert.That(reference, Is.EqualTo(listVariance).Within(reference * EPSILON),
                    $"Extension method MathExtensions.Mean {listVariance} != MathNet Mean {reference}");
            }
        }

        [Test]
        public void VarianceAndAverageTest()
        {
            {
                // test doubles
                var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
                Assert.NotNull(doubleList, $"VarianceTest: DoubleList is null!");
                Assert.AreEqual(doubleList.Count, 100, $"doubleList.Count {doubleList.Count} != {Count}");
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                var listAverage = 0.0;
                var listVariance = MathExtensions.Variance(doubleList, out listAverage);
                Assert.That(referenceVariance / listVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) {listVariance} != MathNet Variance {referenceVariance}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage}");
                listVariance = doubleList.Variance<double>(out listAverage);
                Assert.That(referenceVariance, Is.EqualTo(listVariance).Within(referenceVariance * EPSILON),
                    $"Extension method MathExtensions.Variance(out ave) {listVariance} != MathNet Variance {referenceVariance}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(1e-15),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
                var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null));
                Assert.NotNull(intList, $"MeanTest: intList is null!");
                Assert.AreEqual(intList.Count, Count, $"intList.Count {intList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                var listAverage = 0.0;
                var listVariance = MathExtensions.Variance(intList, out listAverage);
                Assert.That(listVariance / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Mean {listVariance} != MathNet Mean {reference} Difference: {listVariance - reference}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage} Difference: {listAverage - referenceAverage}");
                listVariance = intList.Variance(out listAverage);
                Assert.That(listVariance / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"Extension method MathExtensions.Mean {listVariance} != MathNet Mean {reference} Difference: {listVariance - reference}");
                Assert.That(listAverage / referenceAverage, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage} Difference: {listAverage - referenceAverage}");
            }
        }

        [Test]
        public void VarianceAverageAndCountTest()
        {
            {
                // test doubles
                var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
                Assert.NotNull(doubleList, $"VarianceTest: DoubleList is null!");
                Assert.AreEqual(doubleList.Count, 100, $"doubleList.Count {doubleList.Count} != {Count}");
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                var listAverage = 0.0;
                var varianceCount = 0;
                var listVariance = MathExtensions.Variance(doubleList, out listAverage, out varianceCount);
                Assert.That(referenceVariance / listVariance, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) {listVariance} != MathNet Variance {referenceVariance}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage}");
                listVariance = doubleList.Variance<double>(out listAverage, out varianceCount);
                Assert.That(referenceVariance, Is.EqualTo(listVariance).Within(referenceVariance * EPSILON),
                    $"Extension method MathExtensions.Variance(out ave) {listVariance} != MathNet Variance {referenceVariance}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(1e-15),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage}");
            }
            {
                // test ints
                var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
                var doubleList = intList.Select(i => ((IConvertible) i).ToDouble(null));
                Assert.NotNull(intList, $"MeanTest: intList is null!");
                Assert.AreEqual(intList.Count, Count, $"intList.Count {intList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                var listAverage = 0.0;
                var varianceCount = 0;
                var listVariance = MathExtensions.Variance(intList, out listAverage, out varianceCount);
                Assert.That(listVariance / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Mean {listVariance} != MathNet Mean {reference} Difference: {listVariance - reference}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage} Difference: {listAverage - referenceAverage}");
                listVariance = intList.Variance(out listAverage, out varianceCount);
                Assert.AreEqual(Count, varianceCount);
                Assert.That(listVariance / reference, Is.EqualTo(1.0).Within(EPSILON),
                    $"Extension method MathExtensions.Mean {listVariance} != MathNet Mean {reference} Difference: {listVariance - reference}");
                Assert.That(listAverage / referenceAverage, Is.EqualTo(1.0).Within(EPSILON),
                    $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage} Difference: {listAverage - referenceAverage}");
            }
        }

        private static Func<string, double> func = Convert.ToDouble;

        [Test]
        public void VarianceFromStringsTest()
        {
            {   // test doubles
                var doubleList = rng.InclusiveList(-1e12, 1e12, Count);
                var stringList = doubleList.Select(d => d.ToString()).ToList();
                Assert.NotNull(doubleList, $"VarianceTest: DoubleList is null!");
                Assert.AreEqual(stringList.Count, 100, $"doubleList.Count {stringList.Count} != {Count}");
                var referenceVariance = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                var listAverage = 0.0;
                var varianceCount = 0;
                var listVariance = MathExtensions.Variance(stringList, out listAverage, out varianceCount, func);
                Assert.That(referenceVariance / listVariance, Is.EqualTo(1.0).Within(EPSILON), $"MathExtensions.Variance(out ave) {listVariance} != MathNet Variance {referenceVariance}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(EPSILON), $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage}");
                listVariance = stringList.Variance<string>(out listAverage, out varianceCount,func);
                Assert.That(referenceVariance, Is.EqualTo(listVariance).Within(referenceVariance * EPSILON), $"Extension method MathExtensions.Variance(out ave) {listVariance} != MathNet Variance {referenceVariance}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(1e-15), $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage}");
            }
            {   // test ints
                var intList = rng.InclusiveList(int.MinValue / 2, int.MaxValue / 2, Count);
                var stringList = intList.Select((i) => i.ToString()).ToList();
                var doubleList = intList.Select(i => ((IConvertible)i).ToDouble(null));
                Assert.NotNull(stringList, $"MeanTest: stringList is null!");
                Assert.AreEqual(stringList.Count, Count, $"intList.Count {stringList.Count} != {Count}");
                var reference = MathNet.Numerics.Statistics.Statistics.Variance(doubleList);
                var referenceAverage = MathNet.Numerics.Statistics.Statistics.Mean(doubleList);
                var listAverage = 0.0;
                var varianceCount = 0;
                var listVariance = MathExtensions.Variance(stringList, out listAverage, out varianceCount,func);
                Assert.That(listVariance / reference, Is.EqualTo(1.0).Within(EPSILON), $"MathExtensions.Mean {listVariance} != MathNet Mean {reference} Difference: {listVariance - reference}");
                Assert.That(referenceAverage / listAverage, Is.EqualTo(1.0).Within(EPSILON), $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage} Difference: {listAverage - referenceAverage}");
                listVariance = stringList.Variance(out listAverage, out varianceCount,func);
                Assert.AreEqual(Count, varianceCount);
                Assert.That(listVariance / reference, Is.EqualTo(1.0).Within(EPSILON), $"Extension method MathExtensions.Mean {listVariance} != MathNet Mean {reference} Difference: {listVariance - reference}");
                Assert.That(listAverage / referenceAverage, Is.EqualTo(1.0).Within(EPSILON), $"MathExtensions.Variance(out ave) Average={listAverage} != MathNet Average {referenceAverage} Difference: {listAverage - referenceAverage}");
            }
        }
    }
}
