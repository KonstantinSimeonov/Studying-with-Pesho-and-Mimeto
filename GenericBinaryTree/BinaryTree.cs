namespace GenericBinaryTree
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Binary tree component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T> : ICoolBinaryNode<T>
    {
        public T Value { get; set; }
        public ICoolBinaryNode<T> Left { get; set; }
        public ICoolBinaryNode<T> Right { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }

        // cause we cool
        public ICoolBinaryNode<T> this[int comparisonResult]
        {
            get
            {
                return comparisonResult > 0 ? this.Right : this.Left;
            }
            
            set
            {
                if(comparisonResult > 0)
                {
                    this.Right = value;
                }
                else
                {
                    this.Left = value;
                }
            }
        }
    }

    public class BinaryTree<T> : IBinaryTree<T>
        where T : IComparable<T>
    {
        public ICoolBinaryNode<T> Root { get; private set; }

        public BinaryTree(T rootValue)
        {
            this.Root = new Node<T>(rootValue);
        }

        public void Insert(T element)
        {
            var current = this.Root;
            ICoolBinaryNode<T> parentOfCurrent = null;

            while(current != null)
            {
                parentOfCurrent = current;
                current = current[element.CompareTo(current.Value)];
            }

            parentOfCurrent[element.CompareTo(parentOfCurrent.Value)] = new Node<T>(element);
        }

        public void Render(int depth)
        {
            Display(0, this.Root);
        }

        private void Display(int depth, ICoolBinaryNode<T> node)
        {
            if(node == null)
            {
                return;
            }

            Console.WriteLine(new string(' ', depth) + node.Value);
            this.Display(depth + 2, node.Left);
            this.Display(depth + 2, node.Right);
        }

        // implement duh
        #region NotImplemented

        IBinaryTree<T> IBinaryTree<T>.Insert(T element)
        {
            throw new NotImplementedException();
        }

        public IBinaryTree<T> Insert(IBinaryTree<T> tree)
        {
            throw new NotImplementedException();
        }

        public IBinaryTree<T> Remove(T element)
        {
            throw new NotImplementedException();
        }

        public IBinaryTree<T> Remove(ICoolBinaryNode<T> binaryNode)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
