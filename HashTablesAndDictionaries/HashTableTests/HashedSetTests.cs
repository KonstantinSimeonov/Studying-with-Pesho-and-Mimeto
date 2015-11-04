namespace HashTableTests
{
    using System;
    using System.Collections.Generic;
    using HashtablesDictionariesSets;
    using HashtablesDictionariesSets.HashedSet;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

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
            var hashedset = new HashedSet<int>();

            var hasBeenRemoved = hashedset.Remove(10);

            Assert.IsFalse(hasBeenRemoved);
        }

        [TestMethod]
        public void RemoveShouldDeleteTheElement()
        {
            var hashedset = new HashedSet<int>();

            hashedset.Add(10);

            Assert.IsTrue(hashedset.Contains(10));

            var removed = hashedset.Remove(10);

            Assert.IsTrue(removed);
        }

        [TestMethod]
        public void ForeachShouldIterateOverAllAddedValues()
        {
            var addedValues = new HashSet<int>();
            var hashedset = new HashedSet<int>();

            sampleNames.ForEach(n =>
            {
                hashedset.Add(n.Length);
                addedValues.Add(n.Length);
            });

            hashedset.ForEach(v => Assert.IsTrue(addedValues.Contains(v)));
        }

        [TestMethod]
        public void UnionShouldProduceSetContainingElementsFromBothSets()
        {
            ICollection<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            var hashedset = new HashedSet<int>();

            sampleNames.ForEach(n => hashedset.Add(n.Length));

            var result = hashedset.UnionWith(numbers);

            numbers.ForEach(x => Assert.IsTrue(result.Contains(x)));

            hashedset.ForEach(x => Assert.IsTrue(result.Contains(x)));
        }

        [TestMethod]
        public void UnionShouldReturnUniqueValues()
        {
            ICollection<int> numbers = new List<int>() { 1, 2, 2, 2, 3, 3, 4, 4, 5 };
            var hashedset = new HashedSet<int>();

            new int[] { 6, 2, 6, 7, 3 }.ForEach(n => hashedset.Add(n));

            var result = hashedset.UnionWith(numbers);

            Assert.AreEqual(7, result.Count);
        }

        [TestMethod]
        public void IntersectShouldProducesSetContainingCommonUniqueItems()
        {
            ICollection<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var hashset = new HashedSet<int>();

            new int[] { 6, 7, 6, 6, 8, 9, 10, 11, 12 }.ForEach(x => hashset.Add(x));

            var result = hashset.IntersectWith(numbers);

            new int[] { 6, 7, 8 }.ForEach(x => Assert.IsTrue(result.Contains(x)));

            Assert.AreEqual(3, result.Count);
        }
    }
}
