namespace HashTableTests
{
    using System.Linq;
    using HashtablesDictionariesSets;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HashTableTests
    {
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
    }
}
