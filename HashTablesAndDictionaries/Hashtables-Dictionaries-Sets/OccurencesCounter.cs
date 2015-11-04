namespace HashtablesDictionariesSets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class OccurencesCounter
    {
        private static readonly char[] Punctuation; // = new char[] { '.', ',', '!', ' ', '/', '-', ':', ';', '?', '"', '\'', '&', '–' };

        static OccurencesCounter()
        {
            // lets generate some punctuation :D
            var nonLetters = new List<char>();

            for (int i = 0; i < 128; i++)
            {
                var codeAsChar = i.ToChar();
                if(!codeAsChar.IsLetter() && !codeAsChar.IsDigit())
                {
                    nonLetters.Add(i.ToChar());
                }
            }

            Punctuation = nonLetters.ToArray();
        }

        /// <summary>
        /// Write a program that counts in a given array of double values the
        /// number of occurrences of each value. Use Dictionary<TKey,TValue>.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static IDictionary<double, int> GetDoubleOccurrencesWithin(IEnumerable<double> numbers)
        {
            return GetOccurrencesCountsWithin(numbers);
        }

        /// <summary>
        /// Write a program that extracts from a given sequence of strings all elements that present in it odd number of times.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static IDictionary<string, int> GetOddOccurringElements(IEnumerable<string> strings)
        {
            var result = GetOccurrencesCountsWithin(strings)
                                                .Where(o => o.Value % 2 == 1)
                                                .ToDictionary(p => p.Key, p => p.Value);

            return result;
        }

        /// <summary>
        /// Write a program that counts how many times each word from given text file words.txt appears in it. 
        /// The character casing differences should be ignored. The result words should be ordered by their number of occurrences in the text. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IDictionary<string, int> GetOccurrencesFromText(string text)
        {
            var words = text.Split(Punctuation, StringSplitOptions.RemoveEmptyEntries)
                                                                        .Select(w => w.ToLower())
                                                                        .ToArray();

            var result = GetOccurrencesCountsWithin(words)
                                            .OrderBy(p => p.Value)
                                            .ToDictionary(p => p.Key, p => p.Value);

            return result;
        }

        private static IDictionary<T, int> GetOccurrencesCountsWithin<T>(IEnumerable<T> sequence)
        {
            var occurrences = new Dictionary<T, int>();

            sequence.ForEach(x =>
            {
                if (!occurrences.ContainsKey(x))
                {
                    occurrences.Add(x, 0);
                }

                occurrences[x]++;
            });

            return occurrences;
        }
    }
}
