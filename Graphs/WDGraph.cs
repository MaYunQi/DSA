using DSA.Graphs.Entities;

namespace DSA.Graphs
{
    /// <summary>
    /// Weighted, Directional Graph
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WDGraph<T> : WGraph<T> where T :IComparable<T>,IEquatable<T>
    {
        public WDGraph(): base(){ }
        public WDGraph(T value) : base(value) { }
        public WDGraph(Vertex<T> vertex) : base(vertex) { }

        public override void AddEdge(Vertex<T> from, Vertex<T> to,int weight)
        {
            if (from == null || to == null)
                return;
            if (Edges.FirstOrDefault(e => e.From.IsIdenticalTo(from) && e.To.IsIdenticalTo(to)) != null)
                return;
            Edge<T> edge=new Edge<T>(from, to, weight);
            Edges.Add(edge);
        }
        public override void RemoveEdge(Vertex<T> from, Vertex<T> to)
        {
            Edge<T> e = Edges.FirstOrDefault(e => e.From.IsIdenticalTo(from) && e.To.IsIdenticalTo(to));
            if (e == null) 
                return;
            Edges.Remove(e);
        }
        /// <summary>
        /// Link vertex to genesis vertex, Direction is vertex to genesis vertex, if the direction>0,
        /// add edge from vertex to enesis, if the direction<0, then add edge from gensisi to vertex.
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="weight"></param>
        public void LinkVertexToGenesisVertex(Vertex<T> vertex, int weight, int direction=1)
        {
            if(direction>0)
                AddEdge(vertex, GenesisVertex,weight);
            if(direction<0)
                AddEdge(GenesisVertex, GenesisVertex, weight);
        }
    }
}
