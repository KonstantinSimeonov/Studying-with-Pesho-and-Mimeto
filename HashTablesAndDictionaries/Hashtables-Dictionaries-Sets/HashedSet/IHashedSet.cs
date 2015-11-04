namespace HashtablesDictionariesSets.HashedSet
{
    using System.Collections.Generic;

    public interface IHashedSet<T> : ICollection<T>
    {
        IHashedSet<T> UnionWith(params ICollection<T>[] sets);

        IHashedSet<T> IntersectWith(params ICollection<T>[] sets);
    }
}
