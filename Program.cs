using DSA.Algorithms.Backtracking;
using DSA.Graphs;
using DSA.Graphs.Entities;

namespace DSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            WGraph<char> graph = new WGraph<char>('a');
            Vertex<char> b= new Vertex<char>('b');
            Vertex<char> c = new Vertex<char>('c');
            Vertex<char> d = new Vertex<char>('d');
            graph.AddVertex(b);
            graph.AddVertex(c);
            graph.AddVertex(d);
            graph.AddEdge(b, graph.GenesisVertex,3);
            graph.AddEdge(c, graph.GenesisVertex, 7);
            graph.AddEdge(d, graph.GenesisVertex, 6);
            Vertex<char> e = new Vertex<char>('e');
            Vertex<char> f = new Vertex<char>('f');
            Vertex<char> g = new Vertex<char>('g');
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
            List<Edge<char>> mst = graph.GetMinimalSpanningTreeKurskal();
            foreach(Edge<char> edge in mst)
            {
                Console.WriteLine(edge.From.Value+"\t"+edge.To.Value+"\t"+edge.Weight);
            }

            NQueens nQueens = new NQueens();
            List<List<string>> result=nQueens.SolveNQueens(8);
            foreach(List<string> list in result)
            {
                Console.WriteLine("---------------");
                foreach (string s in list)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        Console.Write(s[i]+" ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("---------------");
            }
        }
    }
}
