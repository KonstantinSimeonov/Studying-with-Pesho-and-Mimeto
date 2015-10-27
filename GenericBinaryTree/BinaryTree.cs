namespace GenericBinaryTree
{
    using System;
    using System.Collections;
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
        // TODO: move the CompareTo call inside the indexer, writing it every time is cumbersome
        public ICoolBinaryNode<T> this[int comparisonResult]
        {
            get
            {
                return comparisonResult > 0 ? this.Right : this.Left;
            }

            set
            {
                if (comparisonResult > 0)
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

    // TODO: balance

    /// <summary>
    /// Concrete implementation of the ADT Binary Tree.
    /// </summary>
    /// <typeparam name="T">The type of the values in the tree.</typeparam>
    public class BinaryTree<T> : IBinaryTree<T>
        where T : IComparable<T>
    {
        public ICoolBinaryNode<T> Root { get; private set; }

        /// <summary>
        /// A constructor for a tree with a root.
        /// </summary>
        /// <param name="rootValue">The value of the root.</param>
        public BinaryTree(T rootValue)
        {
            this.Root = new Node<T>(rootValue);
        }

        public IBinaryTree<T> Insert(T element)
        {
            this.InsertNode(new Node<T>(element));
            return this;
        }

        public IBinaryTree<T> Insert(IBinaryTree<T> tree)
        {
            this.InsertNode(tree.Root);
            return this;
        }

        public IBinaryTree<T> RemoveSubtree(T subtreeRootValue)
        {
            var parent = this.GetParentForValue(subtreeRootValue);

            parent[subtreeRootValue.CompareTo(parent.Value)] = null;

            return this;
        }

        // implement duh
        #region NotImplemented

        public IBinaryTree<T> Remove(T element)
        {
            throw new NotImplementedException("Implement removing for singular values");

            // TOOO: finish this implementation
            var parent = this.GetParentForValue(element);
            var nodeToRemove = parent[element.CompareTo(parent.Value)];

            if(nodeToRemove.Right == null && nodeToRemove.Left == null)
            {
                parent[element.CompareTo(parent.Value)] = null;
            }
            else if(nodeToRemove.Left == null)
            {
                parent[element.CompareTo(parent.Value)] = nodeToRemove.Right;
            }
            else if(nodeToRemove.Right == null)
            {
                parent[element.CompareTo(parent.Value)] = nodeToRemove.Left;
            }


            var current = parent[parent.Value.CompareTo(element)];
            var parentOfCurrent = parent;
            return this;
        }

        // TODO: provide a quality API for different tree traversals
        // i.e. for BFS, DFS, etc
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private ICoolBinaryNode<T> GetSuccessor(T value)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void InsertNode(ICoolBinaryNode<T> nodeToInsert)
        {
            var value = nodeToInsert.Value;
            var parent = this.GetParentForValue(value);

            parent[value.CompareTo(parent.Value)] = nodeToInsert;
        }

        private ICoolBinaryNode<T> GetParentForValue(T value)
        {
            var current = this.Root;

            ICoolBinaryNode<T> parentOfCurrent = null;

            while (current != null)
            {
                parentOfCurrent = current;
                current = current[value.CompareTo(current.Value)];
            }

            return parentOfCurrent; //[value.CompareTo(parentOfCurrent.Value)];
        }
    }
}
