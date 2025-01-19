
namespace DSA.Graphs.Entities
{
    public class Edge<T> where T : IEquatable<T>
    {
        public Vertex<T> From { get; set; }
        public Vertex<T>? To { get; set; }
        public int Weight { get;private set; }
        public Edge(Vertex<T> from) 
        {
            From=from;
            To=null;
            Weight = -1;
        }
        public Edge(Vertex<T> from, Vertex<T> to):this(from)
        {
            From = from;
            To = to;
            Weight = 1;
        }
        public Edge(Vertex<T> from, Vertex<T> to,int weight) : this(from,to)
        {
            From = from;
            To = to;
            Weight = weight;
        }
        public void AddEdge(Vertex<T> from, Vertex<T> to)
        {
            From = from;
            To = to;
            Weight = 1;
        }
        public void AddEdge(Vertex<T> from, Vertex<T> to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
        public void RemoveEdge(Vertex<T> from, Vertex<T> to)
        {
            From = null;
            To = null;
            Weight = -1;
        }
        public void SetWeight(int weight)
        {
            Weight = weight;
        }
    }
}
