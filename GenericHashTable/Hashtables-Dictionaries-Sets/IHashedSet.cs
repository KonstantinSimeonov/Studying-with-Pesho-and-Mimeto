namespace HashtablesDictionariesSets
{
    using System.Collections.Generic;

    public interface IHashedSet<T> : ICollection<T>
    {
        IHashedSet<T> UnionWith(ICollection<T> collection);

        IHashedSet<T> IntersectWith(ICollection<T> collection);
    }
}
