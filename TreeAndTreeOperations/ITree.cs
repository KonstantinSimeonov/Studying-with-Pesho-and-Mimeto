using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeAndTreeOperations
{
    public interface ITree<T>
    {
        T Value { get; }

        IList<ITree<T>> Children { get; }
        ITree<T> AddChild(T child);
        ITree<T> RemoveChild(T child);
        ITree<T> AddChild(ITree<T> subtree);
        ITree<T> RemoveChild(ITree<T> child);
    }
}
