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
        public override void AddEdge(Vertex<T> from, Vertex<T> to)
        {
            AddEdge(from, to, 1);
        }
        public virtual void AddEdge(Vertex<T> from, Vertex<T> to, int weight)
        {
            if (from == null || to == null)
                return;
            if (Edges.FirstOrDefault(e => e.From.IsIdenticalTo(from) && e.To.IsIdenticalTo(to)) != null)
                return;
            if (Edges.FirstOrDefault(e => e.From.IsIdenticalTo(to) && e.To.IsIdenticalTo(from)) != null)
                return;
            if (!VerticesSet.Contains(from))
                AddVertex(from);
            if (!VerticesSet.Contains(to))
                AddVertex(to);
            Edges.Add(new Edge<T>(from, to,weight));
            Edges.Add(new Edge<T>(to, from, weight));
        }
        public virtual void ChangeWeight(Vertex<T> from, Vertex<T> to, int weight)
        {
            if (from == null || to == null)
                return;
            Edges.RemoveWhere(e=>e.From.IsIdenticalTo(from)&&e.To.IsIdenticalTo(to));
            Edges.RemoveWhere(e=>e.From.IsIdenticalTo(to)&&e.To.IsIdenticalTo(from));
            Edges.Add(new Edge<T>(from, to, weight));
            Edges.Add(new Edge<T>(to, from, weight));
        }
        public override void LinkVertexToGenesisVertex(Vertex<T> vertex)
        {
            LinkVertexToGenesisVertex(vertex, 1);
        }
        public virtual void LinkVertexToGenesisVertex(Vertex<T> vertex, int weight)
        {
            if (vertex == null || GenesisVertex == null)
                return;
            AddEdge(vertex, GenesisVertex, weight);
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
                    List<Edge<T>> list = Edges.Where(e => e.From.IsIdenticalTo(vertex)&&!connectedVertics.Contains(e.To)).ToList();
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
        public List<Edge<T>> GetMinimalSpanningTreePrimOptimized()
        {
            List<Edge<T>> mst = new List<Edge<T>>();
            if (Count == 0 || !CheckConnectivity())
                return mst;
            HashSet<Vertex<T>> connectedVertics = new HashSet<Vertex<T>>();
            connectedVertics.Add(GenesisVertex);
            PriorityQueue<Edge<T>,int> priorityQueue=new PriorityQueue<Edge<T>,int>();
            foreach(var edge in Edges.Where(e=>e.From.IsIdenticalTo(GenesisVertex)))
            {
                priorityQueue.Enqueue(edge,edge.Weight);
            }
            while (connectedVertics.Count != Count)
            {
                while (priorityQueue.Count > 0) 
                {
                    Edge<T>  shortestPath=priorityQueue.Dequeue();
                    if(!connectedVertics.Contains(shortestPath.To))
                    {
                        mst.Add(shortestPath);
                        connectedVertics.Add(shortestPath.To);
                        foreach(var edge in Edges.Where(e => e.From.IsIdenticalTo(shortestPath.To)))
                        {
                            priorityQueue.Enqueue(edge, edge.Weight);
                        }
                        break;
                    }
                }
                if (priorityQueue.Count == 0 && connectedVertics.Count < Count)
                    return null;
            }
            return mst;
        }
        public List<Edge<T>> GetMinimalSpanningTreeKurskal()
        {
            List<Edge<T>> mst = new List<Edge<T>>();
            if (Count == 0)
                return null;
            Dictionary<Vertex<T>, HashSet<Vertex<T>>> comps=new Dictionary<Vertex<T>, HashSet<Vertex<T>>>();
            foreach (var vertex in VerticesSet) 
            {
                comps[vertex] = new HashSet<Vertex<T>> {vertex};
            }
            PriorityQueue<Edge<T>,int> priorityQueue=new PriorityQueue<Edge<T>,int>();
            foreach(var edge in Edges)
            {
                priorityQueue.Enqueue(edge,edge.Weight);
            }
            while(mst.Count<Count-1&&priorityQueue.Count>0)
            {
                Edge<T> shortestPath=priorityQueue.Dequeue();
                var from = comps[shortestPath.From];
                var to = comps[shortestPath.To];
                if(from!=to)
                {
                    mst.Add(shortestPath);
                    from.UnionWith(to);
                    foreach (var vertex in to)
                    {
                        comps[vertex] = from;
                    }
                }
            }
            if (mst.Count != Count - 1)
                return null;
            return mst;
        }
        public List<Edge<T>> GetShortestPathDijkstra(Vertex<T> from, Vertex<T> to)
        {
            List<Edge<T>> shortestPath = new List<Edge<T>>();
            if (Count == 0)
                return shortestPath;
            List<Vertex<T>> vertices=GetAllVerticsList();
            throw new NotImplementedException();
        }
        public List<Edge<T>> GetShortestPathFord(Vertex<T> from, Vertex<T> to)
        {
            List<Edge<T>> shortestPath = new List<Edge<T>>();
            if (Count == 0)
                return shortestPath;
            throw new NotImplementedException();
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
