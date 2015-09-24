using System;
using TreeAndTreeOperations.Traversal;
using TreeAndTreeOperations.TraversalStrategy;

namespace TreeAndTreeOperations
{
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
                    .AddChild(child212))
                .AddChild(child12
                    .AddChild(child221)
                    .AddChild(child222));

            var dfs = new DFSRecursive<string>(root);
            
            dfs.TraverseWithAction(root, Console.WriteLine);

            var dfsIterative = new DFSIterative<string>();

            Console.WriteLine("\n\nITERATIVE BELOW\n\n");

            dfsIterative.TraverseWithAction(root, x => Console.WriteLine(x));
        }
    }
}
