using DSA.Graphs;
using DSA.Graphs.Entities;

namespace DSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            WGraph<int> graph = new WGraph<int>(10);
            Random rand1 = new Random();
            Random rand2 = new Random();
            for (int i = 0; i < 5; i++) 
            {
                int x = rand1.Next(0,101);
                int w = rand2.Next(1,21);
                Vertex<int> vertex = new Vertex<int>(x);
                graph.LinkVertexToGenesisVertex(vertex, w);
            }
            Vertex<int> v1 = new Vertex<int>(130);
            graph.AddVertex(v1);
            Vertex<int> v2 = new Vertex<int>(180);
            graph.AddVertex(v2);
            graph.AddEdge(v1, v2,25);
            Vertex<int> v3 = new Vertex<int>(200);
            graph.AddVertex(v3);
            graph.AddEdge(v3, v1,30);
            //graph.RemoveVertex(v1); 
            graph.PrintGraph();
        }
    }
}
