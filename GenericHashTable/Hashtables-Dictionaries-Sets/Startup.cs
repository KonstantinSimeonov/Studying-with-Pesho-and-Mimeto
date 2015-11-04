using System;
using System.Collections.Generic;
using System.Linq;

namespace HashtablesDictionariesSets
{
    public static class Startup
    {
        public static void Main()
        {
            //var sampleText = "This is the TEXT. Text, text, text - THIS TEXT! Is this the text?";

            //OccurencesCounter.GetOccurrencesFromText(sampleText).StringJoin().Print();

            //var hashy = new HashTable<string, string>();
            //var rng = new Random();
            //var names = new string[]
            //{
            //    "gosho", "tosho", "ivan", "skumriq", "gencho", "pencho", "penka", "stamat", "pako",
            //    "djodjo", "strahil", "maria", "uruspiq", "ginka", "nikola", "djena", "plamena", "enrike"
            //};

            //names.ForEach(n => 
            //{
            //    hashy.Add(n, names[rng.Next(0, names.Length)]);
            //});

            //// remove first 10
            //names.Take(10).ForEach(x => hashy.Remove(x));

            //// print second 10 via indexer
            //names.Skip(10).ForEach(x => hashy[x].Print());

            var hash = new HashSet<Test>();

            var test1 = new Test();
            var test2 = new Test();

            hash.Add(test1);
            hash.Add(test2);

            hash.Count.Print();
        }
    }

    class Test
    {
        public override int GetHashCode()
        {
            return 5;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }
    }
}