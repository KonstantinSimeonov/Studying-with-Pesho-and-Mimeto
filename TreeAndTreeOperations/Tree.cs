using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeAndTreeOperations
{
    public class Tree<T> : IGraphNode<T>
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
            return this.AddChild(new Tree<T>(child));
        }

        public IGraphNode<T> RemoveChild(T child)
        {
            for (int i = 0, length = this.Children.Count; i < length; i++)
            {
                if(this.Children[i].Value.Equals(child))
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
            this.Children.Add(subtree);
            return this;
        }

        public IGraphNode<T> RemoveChild(IGraphNode<T> child)
        {
            this.Children.Remove(child);
            return this;
        }
    }
}
