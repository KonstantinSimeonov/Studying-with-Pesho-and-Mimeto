namespace HashtablesDictionariesSets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Phonebook;

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

            var phonebook = new PhoneBook();

            var info = new string[]
            {
                "Mimi Shmatkata          | Plovdiv  | 0888 12 34 56",
                "Kireto                  | Varna    | 052 23 45 67",
                "Daniela Ivanova Petrova | Karnobat | 0899 999 888",
                "Bat Gancho              | Sofia    | 02 946 946 946",
                "Bat Gancho              | Pleven   | 02 888 946 946",
            };

            info.ForEach(x =>
            {
                var groomedArray = x.Split('|').Select(y => y.Trim()).ToArray();

                phonebook.Add(new PersonInfo()
                {
                    Name = groomedArray[0],
                    City = groomedArray[1],
                    PhoneNumber = groomedArray[2]
                });
            });

            var queries = new string[][]
            {
                new string[] { "Mimi" },
                new string[] { "Bat Gancho" },
                new string[] { "Bat Gancho", "Pleven" }
            };

            queries.ForEach(x => 
            {
                if(x.Length == 1)
                {
                    phonebook.Find(x[0]).StringJoin().Print();
                }
                else
                {
                    phonebook.Find(x[0], x[1]).StringJoin().Print();
                }
            });
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