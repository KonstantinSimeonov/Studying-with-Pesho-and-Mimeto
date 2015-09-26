namespace GenericBinaryTree
{
    public interface IBinaryTree<T>
    {
        ICoolBinaryNode<T> Root { get; }

        IBinaryTree<T> Insert(T element);
        IBinaryTree<T> Insert(IBinaryTree<T> tree);
        IBinaryTree<T> Remove(T element);
        IBinaryTree<T> Remove(ICoolBinaryNode<T> binaryNode);

        void Render(int depth);
    }
}