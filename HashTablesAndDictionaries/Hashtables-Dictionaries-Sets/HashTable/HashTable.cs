namespace HashtablesDictionariesSets.HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HashTable<K, V> : IDictionary<K, V>
    {
        private const int ExpansionCoefficient = 2;
        private const double AllowedOccupationPercentage = 0.75D;
        private const int DefaultCapacity = 16;

        private LinkedList<KeyValuePair<K, V>>[] elements;

        private ICollection<K> keys;

        public HashTable()
        {
            this.Clear();
            this.keys = new HashSet<K>();
        }

        public V this[K key]
        {
            get
            {
                //var result = this.elements[this.GetNormalizedHash(key)]
                //                                .First(x => x.Key.Equals(key))
                //                                .Value;

                V result = default(V);

                var keyExists = this.TryGetValue(key, out result);

                if(!keyExists)
                {
                    throw new KeyNotFoundException("The provided key was not in the dictionary");
                }

                return result;
            }

            set
            {
                var container = this.elements[this.GetNormalizedHash(key)];

                if(container == null)
                {
                    this.Add(new KeyValuePair<K, V>(key, value));
                    return;
                }

                var current = container.First;

                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        current.Value = new KeyValuePair<K, V>(key, value);
                        return;
                    }

                    current = current.Next;
                }
            }
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return this.elements.Length;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<K> Keys
        {
            get
            {
                return this.keys;
            }
        }

        public ICollection<V> Values
        {
            get
            {
                var result = new List<V>();

                foreach (var k in this.Keys)
                {
                    foreach (var element in this.elements[this.GetNormalizedHash(k)])
                    {
                        result.Add(element.Value);
                    }
                }

                return result;
            }
        }

        public void Add(KeyValuePair<K, V> item)
        {
            if (this.Keys.Contains(item.Key))
            {
                throw new InvalidOperationException("An item with the same key has already been added.");
            }

            this.Keys.Add(item.Key);

            if (this.elements.Length * AllowedOccupationPercentage <= this.Count)
            {
                this.ResizeTo(this.elements.Length * ExpansionCoefficient);
            }

            if (this.elements[this.GetNormalizedHash(item.Key)] == null)
            {
                this.elements[this.GetNormalizedHash(item.Key)] = new LinkedList<KeyValuePair<K, V>>();
            }

            this.elements[this.GetNormalizedHash(item.Key)].AddLast(item);
            this.Count++;
        }

        public void Add(K key, V value)
        {
            this.Add(new KeyValuePair<K, V>(key, value));
        }

        public void Clear()
        {
            this.elements = new LinkedList<KeyValuePair<K, V>>[DefaultCapacity];
            this.Count = 0;
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return this.elements[this.GetNormalizedHash(item.Key)] != null
                && this.elements[this.GetNormalizedHash(item.Key)].Any(x => x.Equals(item));
        }

        public bool ContainsKey(K key)
        {
            return this.Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; ;)
            {
                foreach (var k in this.Keys)
                {
                    foreach (var element in this.elements[k.GetHashCode() % this.elements.Length])
                    {
                        if (i >= array.Length)
                        {
                            return;
                        }

                        array[i++] = element;
                    }
                }
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            foreach (var k in this.Keys)
            {
                foreach (var element in this.elements[this.GetNormalizedHash(k)])
                {
                    yield return element;
                }
            }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return this.Remove(item.Key, item.Value, true);
        }

        public bool Remove(K key)
        {
            return this.Remove(key, default(V), false);
        }

        public bool TryGetValue(K key, out V value)
        {
            var container = this.elements[this.GetNormalizedHash(key)];
            var exists = container != null && container.Any(x => x.Key.Equals(key));

            value = exists ? container.First(x => x.Key.Equals(key)).Value : default(V);

            return exists;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var kvp in this)
            {
                result.Append(kvp);
                result.Append(", ");
            }

            return result.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool Remove(K key, V value, bool shouldRemoveValue)
        {
            if (this.elements.Length * AllowedOccupationPercentage / 2 > this.Count)
            {
                this.ResizeTo(this.elements.Length / ExpansionCoefficient);
            }

            this.Keys.Remove(key);

            var container = this.elements[this.GetNormalizedHash(key)];

            if(container == null)
            {
                return false;
            }

            var current = container.First;

            while (current != null)
            {
                if (current.Value.Key.Equals(key))
                {
                    if (!shouldRemoveValue || current.Value.Value.Equals(value))
                    {
                        container.Remove(current);
                        this.Count--;
                        return true;
                    }
                }

                current = current.Next;
            }

            return false;
        }

        private void ResizeTo(int length)
        {
            var newElements = new LinkedList<KeyValuePair<K, V>>[length];

            foreach (var k in this.Keys)
            {
                newElements[Math.Abs(k.GetHashCode() % newElements.Length)] = this.elements[this.GetNormalizedHash(k)];
            }

            this.elements = newElements;
        }

        private int GetNormalizedHash(K key)
        {
            return Math.Abs(key.GetHashCode() % this.elements.Length);
        }
    }
}