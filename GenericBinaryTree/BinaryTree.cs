using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericBinaryTree
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }

        public Node<T> this[int comparisonResult]
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

    public class BinaryTree<T>
        where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public BinaryTree(T rootValue)
        {
            this.Root = new Node<T>(rootValue);
        }

        public void Insert(T element)
        {
            var current = this.Root;
            Node<T> parentOfCurrent = null;

            while(current != null)
            {
                parentOfCurrent = current;
                current = current[element.CompareTo(current.Value)];
            }

            parentOfCurrent[element.CompareTo(parentOfCurrent.Value)] = new Node<T>(element);
        }

        public void DisplayTree(int depth)
        {
            // Display(0, this.Root);

            BFS();
        }

        private void Display(int depth, Node<T> node)
        {
            if(node == null)
            {
                return;
            }
            Console.WriteLine(new string(' ', depth) + node.Value);
            this.Display(depth + 1, node.Left);
            this.Display(depth + 1, node.Right);
        }

        private void BFS()
        {
            var nodes = new Queue<Node<T>>();
            var counter = 1;
            nodes.Enqueue(this.Root);
            var depth = 8;

            while (nodes.Count > 0)
            {
                var current = nodes.Dequeue();

                Console.Write(current.Value + " ");
                if(current.Left != null)
                nodes.Enqueue(current.Left);
                if(current.Right != null)
                nodes.Enqueue(current.Right);

                counter++;

                if (Math.Log(counter, 2).Equals(Math.Round(Math.Log(counter, 2))))
                {
                    Console.Write("\n" + new string(' ', depth));
                    depth /= 2;
                }
            }
        }
    }
}
