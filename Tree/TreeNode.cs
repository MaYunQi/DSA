
namespace DSA.Tree
{
    public class TreeNode<T> where T:IComparable<T>,IEquatable<T>
    {
        public T? Value { get; set; }
        public TreeNode<T>? Left { get; set; }
        public TreeNode<T>? Right { get; set; }
        public TreeNode()
        {
            Value = default;
            Left = new TreeNode<T>();
            Right = new TreeNode<T>();
        }
        public TreeNode(T value)
        {
            Value= value;
            Left = new TreeNode<T>();
            Right = new TreeNode<T>();
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
            return left.Value.Equals(right.Value);
        }
        public static bool operator !=(TreeNode<T> left, TreeNode<T> right)
        {
            return !(left==right);
        }
    }
}
