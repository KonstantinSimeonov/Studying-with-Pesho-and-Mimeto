namespace TreeAndTreeOperations
{
    using System.Collections;
    using System.Collections.Generic;

    public class Tree<T> : IGraphNode<T>, IEnumerable<IGraphNode<T>>
    {
        public T Value { get; private set; }

        public IGraphNode<T> Parent { get; set; }

        public IList<IGraphNode<T>> Children { get; private set; }

        public Tree(T value)
        {
            this.Value = value;
            this.Children = new List<IGraphNode<T>>();
        }

        public IGraphNode<T> AddChild(T child)
        {
            var childNode = new Tree<T>(child);
            childNode.Parent = this;

            return this.AddChild(childNode);
        }

        public IGraphNode<T> RemoveChild(T child)
        {
            for (int i = 0, length = this.Children.Count; i < length; i++)
            {
                if (this.Children[i].Value.Equals(child))
                {
                    foreach (var grandchild in this.Children[i].Children)
                    {
                        grandchild.Parent = this;
                        this.Children.Add(grandchild);
                    }

                    this.Children.RemoveAt(i);
                    return this;
                }

            }

            return this;
        }

        public IGraphNode<T> AddChild(IGraphNode<T> subtree)
        {
            subtree.Parent = this;
            this.Children.Add(subtree);
            return this;
        }

        public IGraphNode<T> RemoveChild(IGraphNode<T> child)
        {
            this.Children.Remove(child);
            return this;
        }

        public IEnumerator<IGraphNode<T>> GetEnumerator()
        {
            var stack = new Stack<IGraphNode<T>>();

            stack.Push(this);

            while (stack.Count > 0)
            {
                var currentTree = stack.Pop();

                foreach (var child in currentTree.Children)
                {
                    stack.Push(child);
                }

                yield return currentTree;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}