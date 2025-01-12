
namespace DSA.List
{
    public class ListNode<T> where T : IComparable<T>,IEquatable<T>
    {
        public T Value { get; set; }
        public ListNode<T>? Next { get; set; }
        public ListNode() { }
        public ListNode(T value)
        {
            Value = value;
        }
        public ListNode(T value, ListNode<T> next)
        {
            Value = value;
            Next = next;
        }
        public static bool operator < (ListNode<T> left,ListNode<T> right)
        {
            return left.Value.CompareTo(right.Value) < 0;
        }
        public static bool operator >(ListNode<T> left, ListNode<T> right)
        {
            return left.Value.CompareTo(right.Value) > 0;
        }
        public static bool operator <=(ListNode<T> left, ListNode<T> right)
        {
            return left.Value.CompareTo(right.Value) <= 0;
        }
        public static bool operator >=(ListNode<T> left, ListNode<T> right)
        {
            return left.Value.CompareTo(right.Value) >= 0;
        }
        public static bool operator ==(ListNode<T> left, ListNode<T> right)
        {
            if(ReferenceEquals(left,right))
                return true;
            if(left is null || right is null)
                return false;
            return left.Value.Equals(right.Value);
        }
        public static bool operator !=(ListNode<T> left, ListNode<T> right)
        {
            return !(left == right);
        }
    }
}
