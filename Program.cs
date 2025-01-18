using DSA.Graphs;
using DSA.Graphs.Entities;

namespace DSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            Graph<int> graph=new Graph<int>(10);

            Vertex<int> v1=new Vertex<int>(17);
            graph.LinkVertexToGenesisVertex(v1);
           
            Vertex<int> v2=new Vertex<int>(21);
            v2.AddEdge(v1);
            Vertex<int> v3 = new Vertex<int>(7);
            v3.AddEdge(v1);
            Vertex<int> v4 = new Vertex<int>(154);
            v4.AddEdge(v2);
            v3.AddEdge(v4);
            Vertex<int> v5 = new Vertex<int>(45);
            v5.AddEdge(v2);


            List<Vertex<int>> list=graph.GetAllVerticesByDFS();
            foreach (Vertex<int> vertex in list) 
            {
                Console.Write(vertex.Value+"\t");
            }
        }
    }
}
