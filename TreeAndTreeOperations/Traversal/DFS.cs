using System;

namespace TreeAndTreeOperations.TraversalStrategy
{
    public class DFSRecursive<T> : ITraversalStrategy<T>
    {
        public ITree<T> Tree { get; private set; }

        public DFSRecursive(ITree<T> tree)
        {
            this.Tree = tree;
        }

        public void TraverseWithAction(ITree<T> tree, Action<T> action)
        {
            action(tree.Value);
            this.TraverseChildNode(tree, action);
        }

        private void TraverseChildNode(ITree<T> tree, Action<T> action)
        {
            foreach (var child in tree.Children)
            {
                action(child.Value);
                TraverseChildNode(child, action);
            }

            Console.WriteLine("~~~~~ End of Node ~~~~~ " + tree.Value);
        }
    }
}
