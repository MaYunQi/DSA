using DSA.Graphs;
using DSA.Graphs.Entities;

namespace DSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            WGraph<int> graph = new WGraph<int>(0);
            Vertex<int> b= new Vertex<int>(1);
            Vertex<int> c = new Vertex<int>(2);
            Vertex<int> d = new Vertex<int>(3);
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddEdge(b, graph.GenesisVertex,3);
            graph.AddEdge(c, graph.GenesisVertex, 7);
            graph.AddEdge(d, graph.GenesisVertex, 6);
            Vertex<int> e = new Vertex<int>(4);
            Vertex<int> f = new Vertex<int>(5);
            Vertex<int> g = new Vertex<int>(6);
            graph.AddVertex(e);
            graph.AddVertex(f);
            graph.AddVertex(g);
            graph.AddEdge(e,b,5);
            graph.AddEdge(e, f, 4);
            graph.AddEdge(b, f, 8);
            graph.AddEdge(f, c, 10);
            graph.AddEdge(f, g, 9);
            graph.AddEdge(g, c, 2);
            graph.AddEdge(g, d, 1);
            List<Edge<int>> mst = graph.GetMinimalSpanningTreePrim();
            foreach(Edge<int> edge in mst)
            {
                Console.WriteLine(edge.From.Value+"\t"+edge.To.Value+"\t"+edge.Weight);
            }
        }
    }
}
