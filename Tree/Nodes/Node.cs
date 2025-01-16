
namespace DSA.Tree.Nodes
{
    public abstract class Node<T> where T:IComparable<T>, IEquatable<T>
    {
        public T? Value { get; set; }
    }
}
