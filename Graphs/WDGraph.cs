
using DSA.Graphs.Entities;
using System.Collections.Generic;

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
            Edge<T> e=Edges.FirstOrDefault(e=>e.Weight==weight&&e.From.IsIdenticalTo(from)&&e.To.IsIdenticalTo(to));
            if (e == null)
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
        /// Link vertex to genesis vertex, Direction is vertex to genesis vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="weight"></param>
        public  void LinkVertexToGenesisVertex(Vertex<T> vertex, int weight, int direction=1)
        {
            if(direction>0)
                AddEdge(vertex, GenesisVertex,weight);
            if(direction<0)
                AddEdge(GenesisVertex, GenesisVertex, weight);
        }
        public List<Edge<T>> GetShortestPathDijkstra(Vertex<T> from, Vertex<T> to)
        {
            List <Edge<T>> shortestPath= new List<Edge<T>>();
            if(Count==0)
                return shortestPath;
            throw new NotImplementedException();
        }
        public List<Edge<T>> GetShortestPathFord(Vertex<T> from, Vertex<T> to)
        {
            List<Edge<T>> shortestPath = new List<Edge<T>>();
            if (Count == 0)
                return shortestPath;
            throw new NotImplementedException();
        }
    }
}
