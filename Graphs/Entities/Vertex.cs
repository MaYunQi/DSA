
namespace DSA.Graphs.Entities
{
    public class Vertex<T> where T : IEquatable<T>
    {
        public T? Value { get; set; }
        public HashSet<Vertex<T>> Edge { get; set; }
        public Vertex()
        {
            Value = default;
            Edge = new HashSet<Vertex<T>>();
        }
        public Vertex(T value)
        {
            Value=value;
            Edge = new HashSet<Vertex<T>>();
        }
        public void AddEdge(T value)
        {
            if(value!=null)
            {
                Vertex<T> vertex = new Vertex<T>();
                AddEdge(vertex);
            }
        }
        public void AddEdge(Vertex<T> vertex)
        {
            if (vertex != null&& !ContainsEdge(vertex))
                Edge.Add(vertex);
            if(vertex!=null&&!vertex.Edge.Contains(this))
                vertex.Edge.Add(this);
        }
        public void RemoveEdge(Vertex<T> vertex)
        {
            if(vertex!=null)
            {
                Edge.Remove(vertex);
                vertex.Edge.Remove(this);
            }
        }
        public bool ContainsEdge(Vertex<T> vertex)
        {
            return Edge.Contains(vertex);
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
