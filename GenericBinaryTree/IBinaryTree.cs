namespace GenericBinaryTree
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides an abstraction over common action performed on a binary tree.
    /// </summary>
    /// <typeparam name="T">The type of data in the binary tree.</typeparam>
    public interface IBinaryTree<T> : IEnumerable<T>
    {
        /// <summary>
        /// The root of the tree.
        /// </summary>
        ICoolBinaryNode<T> Root { get; }

        /// <summary>
        /// Inserts an element in the tree.
        /// </summary>
        /// <param name="element">The element to insert.</param>
        /// <returns>A reference to the tree.</returns>
        IBinaryTree<T> Insert(T element);

        /// <summary>
        /// Inserts the given binary tree as a subtree in the caller tree.
        /// </summary>
        /// <param name="tree">The tree to be inserted.</param>
        /// <returns>A reference to the inserting tree.</returns>
        IBinaryTree<T> Insert(IBinaryTree<T> tree);

        /// <summary>
        /// Removes the given value from the binary tree.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        IBinaryTree<T> Remove(T element);

        /// <summary>
        /// Removes the given value and all it's children from the tree.
        /// </summary>
        /// <param name="subtreeRootValue">The value of the root of the subtree to be removed from the tree.</param>
        /// <returns>A reference to the tree from which the removal is executed.</returns>
        IBinaryTree<T> RemoveSubtree(T subtreeRootValue);
    }
}