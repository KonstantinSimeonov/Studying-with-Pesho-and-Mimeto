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

        public static IDictionary<double, int> GetDoubleOccurrencesWithin(IEnumerable<double> numbers)
        {
            return GetOccurrencesCountsWithin(numbers);
        }

        public static IDictionary<string, int> GetOddOccurringElements(IEnumerable<string> strings)
        {
            var result = GetOccurrencesCountsWithin(strings)
                                                .Where(o => o.Value % 2 == 1)
                                                .ToDictionary(p => p.Key, p => p.Value);

            return result;
        }

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

        private static IDictionary<T, int> GetOccurrencesCountsWithin<T>(IEnumerable<T> numbers)
        {
            var occurrences = new Dictionary<T, int>();

            numbers.ForEach(x =>
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
