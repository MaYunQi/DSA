using DSA.Graphs.Entities;

namespace DSA.Graphs
{
    /// <summary>
    /// Weighted, undirectional graph
    /// </summary>
    /// <typeparam name="T">Generics type</typeparam>
    public class WGraph<T>:Graph<T> where T : IEquatable<T>
    {
        public WGraph() : base() { }
        public WGraph(T value) : base(value) { }
        public WGraph(Vertex<T> vertex) : base(vertex) { }
        public void AddEdge(Vertex<T> from, Vertex<T> to, int weight)
        {
            Edge<T> edge = Edges.FirstOrDefault(v => v.From.IsIdenticalTo(from) && v.To == null);
            if (edge != null)
                Edges.Remove(edge);
            Edges.Add(new Edge<T>(from, to,weight));
            edge = Edges.FirstOrDefault(v => v.From.IsIdenticalTo(to) && v.To == null);
            if (edge != null)
                Edges.Remove(edge);
            Edges.Add(new Edge<T>(to, from,weight));
        }
        public void ChangeWeight(Vertex<T> from, Vertex<T> to, int weight)
        {
            AddEdge(from, to, weight);
        }
        public void LinkVertexTo(Vertex<T> node, Vertex<T> to, int weight)
        {
            if (node != null && to != null)
            {
                AddEdge(node, to, weight);
            }
        }
        public void LinkVertexToGenesisVertex(Vertex<T> node,int weight)
        {
            if (node == null || GenesisVertex == null)
                return;
            AddEdge(node, GenesisVertex, weight);
        }
        public int GetShortestPath(Vertex<T> from, Vertex<T> to)
        {
            if(Count==0)
                return 0;

            return 1;
        }
        public override void PrintGraph()
        {
            Console.WriteLine("From: " + "\t" + "To: " + "\t"+ "Weight");
            Console.WriteLine("----------------------");
            foreach (var edge in Edges)
            {
                if (edge.To != null)
                    Console.WriteLine(edge.From.Value + "\t" + edge.To.Value + "\t" +edge.Weight);
                else
                    Console.WriteLine(edge.From.Value + "\t" + "Null"+ "\t" + edge.Weight);
            }
            Console.WriteLine("----------------------");
        }
    }
}
