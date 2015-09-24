using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeAndTreeOperations.TraversalStrategy
{
    public class DFSRecursive<T> : ITraversalStrategy<T>
    {
        public ITree<T> Tree { get; private set; }

        public DFSRecursive(ITree<T> tree)
        {
            this.Tree = tree;
        }

        public void TraverseWithAction(ITree<T> tree, Action action)
        {
            
        }
    }
}
