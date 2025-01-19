using DSA.Graphs.Entities;
using DSA.Graphs.Interfaces;

namespace DSA.Graphs
{
    public class Graph<T> : IGraph<T> where T : IEquatable<T>
    {
        public Vertex<T> GenesisVertex { get; private set; }
        public HashSet<Edge<T>> Edges { get; private set; }
        public uint Count { get;private set; }
        public Graph()
        {
            GenesisVertex = null;
            Edges= new HashSet<Edge<T>>();
            Count = 0;
        }
        public Graph(Vertex<T> vertex):this() 
        {
            GenesisVertex = vertex;
            Edge<T> edge = new Edge<T>(GenesisVertex);
            Edges.Add(edge);
            Count = 1;
        }
        public Graph(T value): this(new Vertex<T>(value)) { }
        public void AddVertex(Vertex<T> vertex)
        {
            Edge<T> edge=new Edge<T>(vertex);
            Edges.Add(edge);
            Count++;
        }
        public void AddValue(T value)
        {
            Vertex<T> v = new Vertex<T>(value);
            AddVertex(v);
        }
        public void RemoveVertex(Vertex<T> vertex)
        {
            if (vertex == null)
                return;
            if (vertex.IsIdenticalTo(GenesisVertex))
            { 
                Vertex<T> temp = Edges.FirstOrDefault(v=>v.From.IsIdenticalTo(GenesisVertex)).To;
                RemoveConnection(GenesisVertex);
                GenesisVertex = temp;
            }
            else
                RemoveConnection(vertex);
            vertex = null;
            Count--;
        }
        private void RemoveConnection(Vertex<T> vertex)
        {
            Edges.RemoveWhere(v => v.To!=null&&v.To.IsIdenticalTo(vertex));
            Edges.RemoveWhere(v => v.From.IsIdenticalTo(vertex));
        }
        public void RemoveValue(T value)
        {
            List<Vertex<T>> verticsToRemove = GetAllVerticsByValue(value);
            for (int i = 0;i < verticsToRemove.Count;i++)
            {
                RemoveVertex(verticsToRemove[i]);
            }
        }
        public List<Vertex<T>> GetAllNeighbors(Vertex<T> vertex)
        {
            List<Vertex<T>> list= new List<Vertex<T>>();
            if (vertex == null)
                return list;
            foreach(var edge in Edges)
            {
                if(edge.From.IsIdenticalTo(vertex) &&edge.To!=null)
                    list.Add(edge.To);
            }
            return list;
        }
        public void Clear()
        {
            GenesisVertex = null;
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
            return GetVertex(vertex)!=null;
        }
        public bool ContainsValue(T value)
        {
            if(value==null)
                return false;
            if (Count == 0)
                return false;
            return GetFirstVerticsByValue(value)!=null;
        }
        public List<Vertex<T>> GetAllVertics()
        {
            List<Vertex<T> > list= new List<Vertex<T>>();
            if(Count == 0) 
                return list;
            HashSet<Vertex<T>> set=new HashSet<Vertex<T>>();
            foreach(var edge in Edges)
            {
                set.Add(edge.From);
                if(edge.To!=null)
                    set.Add(edge.To);
            }
            return set.ToList();
        }
        public List<T> GetAllValueByDFS()
        {
            List<T> list= new List<T>();
            if(Count==0)
                return list;
            Stack<Vertex<T>> stack= new Stack<Vertex<T>>();
            HashSet<Vertex<T>> visited= new HashSet<Vertex<T>>();
            stack.Push(GenesisVertex);
            while(stack.Count > 0)
            {
                Vertex<T> vertex= stack.Pop();
                if(!visited.Contains(vertex))
                {
                    visited.Add(vertex);
                    list.Add(vertex.Value);
                    List<Edge<T>> edges = Edges.Where(v => v.From.IsIdenticalTo(vertex)).ToList();
                    foreach(Edge<T> edge in edges)
                    {
                        if(edge.To!=null&&!visited.Contains(edge.To))
                            stack.Push(edge.To);
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
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            queue.Enqueue(GenesisVertex);
            while (queue.Count > 0)
            {
                Vertex<T> vertex = queue.Dequeue();
                if (!visited.Contains(vertex))
                {
                    visited.Add(vertex);
                    list.Add(vertex.Value);
                    List<Edge<T>> edges = Edges.Where(v => v.From.IsIdenticalTo(vertex)).ToList();
                    foreach (Edge<T> edge in edges)
                    {
                        if (edge.To != null && !visited.Contains(edge.To))
                            queue.Enqueue(edge.To);
                    }
                }
            }
            return list;
        }
        public void AddEdge(Vertex<T> left, Vertex<T> right)
        {
            Edges.Add(new Edge<T>(left, right));
            Edges.Add(new Edge<T>(right, left));
        }
        public void RemoveEdge(Vertex<T> left, Vertex<T> right)
        {
            Edges.RemoveWhere(edge=>edge.From.IsIdenticalTo(left)&&edge.To.IsIdenticalTo(right));
            Edges.RemoveWhere(edge => edge.From.IsIdenticalTo(right) && edge.To.IsIdenticalTo(left));
        }
        public Vertex<T> GetFirstVerticsByValue(T value)
        {
            if (Count == 0)
                return null;
            foreach(var edge in Edges)
            {
                if (edge.From.Value.Equals(value))
                    return edge.From;
                else if(edge.To!=null&&edge.To.Value.Equals(value))
                    return edge.To;
            }
            return null;
        }
        public List<Vertex<T>> GetAllVerticsByValue(T value)
        {
            List<Vertex<T>> list = new List<Vertex<T>>();
            if (Count == 0)
                return list;
            HashSet<Vertex<T>> set = new HashSet<Vertex<T>>();
            foreach (var edge in Edges)
            {
                if(edge.From.Value.Equals(value))
                    set.Add(edge.From);
            }
            return set.ToList();
        }
        public Vertex<T> GetVertex(Vertex<T> vertex)
        {
            if (Count == 0)
                return null;
            foreach(Edge<T> edge in Edges)
            {
                if (edge.From.IsIdenticalTo(vertex))
                    return vertex;
            }
            return null;
        }
        public List<Vertex<T>> GetAllVerticesByDFS()
        {
            List <Vertex<T>> list = new List<Vertex<T>>();
            if(Count==0)
                return list;
            Stack<Vertex<T>> stack= new Stack<Vertex<T>>();
            HashSet<Vertex<T>> visited= new HashSet<Vertex<T>>();
            stack.Push(GenesisVertex);
            while (stack.Count > 0)
            {
                Vertex<T> vertex = stack.Pop();
                if(!visited.Contains(vertex))
                {
                    visited.Add(vertex);
                    list.Add(vertex);
                    List<Edge<T>> edges = Edges.Where(e=>e.From.IsIdenticalTo(vertex)).ToList();
                    foreach(Edge<T> edge in edges)
                    {
                        if(edge.To!=null&&!visited.Contains(edge.To))
                            stack.Push(edge.To);
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
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            queue.Enqueue(GenesisVertex);
            while (queue.Count > 0) 
            {
                Vertex<T> vertex= queue.Dequeue();
                if (!visited.Contains(vertex)) 
                {
                    visited.Add(vertex);
                    list.Add(vertex);
                    List<Edge<T>> edges=Edges.Where(v=>v.From.IsIdenticalTo(vertex)).ToList();
                    foreach (Edge<T> edge in edges)
                    {
                        if(edge.To!=null&&!visited.Contains(edge.To))
                            queue.Enqueue(edge.To);
                    }
                }
            }
            return list;
        }
        public void LinkVertexTo(Vertex<T> node, Vertex<T> to)
        {
            if(node!=null&&to!=null)
            {
                Edges.Add(new Edge<T>(node,to));
                Edges.Add(new Edge<T>(to, node));
            }
        }
        public void LinkVertexToGenesisVertex(Vertex<T> node)
        {
            if (node == null||GenesisVertex==null)
                return;
            Edges.Add(new Edge<T>(GenesisVertex, node));
            Edges.Add(new Edge<T>(node, GenesisVertex));
        }
    }
}
