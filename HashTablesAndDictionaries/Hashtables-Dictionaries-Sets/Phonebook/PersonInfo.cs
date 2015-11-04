namespace HashtablesDictionariesSets.Phonebook
{
    public class PersonInfo
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(this, obj))
            {
                return true;
            }

            if(obj as PersonInfo == null)
            {
                return false;
            }

            var other = obj as PersonInfo;

            return this.Name == other.Name && this.City == other.City && this.PhoneNumber == other.PhoneNumber;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * City.GetHashCode() ^ (PhoneNumber.GetHashCode() * 17);
        }

        public override string ToString()
        {
            return string.Format("{0} from {1}. Phone: {2}", this.Name, this.City, this.PhoneNumber);
        }
    }
}