using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeAndTreeOperations.Traversal
{
    public class GraphBFS
    {
        public void TraverseWithAction<T>(IGraphNode<T> root, Action<T> action)
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
                    if(!visited.Contains(child))
                    {
                        nodes.Enqueue(child);
                        visited.Add(child);
                    }
                }
            }
        }
    }
}
