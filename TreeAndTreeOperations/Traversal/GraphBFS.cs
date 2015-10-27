namespace TreeAndTreeOperations.Traversal
{
    using System;
    using System.Collections.Generic;
    public class GraphBFS<T> : ITraversalStrategy<T>
    {
        public void TraverseWithAction(IGraphNode<T> root, Action<T> action)
        {
            // queue for bfs
            var nodes = new Queue<IGraphNode<T>>();
            // list for visited
            var visited = new HashSet<IGraphNode<T>>();
            // var visited = new List<IGraphNode<T>>();

            nodes.Enqueue(root);
            visited.Add(root);

            while (nodes.Count > 0)
            {
                var current = nodes.Dequeue();

                action(current.Value);

                foreach (var child in current.Children)
                {
                    if (!visited.Contains(child))
                    {
                        nodes.Enqueue(child);
                        visited.Add(child);
                    }
                }
            }
        }
    }
}
