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
            Edge<T> edge = Edges.FirstOrDefault(v => v.From.IsIdenticalTo(from));
            if (edge == null)
                AddCountByOne();
            edge = Edges.FirstOrDefault(v => v.From.IsIdenticalTo(to));
            if (edge == null)
                AddCountByOne();
            edge = Edges.FirstOrDefault(v => v.From.IsIdenticalTo(from) && v.To == null);
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
            Edge<T> edge = Edges.FirstOrDefault(v => v.From.IsIdenticalTo(from)&&v.To.IsIdenticalTo(to));
            Edges.Remove(edge);
            edge= Edges.FirstOrDefault(v => v.From.IsIdenticalTo(to) && v.To.IsIdenticalTo(from));
            Edges.Remove(edge);
            Edges.Add(new Edge<T>(from, to, weight));
            Edges.Add(new Edge<T>(to, from, weight));
        }
        public void LinkVertexToGenesisVertex(Vertex<T> node,int weight)
        {
            if (node == null || GenesisVertex == null)
                return;
            AddEdge(node, GenesisVertex, weight);
        }
        public List<Edge<T>> GetMinimalSpanningTreePrim()
        {
            List<Edge<T>> mst= new List<Edge<T>>();
            if(Count==0|| !CheckConnectivity())
                return mst;
            HashSet<Vertex<T>> connectedVertics = new HashSet<Vertex<T>>();
            connectedVertics.Add(GenesisVertex);
            while (connectedVertics.Count!=Count)
            {
                List<Edge<T>> edges=new List<Edge<T>>();
                foreach (var vertex in connectedVertics)
                {
                    List<Edge<T>> list = Edges.Where(e => e.From.IsIdenticalTo(vertex) && e.To != null&&!connectedVertics.Contains(e.To)).ToList();
                    edges.AddRange(list);
                }
                if (edges != null || edges.Count != 0)
                {
                    Edge<T> shortestEdge = edges.MinBy(e => e.Weight);
                    mst.Add(shortestEdge);
                    connectedVertics.Add(shortestEdge.To);
                }
                else
                    return null;
            }
            return mst;
        }
        public List<Edge<T>> GetMinimalSpanningTreeKurskal()
        {
            List<Edge<T>> mst = new List<Edge<T>>();
            if (Count == 0)
                return null;
            //To be implemented
            return mst;
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
