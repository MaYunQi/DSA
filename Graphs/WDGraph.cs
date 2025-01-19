using DSA.Graphs.Entities;

namespace DSA.Graphs
{
    public class WDGraph<T> where T : IEquatable<T>
    {
        public Vertex<T>? GenesisVertex { get; set; }
        public HashSet<Edge<T>>? Edges { get; set; }
        public uint Count { get;private set; }
        public WDGraph() { }
        public WDGraph(T value)
        {
            GenesisVertex=new Vertex<T>(value);
        }
    }
}
