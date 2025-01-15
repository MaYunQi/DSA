namespace DSA.Tree.Nodes
{
    public class TreeNode<T>: Node<T> where T : IComparable<T>, IEquatable<T>
    {
        public TreeNode<T>? Left { get; set; }
        public TreeNode<T>? Right { get; set; }
        public TreeNode()
        {
            Value = default;
            Left = null;
            Right = null;
        }
        public TreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
        public static bool operator <(TreeNode<T> left, TreeNode<T> right)
        {
            return left.Value.CompareTo(right.Value) < 0;
        }
        public static bool operator >(TreeNode<T> left, TreeNode<T> right)
        {
            return left.Value.CompareTo(right.Value) > 0;
        }
        public static bool operator ==(TreeNode<T> left, TreeNode<T> right)
        {
            if(ReferenceEquals(left,right))
                return true;
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;
            return left.Value.Equals(right.Value);
        }
        public static bool operator !=(TreeNode<T> left, TreeNode<T> right)
        {
            return !(left == right);
        }
    }
}
