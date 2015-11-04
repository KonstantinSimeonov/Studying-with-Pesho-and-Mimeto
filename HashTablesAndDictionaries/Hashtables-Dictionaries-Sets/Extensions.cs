namespace HashtablesDictionariesSets
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Linq;

    internal static class Extensions
    {
        private static readonly DateTime SqlDateTimeMinValue = DateTime.Parse("1753/1/1");

        private const string AllowedSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static int ToInt32(this string str)
        {
            return int.Parse(str);
        }

        public static int ToInt32(this char ch)
        {
            if (ch < '0' || '0' < ch)
            {
                throw new ArgumentException("Input char was not in correct format");
            }

            return ch - 48;
        }

        public static bool IsMatch(this string str, string pattern)
        {
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }

        public static char ToChar(this int code)
        {
            return (char)code;
        }

        public static bool IsLetter(this char ch)
        {
            return char.IsLetter(ch);
        }

        public static bool IsDigit(this char ch)
        {
            return char.IsDigit(ch);
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static string NextString(this Random random, int minLength = 0, int maxLength = int.MaxValue / 2)
        {
            var result = new StringBuilder();
            var length = random.Next(minLength, maxLength);

            for (int i = 0; i < length; i++)
            {
                result.Append(AllowedSymbols[random.Next(0, AllowedSymbols.Length)]);
            }

            return result.ToString();
        }

        public static DateTime NextDate(this Random rng, int startYear = 2010)
        {
            var start = new DateTime(startYear, 1, 1);

            int range = (new DateTime(2015, DateTime.Now.Month - 2, 31 - DateTime.Now.Day) - start).Days;
            var result = start.AddDays(rng.Next(range));

            return result;
        }

        public static decimal NextDecimal(this Random random)
        {
            return (decimal)random.NextDouble();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }

        public static string StringJoin<T>(this IEnumerable<T> collection, string separator = ", ")
        {
            return string.Join(separator, collection);
        }

        public static T Print<T>(this T obj, ConsoleColor color = ConsoleColor.White, bool line = true)
        {
            Console.ForegroundColor = color;
            Console.Write(obj);
            if (line)
            {
                Console.WriteLine();
            }
            return obj;
        }

        public static IDictionary<string, string> AddTo(this IDictionary<string, string> dict, string key, string value)
        {
            dict.Add(key, value);
            return dict;
        }

        public static ICollection<T> AddToCol<T>(this ICollection<T> collection, T objToAdd)
        {
            collection.Add(objToAdd);
            return collection;
        }

        public static DateTime Sqlize(this DateTime datetime)
        {
            return datetime < SqlDateTimeMinValue ? SqlDateTimeMinValue : datetime;
        }

        public static bool IsInside<T>(this T[,] matrix, int x, int y)
        {
            return (0 <= y && y < matrix.GetLength(0)) && (0 <= x && x < matrix.GetLength(1));
        }

        public static void EscapeInput()
        {
            string line = Console.ReadLine();

            var sb = new StringBuilder();

            while (line != string.Empty)
            {
                sb.AppendLine("+\"" + line.Replace("\"", "\\\"") + "\"");
                line = Console.ReadLine();
            }

            Console.WriteLine(sb);
        }
    }
}