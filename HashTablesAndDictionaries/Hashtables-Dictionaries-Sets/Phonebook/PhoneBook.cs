namespace HashtablesDictionariesSets.Phonebook
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using HashTable;

    public class PhoneBook
    {
        private IDictionary<string, ICollection<PersonInfo>> byName;

        public PhoneBook(IDictionary<string, ICollection<PersonInfo>> foundation)
        {
            this.byName = foundation;
        }

        public PhoneBook()
            : this(new HashTable<string, ICollection<PersonInfo>>())
        {
        }

        public PhoneBook Add(PersonInfo info)
        {
            if (!this.byName.ContainsKey(info.Name))
            {
                this.byName[info.Name] = new LinkedList<PersonInfo>();
            }

            if (!this.byName[info.Name].Contains(info))
            {
                this.byName[info.Name].Add(info);
            }

            return this;
        }

        public ICollection<PersonInfo> Find(string name)
        {
            var matches = this.byName.Keys.Where(x => Regex.IsMatch(x, name, RegexOptions.IgnoreCase)).ToList();
            var result = new List<PersonInfo>();

            foreach (var key in matches)
            {
                foreach (var item in this.byName[key])
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public ICollection<PersonInfo> Find(string name, string town)
        {
            return this.Find(name).Where(x => x.City == town).ToList();
        }
    }
}