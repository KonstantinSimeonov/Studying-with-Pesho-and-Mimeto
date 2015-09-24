using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeAndTreeOperations
{
    public class Tree<T> : ITree<T>
    {
        public T Value { get; private set; }

        public IList<ITree<T>> Children { get; private set; }

        public Tree(T value)
        {
            this.Value = value;
            this.Children = new List<ITree<T>>();
        }

        public ITree<T> AddChild(T child)
        {
            return this.AddChild(new Tree<T>(child));
        }

        public ITree<T> RemoveChild(T child)
        {
            for (int i = 0, length = this.Children.Count; i < length; i++)
            {
                if(this.Children[i].Value.Equals(child))
                {
                    this.Children.RemoveAt(i);
                    return this;
                }
                
            }

            return this;
        }

        public ITree<T> AddChild(ITree<T> subtree)
        {
            this.Children.Add(subtree);
            return this;
        }

        public ITree<T> RemoveChild(ITree<T> child)
        {
            this.Children.Remove(child);
            return this;
        }
    }
}
