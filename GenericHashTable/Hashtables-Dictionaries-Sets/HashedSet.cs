namespace HashtablesDictionariesSets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class HashedSet<T> : IHashedSet<T>
    {
        private IDictionary<int, T> elements;

        public HashedSet(IDictionary<int, T> foundation)
        {
            this.elements = foundation;
        }

        public HashedSet()
            : this(new HashTable<int, T>())
        {
        }

        public int Count
        {
            get
            {
                return this.elements.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.elements.IsReadOnly;
            }
        }

        public void Add(T item)
        {
            if (!this.elements.Contains(new KeyValuePair<int, T>(item.GetHashCode(), item)))
            {
                this.elements.Add(item.GetHashCode(), item);
            }
        }

        public void Clear()
        {
            this.elements.Clear();
        }

        public bool Contains(T item)
        {
            return this.elements.ContainsKey(item.GetHashCode());
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var iterator = this.elements.GetEnumerator();

            while (arrayIndex < array.Length && iterator.MoveNext())
            {
                array[arrayIndex] = iterator.Current.Value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.elements)
            {
                yield return item.Value;
            }
        }

        public bool Remove(T item)
        {
            return this.elements.Remove(item.GetHashCode());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IHashedSet<T> UnionWith(ICollection<T> collection)
        {
            var result = new HashedSet<T>();

            foreach (var item in this)
            {
                result.Add(item);
            }

            foreach (var item in collection)
            {
                result.Add(item);
            }

            return result;
        }

        public IHashedSet<T> IntersectWith(ICollection<T> collection)
        {
            var result = new HashedSet<T>();

            foreach (var item in collection)
            {
                if (this.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}