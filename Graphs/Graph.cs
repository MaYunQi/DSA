using DSA.Graphs.Entities;
using DSA.Graphs.Interfaces;

namespace DSA.Graphs
{
    /// <summary>
    /// Unweighted, undrectional graph
    /// </summary>
    /// <typeparam name="T">Generics type</typeparam>
    public class Graph<T> : IGraph<T> where T : IEquatable<T>
    {
        public Vertex<T> GenesisVertex { get; private set; }
        public HashSet<Vertex<T>> VerticesSet { get; private set; }
        public HashSet<Edge<T>> Edges { get; private set; }
        public uint Count { get;private set; }
        public Graph()
        {
            GenesisVertex = null;
            VerticesSet= new HashSet<Vertex<T>>();
            Edges = new HashSet<Edge<T>>();
            Count = 0;
        }
        public Graph(Vertex<T> vertex):this() 
        {
            GenesisVertex = vertex;
            VerticesSet.Add(vertex);
            Count = 1;
        }
        public Graph(T value): this(new Vertex<T>(value)) { }
        public void AddVertex(Vertex<T> vertex)
        {
            if (vertex == null||VerticesSet.Contains(vertex))
                return;
            VerticesSet.Add(vertex);
            Count++;
        }
        public void AddValue(T value)
        {
            if (value == null)
                return;
            Vertex<T> v = new Vertex<T>(value);
            AddVertex(v);
        }
        public void RemoveVertex(Vertex<T> vertex)
        {
            if (vertex == null)
                return;
            if (vertex.IsIdenticalTo(GenesisVertex))
            {
                VerticesSet.Remove(GenesisVertex);
                Vertex<T> temp = Edges.FirstOrDefault(v=>v.From.IsIdenticalTo(GenesisVertex)).To;
                RemoveEdgesForRemovedVertex(GenesisVertex);
                GenesisVertex = temp;
            }
            else
            {
                VerticesSet.Remove(vertex);
                RemoveEdgesForRemovedVertex(vertex);
            }
            Count--;
        }
        private void RemoveEdgesForRemovedVertex(Vertex<T> vertex)
        {
            if (vertex == null)
                return;
            Edges.RemoveWhere(e=>e.From.IsIdenticalTo(vertex));
            Edges.RemoveWhere(e=>e.To.IsIdenticalTo(vertex));
        }
        public void RemoveValue(T value)
        {
            if (value == null)
                return;
            List<Vertex<T>> verticsToRemove = GetAllVerticsByValue(value);
            if (verticsToRemove == null || verticsToRemove.Count ==0)
                return;
            for (int i = 0;i < verticsToRemove.Count;i++)
            {
                RemoveVertex(verticsToRemove[i]);
            }
        }
        public List<Vertex<T>> GetAllNeighbors(Vertex<T> vertex)
        {
            List<Vertex<T>> list= new List<Vertex<T>>();
            if (vertex == null||Count==0)
                return list;
            foreach(var edge in Edges)
            {
                if(edge.From.IsIdenticalTo(vertex))
                    list.Add(edge.To);
            }
            return list;
        }
        public void Clear()
        {
            GenesisVertex = null;
            VerticesSet.Clear();
            Edges.Clear();
            Count = 0;
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
        public bool ContainsVertex(Vertex<T> vertex)
        {
            if (Count == 0)
                return false;
            return VerticesSet.Contains(vertex);
        }
        public bool ContainsValue(T value)
        {
            if(value==null)
                return false;
            if (Count == 0)
                return false;
            return VerticesSet.FirstOrDefault(v=>v.Value.Equals(value))!=null;
        }
        public List<Vertex<T>> GetAllVerticsList()
        {
            return VerticesSet.ToList();
        }
        public List<T> GetAllValueByDFS()
        {
            List<T> list= new List<T>();
            if(Count==0)
                return list;
            HashSet<Vertex<T>> verticsSet = VerticesSet;
            while(verticsSet.Count>0)
            {
                Vertex<T> v= verticsSet.FirstOrDefault();
                Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
                HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
                stack.Push(v);
                while (stack.Count > 0)
                {
                    Vertex<T> vertex = stack.Pop();
                    if (!visited.Contains(vertex))
                    {
                        visited.Add(vertex);
                        verticsSet.Remove(vertex);
                        list.Add(vertex.Value);
                        List<Edge<T>> edges = Edges.Where(v => v.From.IsIdenticalTo(vertex)).ToList();
                        foreach (Edge<T> edge in edges)
                        {
                            if (!visited.Contains(edge.To))
                                stack.Push(edge.To);
                        }
                    }
                }
            }
            return list;
        }
        public List<T> GetAllValueByBFS()
        {
            List<T> list = new List<T>();
            if (Count == 0)
                return list;
            HashSet<Vertex<T>> verticsSet=VerticesSet;
            while (verticsSet.Count > 0)
            {
                Vertex<T> v=verticsSet.FirstOrDefault();
                Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
                HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
                queue.Enqueue(v);
                while (queue.Count > 0)
                {
                    Vertex<T> vertex = queue.Dequeue();
                    if (!visited.Contains(vertex))
                    {
                        visited.Add(vertex);
                        verticsSet.Remove(vertex);
                        list.Add(vertex.Value);
                        List<Edge<T>> edges = Edges.Where(v => v.From.IsIdenticalTo(vertex)).ToList();
                        foreach (Edge<T> edge in edges)
                        {
                            if (!visited.Contains(edge.To))
                                queue.Enqueue(edge.To);
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Return true if all ther vertics of graph is connected.
        /// </summary>
        /// <returns></returns>
        public bool CheckConnectivity()
        {
            if(Count==0)
                return false;
            List<Vertex<T>> a = GetVerticesDFSFromGenesis();
            return a.Count == Count;
        }
        public List<Vertex<T>> GetVerticesDFSFromGenesis()
        {
            List<Vertex<T>> list = new List<Vertex<T>>();
            if (Count==0)
                return list;
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            stack.Push(GenesisVertex);
            while (stack.Count > 0)
            {
                Vertex<T> vertex = stack.Pop();
                if (!visited.Contains(vertex))
                {
                    visited.Add(vertex);
                    list.Add(vertex);
                    List<Edge<T>> edges = Edges.Where(e => e.From.IsIdenticalTo(vertex)).ToList();
                    foreach (Edge<T> edge in edges)
                    {
                        if (!visited.Contains(edge.To))
                            stack.Push(edge.To);
                    }
                }
            }
            return list;
        }
        public virtual void AddEdge(Vertex<T> from, Vertex<T> to)
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
            Edges.Add(new Edge<T>(from, to));
            Edges.Add(new Edge<T>(to, from));
        }
        public virtual void RemoveEdge(Vertex<T> from, Vertex<T> to)
        {
            if (from == null || to == null)
                return;
            Edges.RemoveWhere(edge=>edge.From.IsIdenticalTo(from) &&edge.To.IsIdenticalTo(to));
            Edges.RemoveWhere(edge=>edge.From.IsIdenticalTo(to) && edge.To.IsIdenticalTo(from));
        }
        public Vertex<T> GetFirstVerticsByValue(T value)
        {
            if (value == null)
                return null;
            return VerticesSet.FirstOrDefault(v=>v.Value.Equals(value));
        }
        public List<Vertex<T>> GetAllVerticsByValue(T value)
        {
            if (Count == 0||value==null)
                return null;
            return VerticesSet.Where(v=>v.Value.Equals(value)).ToList();
        }
        public Vertex<T> GetVertex(Vertex<T> vertex)
        {
            if (Count == 0||vertex==null)
                return null;
            foreach(Vertex<T> v in VerticesSet)
            {
                if (v.IsIdenticalTo(vertex))
                    return vertex;
            }
            return null;
        }
        public int GetOutDegreeOfVertex(Vertex<T> vertex)
        {
            if (vertex == null || Count == 0)
                return -1;
            List<Edge<T>> edges=Edges.Where(v=>v.From.IsIdenticalTo(vertex)).ToList();
            return edges.Count;
        }
        public int GetInDegreeOfVertex(Vertex<T> vertex)
        {
            if (vertex == null || Count == 0)
                return -1;
            List<Edge<T>> edges = Edges.Where(v => v.To.IsIdenticalTo(vertex)).ToList();
            return edges.Count;
        }
        public List<Vertex<T>> GetAllVerticesByDFS()
        {
            List <Vertex<T>> list = new List<Vertex<T>>();
            if(Count==0)
                return list;
            HashSet<Vertex<T>> verticsSet=VerticesSet;
            while(verticsSet.Count > 0)
            {
                Vertex<T> v=verticsSet.FirstOrDefault();
                Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
                HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
                stack.Push(v);
                while (stack.Count > 0)
                {
                    Vertex<T> vertex = stack.Pop();
                    if (!visited.Contains(vertex))
                    {
                        visited.Add(vertex);
                        verticsSet.Remove(vertex);
                        list.Add(vertex);
                        List<Edge<T>> edges = Edges.Where(e => e.From.IsIdenticalTo(vertex)).ToList();
                        foreach (Edge<T> edge in edges)
                        {
                            if (!visited.Contains(edge.To))
                                stack.Push(edge.To);
                        }
                    }
                }
            }
            return list;
        }
        public List<Vertex<T>> GetAllVerticesByBFS()
        {
            List<Vertex<T>> list = new List<Vertex<T>>();
            if (Count == 0)
                return list;
            HashSet<Vertex<T>> verticsSet=VerticesSet;
            while (verticsSet.Count > 0)
            {
                Vertex<T> v=verticsSet.FirstOrDefault();
                Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
                HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
                queue.Enqueue(v);
                while (queue.Count > 0)
                {
                    Vertex<T> vertex = queue.Dequeue();
                    if (!visited.Contains(vertex))
                    {
                        visited.Add(vertex);
                        verticsSet.Remove(vertex);
                        list.Add(vertex);
                        List<Edge<T>> edges = Edges.Where(v => v.From.IsIdenticalTo(vertex)).ToList();
                        foreach (Edge<T> edge in edges)
                        {
                            if (!visited.Contains(edge.To))
                                queue.Enqueue(edge.To);
                        }
                    }
                }
            }
            return list;
        }
        public virtual void LinkVertexToGenesisVertex(Vertex<T> vertex)
        {
            if (vertex == null||GenesisVertex==null)
                return;
            if (!VerticesSet.Contains(vertex))
                AddVertex(vertex);
            Edges.Add(new Edge<T>(GenesisVertex, vertex));
            Edges.Add(new Edge<T>(vertex, GenesisVertex));
        }
        public virtual void PrintGraph()
        {
            Console.WriteLine("From: "+"\t"+"To: ");
            Console.WriteLine("----------------------");
            foreach (var edge in Edges)
            {
                if(edge.To!=null)
                    Console.WriteLine(edge.From.Value+"\t"+edge.To.Value);
                else
                    Console.WriteLine(edge.From.Value + "\t" + "Null");
            }
            Console.WriteLine("----------------------");
        }
    }
}
