namespace TreeAndTreeOperations
{
    using System;
    using System.Collections.Generic;
    using Traversal;

    using System.Linq;

    class Program
    {
        static void Main()
        {
            var root = new Tree<string>("Grandfather");

            var child11 = new Tree<string>("Father");
            var child12 = new Tree<string>("Aunt");

            var child211 = new Tree<string>("Sister");
            var child212 = new Tree<string>("Brother");

            var child221 = new Tree<string>("Cousin 1");
            var child222 = new Tree<string>("Cousin 2");

            root.AddChild(child11
                            .AddChild(child211)
                            .AddChild(child212)
                         )
                        .AddChild(child12
                                     .AddChild(child221)
                                     .AddChild(child222)
                                 );

            // var dfs = new DFSRecursive<string>(root);

            // dfs.TraverseWithAction(root, Console.WriteLine);

            // var dfsIterative = new DFSIterative<string>();

            // Console.WriteLine("\n\nITERATIVE BELOW\n\n");

            // dfsIterative.TraverseWithAction(root, x => Console.WriteLine(x));

            // represent a graph using adjacency list
            var graphRepresentaion = new Dictionary<IGraphNode<string>, IList<IGraphNode<string>>>() 
            {
                // random links
                {root, new List<IGraphNode<string>>(){child11, child12, child222}},
                {child11, new List<IGraphNode<string>>(){root, child211}},
                {child12, new List<IGraphNode<string>>(){child212, child211}},
                {child212, new List<IGraphNode<string>>(){child11, root, child211}},
                {child211, new List<IGraphNode<string>>(){root}}
            };

            // realize the links
            foreach (var node in graphRepresentaion)
            {
                foreach (var neighbor in node.Value)
                {
                    if(!node.Key.Children.Contains(neighbor))
                    {
                        node.Key.AddChild(neighbor);
                    }
                }
            }

            // var visited = new List<IGraphNode<string>>();

            // var nodes = new Queue<IGraphNode<string>>();

            // new GraphBFS().TraverseWithAction(root, x => Console.WriteLine(x));
            

            var gosho = new int[] { 1, 2, 3, 4, 5, 6, 7 }.Where(x => x % 2 == 0).Select(x => x + 3).ToArray();

            var pesho = new int[,] { { 2, 3 }, { 3, 4 }, { -1, 17 } };
        }
    }
}
