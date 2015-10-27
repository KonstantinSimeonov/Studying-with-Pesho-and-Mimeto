namespace TreeAndTreeOperations.Traversal
{
    using System;
    using System.Collections.Generic;

    public class DFSIterative<T> : ITraversalStrategy<T>
    {
        public void TraverseWithAction(IGraphNode<T> root, Action<T> action)
        {
            var stack = new Stack<IGraphNode<T>>();

            stack.Push(root);

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
