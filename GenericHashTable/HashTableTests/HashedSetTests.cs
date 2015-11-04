using System;
using System.Collections.Generic;
using System.Linq;
using HashtablesDictionariesSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace hashedsetTests
{
    [TestClass]
    public class HashedSetTests
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
            var hashedset = new HashedSet<string>();

            Assert.AreEqual(0, hashedset.Count);
        }

        [TestMethod]
        public void AddShouldNotThrowExceptionWhenTheSameKeyIsAlreadyPresent()
        {
            var hashedset = new HashedSet<string>();

            hashedset.Add("gosho");
            hashedset.Add("gosho");
        }

        [TestMethod]
        public void AddShouldStoreDifferentKeysWithSameHashCode()
        {
            var mock1 = new Mock<ICloneable>();

            mock1.Setup(x => x.GetHashCode()).Returns(5);
            mock1.Setup(x => x.Equals(It.IsAny<object>())).Returns(false);
            var mock2 = new Mock<ICloneable>();

            mock2.Setup(x => x.GetHashCode()).Returns(5);
            mock2.Setup(x => x.Equals(It.IsAny<object>())).Returns(false);
            var hashedset = new HashedSet<ICloneable>();

            hashedset.Add(mock1.Object);
            hashedset.Add(mock2.Object);

            Assert.AreEqual(2, hashedset.Count);
        }

        [TestMethod]
        public void ContainsShouldReturnTruePersistentlyWhenKeyHasBeenAdded()
        {
            var hashedset = new HashedSet<int>();

            for (int i = 0; i < 1000; i++)
            {
                hashedset.Add(i);
                Assert.IsTrue(hashedset.Contains(i));
            }
        }

        [TestMethod]
        public void ContainsKeyShouldReturnFalsePersistentlyWhenKeyHasNotBeenAdded()
        {
            var hashedset = new HashedSet<int>();

            for (int i = 0; i < 2000; i += 2)
            {
                hashedset.Add(i);
            }

            for (int i = 1; i < 2000; i += 2)
            {
                Assert.IsFalse(hashedset.Contains(i));
            }
        }

        [TestMethod]
        public void RemoveShouldReturnTrueWhenTheElementIsFoundAndRemoved()
        {
            var hashedset = new HashedSet<int>();

            hashedset.Add(10);

            var hasBeenRemoved = hashedset.Remove(10);

            Assert.IsTrue(hasBeenRemoved);
            Assert.IsFalse(hashedset.Contains(10));
        }

        [TestMethod]
        public void RemoveShouldReturnFalseWhenElementIsNotFound()
        {
            var hashedset = new HashedSet<string>();

            var hasBeenRemoved = hashedset.Remove(10);

            Assert.IsFalse(hasBeenRemoved);
        }

        [TestMethod]
        public void RemoveShouldDeleteTheElementWithGivenKey()
        {
            var hashedset = new HashedSet<string>();

            hashedset.Add(10, 15);

            Assert.AreEqual(15, hashedset[10]);

            hashedset.Remove(10);

            int outVar;

            Assert.IsFalse(hashedset.TryGetValue(10, out outVar));
        }

        [TestMethod]
        public void RemoveShouldDeleteTheMatchingKeyValuePair()
        {
            var hashedset = new HashedSet<string>();

            hashedset.Add(10, 15);

            Assert.AreEqual(15, hashedset[10]);

            var hasBeenRemoved = hashedset.Remove(new KeyValuePair<int, int>(10, 15));

            Assert.IsTrue(hasBeenRemoved);
        }

        [TestMethod]
        public void RemoveShouldNotRemoveKeyValuePairWithMatchingKeyButDifferentValue()
        {
            var hashedset = new HashedSet<string>();

            hashedset.Add(10, 15);

            Assert.AreEqual(15, hashedset[10]);

            var hasBeenRemoved = hashedset.Remove(new KeyValuePair<int, int>(10, 10));

            Assert.IsFalse(hasBeenRemoved);

            Assert.AreEqual(15, hashedset[10]);
        }

        [TestMethod]
        public void ValuesPropertyShouldReturnAllAddedValues()
        {
            var hashedset = new HashedSet<string>();

            var values = new HashSet<string>();

            for (int i = 0; i < 200; i++)
            {
                var nextKeyValuePair = new KeyValuePair<string, string>(i.ToString(), rng.NextString(0, 10));

                values.Add(nextKeyValuePair.Value);

                hashedset.Add(nextKeyValuePair);
            }

            hashedset.Values.ForEach(v => Assert.IsTrue(values.Contains(v)));
        }

        [TestMethod]
        public void ValuesPropertyShouldReturnEmptyCollectionByDefault()
        {
            var hashedset = new HashedSet<string>();

            Assert.AreEqual(0, hashedset.Values.Count);
        }

        [TestMethod]
        public void ValuesPropertyShouldNotReturnRemovedElements()
        {
            var hashedset = new HashedSet<string>();

            for (int i = 0; i < 5; i++)
            {
                hashedset.Add(i, i);
            }

            hashedset.Remove(0);

            var values = hashedset.Values;

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
            var hashedset = new HashedSet<string>();

            var shouldCrash = hashedset["gosho"];
        }

        [TestMethod]
        public void IndexerSetterShouldAddTheValueWhenTheKeyIsNotPresent()
        {
            var hashedset = new HashedSet<string>();

            hashedset["gosho"] = "penka";

            Assert.AreEqual("penka", hashedset["gosho"]);
        }

        [TestMethod]
        public void IdexerSetterShouldModifyTheValueWhenTheKeyIsPresent()
        {
            var hashedset = new HashedSet<string>();

            hashedset["gosho"] = "penka";

            hashedset["gosho"] = "ivanka";

            Assert.AreEqual("ivanka", hashedset["gosho"]);
        }

        [TestMethod]
        public void IndexerShouldSetTheElementWithTheGivenKey()
        {
            var hashedset = new HashedSet<string>();

            hashedset.Add("gosho", "tosho");

            hashedset["gosho"] = "ivan";

            Assert.AreEqual("ivan", hashedset["gosho"]);
        }

        [TestMethod]
        public void ForeachShouldIterateOverAllAddedValues()
        {
            var addedValues = new HashSet<int>();
            var hashedset = new HashedSet<string>();

            sampleNames.ForEach(n =>
            {
                hashedset.Add(n, n.Length);
                addedValues.Add(n.Length);
            });

            hashedset.ForEach(kvp => Assert.IsTrue(addedValues.Contains(kvp.Value)));
        }
    }
}
