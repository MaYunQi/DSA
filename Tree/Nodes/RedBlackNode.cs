
namespace DSA.Tree.Nodes
{
    public class RedBlackNode<T> : Node<T> where T : IComparable<T>,IEquatable<T>
    {
        public RedBlackNode<T> Left { get; set; }
        public RedBlackNode<T> Right { get; set; }
        public RedBlackNode<T> Parent { get; set; }
        public Color Color { get; set; }
        public RedBlackNode() { }
        public RedBlackNode(T value)
        {
            Value = value;
            Color = Color.Red;
            Left = Right = Parent = null;
        }
        public static bool operator <(RedBlackNode<T> left, RedBlackNode<T> right)
        {
            return left.Value.CompareTo(right.Value) < 0;
        }
        public static bool operator >(RedBlackNode<T> left, RedBlackNode<T> right)
        {
            return left.Value.CompareTo(right.Value) > 0;
        }
        public static bool operator ==(RedBlackNode<T> left, RedBlackNode<T> right)
        {
            if (left is null && right is null)
                return true;
            if(left is null || right is null)
                return false;
            return left.Value.Equals(right.Value);
        }
        public static bool operator !=(RedBlackNode<T> left, RedBlackNode<T> right)
        {
            return !(left == right);
        }
        public bool IsIdenticalTo(RedBlackNode<T> right)
        {
            return ReferenceEquals(this, right);
        }
    }
    public enum Color
    { 
        Red,
        Black
    }
}
