namespace GenericBinaryTree
{
    public interface ICoolBinaryNode<T>
    {
        T Value { get; set; }
        ICoolBinaryNode<T> Left { get; set; }
        ICoolBinaryNode<T> Right { get; set; }
        ICoolBinaryNode<T> this[int comparisonResult] { get; set; }
    }
}