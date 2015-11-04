namespace HashTableTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HashtablesDictionariesSets;
    using HashtablesDictionariesSets.HashTable;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class HashTableTests
    {
        private static Random rng = new Random();

        private static string[] sampleNames = new string[]
        {
            "gosho", "tosho", "ivan", "skumriq", "gencho", "pencho", "penka", "stamat", "pako",
            "djodjo", "strahil", "maria", "uruspiq", "ginka", "nikola", "djena", "plamena", "enrike"
        };

        [TestMethod]
        public void InitialCountShouldBeZero()
        {
            var hashtable = new HashTable<string, string>();

            Assert.AreEqual(0, hashtable.Count);
        }

        [TestMethod]
        public void IndexerShouldReturnTheElementAddedWithAdd()
        {
            var hashtable = new HashTable<string, string>();

            var key = "npesheva";
            var value = "es6 promises";

            hashtable.Add(key, value);

            Assert.AreEqual(value, hashtable[key]);
        }

        [TestMethod]
        public void CapacityShouldDoubleAtSeventyFivePercentLoad()
        {
            var hashtable = new HashTable<string, string>();
            var startingCapacity = hashtable.Capacity;

            sampleNames.Take(13).ForEach(x => hashtable.Add(x, "dinozavyr"));

            Assert.AreEqual(startingCapacity * 2, hashtable.Capacity);
        }

        [TestMethod]
        public void CapacityShouldIncreasePersistently()
        {
            var hashtable = new HashTable<string, string>();
            var lastCapacity = hashtable.Capacity;
            var counter = 16;
            var next = 0;

            while (hashtable.Count < 1024)
            {
                for (int i = 0; i < 1 + (counter * 3) / 4; i++)
                {
                    hashtable.Add((next++).ToString(), "dinozavyr");
                }

                lastCapacity = hashtable.Capacity;
                counter *= 2;

                Assert.AreEqual(counter, lastCapacity);
            }
        }

        [TestMethod]
        public void CapacityShouldDecreasePersistently()
        {
            var hashtable = new HashTable<int, string>();

            for (int i = 0; i <= 768; i++)
            {
                hashtable.Add(i, "");
            }

            Assert.AreEqual(2048, hashtable.Capacity);

            for (int i = 0; i <= 384; i++)
            {
                hashtable.Remove(i);
            }

            Assert.AreEqual(1024, hashtable.Capacity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddShouldThrowExceptionWhenTheSameKeyIsAlreadyPresent()
        {
            var hashtable = new HashTable<string, string>();

            hashtable.Add("gosho", "tosho");
            hashtable.Add("gosho", "pesho");
        }

        [TestMethod]
        public void AddShouldStoreKeysPersistently()
        {
            var keys = new List<decimal>();
            var hashtable = new HashTable<decimal, string>();

            for (int i = 0; i < 1000; i++)
            {
                var nextKey = rng.NextDecimal();
                hashtable.Add(nextKey, "dinozavyr");
                keys.Add(nextKey);
            }

            keys.ForEach(x => Assert.IsTrue(hashtable.Keys.Contains(x)));

            Assert.AreEqual(0, keys.Except(hashtable.Keys).Count());
            Assert.AreEqual(0, hashtable.Keys.Except(keys).Count());
        }

        [TestMethod]
        public void AddShouldStoreDifferentKeysWithSameHashCode()
        {
            var mockRandom1 = new Mock<ICloneable>();

            mockRandom1.Setup(x => x.GetHashCode()).Returns(5);

            var mockRandom2 = new Mock<ICloneable>();

            mockRandom2.Setup(x => x.GetHashCode()).Returns(5);

            var hashtable = new HashTable<ICloneable, string>();

            hashtable.Add(mockRandom1.Object, "1");
            hashtable.Add(mockRandom2.Object, "2");

            Assert.AreEqual(2, hashtable.Count);

            Assert.AreEqual("1", hashtable[mockRandom1.Object]);
            Assert.AreEqual("2", hashtable[mockRandom2.Object]);
        }

        [TestMethod]
        public void TryGetValueShouldReturnFalseWhenTheValueIsNotAdded()
        {
            var hashtable = new HashTable<int, string>();

            string outVar;
            Assert.IsFalse(hashtable.TryGetValue(69, out outVar));
        }

        [TestMethod]
        public void TryGetValueShouldReturnFalsePersistently()
        {
            var hashtable = new HashTable<int, string>();

            for (int i = 0; i < 1000; i += 2)
            {
                hashtable.Add(i, "");
            }

            string outVar;


            for (int i = 1; i < 1000; i += 2)
            {
                Assert.IsFalse(hashtable.TryGetValue(i, out outVar));
            }
        }

        [TestMethod]
        public void TryGetValueShouldReturnTruePersistentlyWhenTheValueIsPresent()
        {
            var hashtable = new HashTable<int, string>();

            for (int i = 0; i < 1000; i += 2)
            {
                hashtable.Add(i, i.ToString());
            }

            string outVar;


            for (int i = 0; i < 1000; i += 2)
            {
                Assert.IsTrue(hashtable.TryGetValue(i, out outVar));
                Assert.AreEqual(i.ToString(), outVar);
            }
        }

        [TestMethod]
        public void ContainsKeyShouldReturnTruePersistentlyWhenKeyHasBeenAdded()
        {
            var hashtable = new HashTable<int, int>();

            for (int i = 0; i < 1000; i++)
            {
                hashtable.Add(i, i);
                Assert.IsTrue(hashtable.ContainsKey(i));
            }
        }

        [TestMethod]
        public void ContainsKeyShouldReturnFalsePersistentlyWhenKeyHasNotBeenAdded()
        {
            var hashtable = new HashTable<int, int>();

            for (int i = 0; i < 2000; i+=2)
            {
                hashtable.Add(i, i);
            }

            for (int i = 1; i < 2000; i+=2)
            {
                Assert.IsFalse(hashtable.ContainsKey(i));
            }
        }

        [TestMethod]
        public void ContainsShouldReturnTrueWhenTheGivenKeyValuePairIsPresent()
        {
            var hashtable = new HashTable<double, string>();

            hashtable.Add(1.1, "fsdfsdf");

            Assert.IsTrue(hashtable.Contains(new KeyValuePair<double, string>(1.1, "fsdfsdf")));
        }

        [TestMethod]
        public void RemoveShouldReturnTrueWhenTheElementIsFoundAndRemoved()
        {
            var hashtable = new HashTable<int, int>();

            hashtable.Add(10, 15);

            Assert.AreEqual(15, hashtable[10]);

            var hasBeenRemoved = hashtable.Remove(10);

            Assert.IsTrue(hasBeenRemoved);
        }

        [TestMethod]
        public void RemoveShouldReturnFalseWhenElementIsNotFound()
        {
            var hashtable = new HashTable<int, int>();

            var hasBeenRemoved = hashtable.Remove(10);

            Assert.IsFalse(hasBeenRemoved);
        }

        [TestMethod]
        public void RemoveShouldDeleteTheElementWithGivenKey()
        {
            var hashtable = new HashTable<int, int>();

            hashtable.Add(10, 15);

            Assert.AreEqual(15, hashtable[10]);

            hashtable.Remove(10);

            int outVar;

            Assert.IsFalse(hashtable.TryGetValue(10, out outVar));
        }

        [TestMethod]
        public void RemoveShouldDeleteTheMatchingKeyValuePair()
        {
            var hashtable = new HashTable<int, int>();

            hashtable.Add(10, 15);

            Assert.AreEqual(15, hashtable[10]);

            var hasBeenRemoved = hashtable.Remove(new KeyValuePair<int, int>(10, 15));

            Assert.IsTrue(hasBeenRemoved);
        }

        [TestMethod]
        public void RemoveShouldNotRemoveKeyValuePairWithMatchingKeyButDifferentValue()
        {
            var hashtable = new HashTable<int, int>();

            hashtable.Add(10, 15);

            Assert.AreEqual(15, hashtable[10]);

            var hasBeenRemoved = hashtable.Remove(new KeyValuePair<int, int>(10, 10));

            Assert.IsFalse(hasBeenRemoved);

            Assert.AreEqual(15, hashtable[10]);
        }

        [TestMethod]
        public void ValuesPropertyShouldReturnAllAddedValues()
        {
            var hashtable = new HashTable<string, string>();

            var values = new HashSet<string>();

            for (int i = 0; i < 200; i++)
            {
                var nextKeyValuePair = new KeyValuePair<string, string>(i.ToString(), rng.NextString(0, 10));

                values.Add(nextKeyValuePair.Value);

                hashtable.Add(nextKeyValuePair);
            }

            hashtable.Values.ForEach(v => Assert.IsTrue(values.Contains(v)));
        }

        [TestMethod]
        public void ValuesPropertyShouldReturnEmptyCollectionByDefault()
        {
            var hashtable = new HashTable<Random, Random>();

            Assert.AreEqual(0, hashtable.Values.Count);
        }

        [TestMethod]
        public void ValuesPropertyShouldNotReturnRemovedElements()
        {
            var hashtable = new HashTable<int, int>();

            for (int i = 0; i < 5; i++)
            {
                hashtable.Add(i, i);
            }

            hashtable.Remove(0);

            var values = hashtable.Values;

            for (int i = 1; i < 5; i++)
            {
                Assert.IsTrue(values.Contains(i));
            }

            Assert.IsFalse(values.Contains(0));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void IndexerGetterShouldThrowAnExceptionWhenNoValueWithSuchKeyExists()
        {
            var hashtable = new HashTable<string, string>();

            var shouldCrash = hashtable["gosho"];
        }

        [TestMethod]
        public void IndexerSetterShouldAddTheValueWhenTheKeyIsNotPresent()
        {
            var hashtable = new HashTable<string, string>();

            hashtable["gosho"] = "penka";

            Assert.AreEqual("penka", hashtable["gosho"]);
        }

        [TestMethod]
        public void IdexerSetterShouldModifyTheValueWhenTheKeyIsPresent()
        {
            var hashtable = new HashTable<string, string>();

            hashtable["gosho"] = "penka";

            hashtable["gosho"] = "ivanka";

            Assert.AreEqual("ivanka", hashtable["gosho"]);
        }

        [TestMethod]
        public void IndexerShouldSetTheElementWithTheGivenKey()
        {
            var hashtable = new HashTable<string, string>();

            hashtable.Add("gosho", "tosho");

            hashtable["gosho"] = "ivan";

            Assert.AreEqual("ivan", hashtable["gosho"]);
        }

        [TestMethod]
        public void ForeachShouldIterateOverAllAddedValues()
        {
            var addedValues = new HashSet<int>();
            var hashtable = new HashTable<string, int>();

            sampleNames.ForEach(n => 
            {
                hashtable.Add(n, n.Length);
                addedValues.Add(n.Length);
            });

            hashtable.ForEach(kvp => Assert.IsTrue(addedValues.Contains(kvp.Value)));
        }
    }
}