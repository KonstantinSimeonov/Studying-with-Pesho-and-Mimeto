namespace TreeAndTreeOperations.Traversal
{
    using System;
    public class DFSRecursive<T> : ITraversalStrategy<T>
    {
        public IGraphNode<T> Tree { get; private set; }

        public DFSRecursive(IGraphNode<T> tree)
        {
            this.Tree = tree;
        }

        public void TraverseWithAction(IGraphNode<T> tree, Action<T> action)
        {
            action(tree.Value);
            this.TraverseChildNode(tree, action);
        }

        private void TraverseChildNode(IGraphNode<T> tree, Action<T> action)
        {
            foreach (var child in tree.Children)
            {
                action(child.Value);
                TraverseChildNode(child, action);
            }

           // Console.WriteLine("~~~~~ End of Node ~~~~~ " + tree.Value);
        }
    }
}
