using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeAndTreeOperations
{
    public interface IGraphNode<T>
    {
        T Value { get; }

        IList<IGraphNode<T>> Children { get; }
        IGraphNode<T> AddChild(T child);
        IGraphNode<T> RemoveChild(T child);
        IGraphNode<T> AddChild(IGraphNode<T> subtree);
        IGraphNode<T> RemoveChild(IGraphNode<T> child);
    }
}
