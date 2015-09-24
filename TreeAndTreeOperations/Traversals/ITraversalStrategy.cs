namespace TreeAndTreeOperations.TraversalStrategy
{
    using System;

    public interface ITraversalStrategy<T>
    {
        void TraverseWithAction(ITree<T> tree, Action action);
    }
}