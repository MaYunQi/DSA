
namespace DSA.Graphs.Entities
{
    public class Vertex<T> where T : IEquatable<T>
    {
        public T Value { get;private set; }
        public Vertex()
        {
            Value = default;
        }
        public Vertex(T value)
        {
            Value=value;
        }
        public bool IsIdenticalTo(Vertex<T> vertex)
        {
            return ReferenceEquals(this, vertex);
        }
        public static bool operator ==(Vertex<T> left, Vertex<T> right)
        {
            if (left is null && right is null)
                return true;
            if(left is null || right is null)
                return false;
            return left.Value.Equals(right.Value);
        }
        public static bool operator !=(Vertex<T> left, Vertex<T> right)
        {
            return !(left == right);
        } 
    }
}
