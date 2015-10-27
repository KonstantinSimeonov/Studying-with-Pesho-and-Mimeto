namespace TreeAndTreeOperations.GraphRepresentations
{
    using System;
    using System.Collections.Generic;

    class GraphBuilder<T> : IGraphBuilder<T>
    {
        public IGraphNode<T> Construct(IDictionary<T, IList<T>> adjacencyList)
        {
            throw new NotImplementedException();
        }

        public IGraphNode<T> Construct(IList<T> orderedNodes, bool adjacencyMatrix)
        {
            throw new NotImplementedException();
        }
    }
}
