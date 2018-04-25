using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Visyn.Mathematics
{
    public class CollectionStatistics<T, U> : IStatistics, ICollection<U>
        where T : ICollection<U> 
        where U: IComparable, IComparable<U>, IEquatable<U>
    {
        public double Mean => _collection.Mean();
        public double Variance => _collection.Variance();
        public double StandardDeviation => _collection.StdDev();
        public double Min => Convert.ToDouble(_collection.Min());
        public double Max => Convert.ToDouble(_collection.Max());
        public int SampleSize => _collection.Count;

        private readonly ICollection<U> _collection;
        public CollectionStatistics(T collection)
        {
            _collection = collection;
        }


        public IEnumerator<U> GetEnumerator() => _collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _collection).GetEnumerator();

        public void Add(U item)
        {
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(U item) => _collection.Contains(item);

        public void CopyTo(U[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(U item) => _collection.Remove(item);

        public int Count => _collection.Count;

        public bool IsReadOnly => _collection.IsReadOnly;
    }
}