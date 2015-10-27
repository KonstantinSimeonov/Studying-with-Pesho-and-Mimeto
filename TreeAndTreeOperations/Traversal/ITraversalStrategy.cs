namespace TreeAndTreeOperations.Traversal
{
    using System;

    public interface ITraversalStrategy<T>
    {
        void TraverseWithAction(IGraphNode<T> tree, Action<T> action);
    }
}