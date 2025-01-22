
namespace DSA.Graphs.Entities
{
    public class Edge<T> where T : IEquatable<T>
    {
        public Vertex<T> From { get;private set; }
        public Vertex<T> To { get;private set; }
        public int Weight { get;private set; }
        public Edge(Vertex<T> from, Vertex<T> to)
        {
            From = from;
            To = to;
            Weight = 1;
        }
        public Edge(Vertex<T> from, Vertex<T> to,int weight) : this(from,to)
        {
            Weight = weight;
        }
    }
}
