namespace TreeAndTreeOperations
{ 
    using System.Collections.Generic;

    public interface IGraphBuilder<T>
    {
        IGraphNode<T> Construct(IDictionary<T, IList<T>> adjacencyList);
        IGraphNode<T> Construct(IList<T> orderedNodes, bool adjacencyMatrix);
    }
}