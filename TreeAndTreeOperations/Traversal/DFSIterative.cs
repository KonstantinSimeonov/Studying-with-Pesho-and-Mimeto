using System;
using System.Collections.Generic;
using TreeAndTreeOperations.TraversalStrategy;

namespace TreeAndTreeOperations.Traversal
{
    public class DFSIterative<T> : ITraversalStrategy<T>
    {
        public void TraverseWithAction(IGraphNode<T> tree, Action<T> action)
        {
            var stack = new Stack<IGraphNode<T>>();

            stack.Push(tree);

            while (stack.Count > 0)
            {
                var currentTree = stack.Pop();

                action(currentTree.Value);

                foreach (var child in currentTree.Children)
                {
                    stack.Push(child);
                }
            }
        }
    }
}
