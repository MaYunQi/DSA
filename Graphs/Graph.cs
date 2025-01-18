using DSA.Graphs.Entities;
using DSA.Graphs.Interfaces;

namespace DSA.Graphs
{
    public class Graph<T> : IGraph<T> where T : IEquatable<T>
    {
        public Vertex<T> GenesisVertex { get; private set; }
        public HashSet<Vertex<T>> Vertices { get; private set; }
        public uint Count { get;private set; }
        public Graph()
        {
            GenesisVertex = null;
            Vertices = new HashSet<Vertex<T>>();
            Count = 0;
        }
        public Graph(Vertex<T> vertex):this() 
        {
            GenesisVertex = vertex;
            Vertices.Add(GenesisVertex);
            Count = 1;
        }
        public Graph(T value): this() 
        {
            GenesisVertex=new Vertex<T>(value);
            Vertices.Add(GenesisVertex);
            Count = 1;
        }
        public void AddVertex(Vertex<T> vertex)
        {
            Vertices.Add(vertex);
            Count++;
        }
        public void AddValue(T value)
        {
            Vertex<T> v = new Vertex<T>(value);
            AddVertex(v);
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            Vertex<T> verToRemove=GetVertex(vertex);
            if (verToRemove == null)
                return;
            if (vertex.IsIdenticalTo(GenesisVertex))
                GenesisVertex = GenesisVertex.Edge.First();
            List<Vertex<T>> neighbors=GetAllNeighbors(verToRemove);
            for (int i = 0; i < neighbors.Count; i++) 
            {
                neighbors[i].RemoveEdge(verToRemove);
            }
            verToRemove = null;
            Count--;
        }
        public void RemoveValue(T value)
        {
            List<Vertex<T>> verticsToRemove = GetAllVerticsByValue(value);
            for (int i = 0;i < verticsToRemove.Count;i++)
            {
                Vertex<T> vertex=verticsToRemove[i];
                if (vertex.IsIdenticalTo(GenesisVertex))
                    GenesisVertex = GenesisVertex.Edge.First();
                List<Vertex<T>> neighbors=GetAllNeighbors(vertex);
                for (int j = 0; j < neighbors.Count; j++)
                {
                    neighbors[j].RemoveEdge(vertex);
                }
                vertex = null;
                Count--;
            }
        }
        public List<Vertex<T>> GetAllNeighbors(Vertex<T> vertex)
        {
            List<Vertex<T>> list= new List<Vertex<T>>();
            if (vertex == null)
                return list;
            foreach(var edge in vertex.Edge)
            {
                list.Add(edge);
            }
            return list;
        }
        public void Clear()
        {
            GenesisVertex = null;
            Vertices = null;
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

        public List<T> GetAllValueByDFS()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllValueByBFS()
        {
            throw new NotImplementedException();
        }
        public void AddEdge(Vertex<T> left, Vertex<T> right)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdge(Vertex<T> left, Vertex<T> right)
        {
            throw new NotImplementedException();
        }
        public Vertex<T> GetFirstVerticsByValue(T value)
        {
            if (Count == 0)
                return null;
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            foreach (Vertex<T> ver in Vertices)
            {
                stack.Push(ver);
                while (stack.Count > 0)
                {
                    Vertex<T> v = stack.Pop();
                    if (v.Value.Equals(value))
                        return v;
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        foreach (var u in v.Edge)
                        {
                            if (!visited.Contains(u))
                                stack.Push(u);
                        }
                    }
                }
            }
            return null;
        }
        public List<Vertex<T>> GetAllVerticsByValue(T value)
        {
            List<Vertex<T>> list = new List<Vertex<T>>();
            if (Count == 0)
                return list;
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            foreach (Vertex<T> vertex in Vertices)
            {
                stack.Push(vertex);
                while (stack.Count > 0)
                {
                    Vertex<T> v = stack.Pop();
                    if (v.Value.Equals(value))
                        list.Add(v);
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        foreach (var u in v.Edge)
                        {
                            if (!visited.Contains(u))
                                stack.Push(u);
                        }
                    }
                }
            }
            return list;
        }
        public Vertex<T> GetVertex(Vertex<T> vertex)
        {
            if (Count == 0)
                return null;
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            foreach (Vertex<T> ver in Vertices)
            {
                stack.Push(ver);
                while (stack.Count > 0)
                {
                    Vertex<T> v = stack.Pop();
                    if (vertex.IsIdenticalTo(v))
                        return v;
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        foreach (var u in v.Edge)
                        {
                            if (!visited.Contains(u))
                                stack.Push(u);
                        }
                    }
                }
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
            foreach(Vertex<T> vertex in Vertices)
            {
                stack.Push(vertex);
                while(stack.Count > 0)
                {
                    Vertex<T> v= stack.Pop();
                    if(!visited.Contains(v))
                    {
                        visited.Add(v);
                        list.Add(v);
                        foreach(var u in v.Edge)
                        {
                            if(!visited.Contains(u))
                                stack.Push(u);
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
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
            foreach (Vertex<T> vertex in Vertices)
            {
                queue.Enqueue(vertex);
                while (queue.Count > 0)
                {
                    Vertex<T> v = queue.Dequeue();
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        list.Add(v);
                        foreach (var u in v.Edge)
                        {
                            if (!visited.Contains(u))
                                queue.Enqueue(u);
                        }
                    }
                }
            }
            return list;
        }
        public void LinkVertexTo(Vertex<T> node, Vertex<T> to)
        {
            if(node!=null&&to!=null)
                to.AddEdge(node);
        }
        public void LinkVertexToGenesisVertex(Vertex<T> node)
        {
            if (node == null||GenesisVertex==null)
                return;
            if(!GenesisVertex.ContainsEdge(node))
            {
                GenesisVertex.AddEdge(node);
                if(!Vertices.Contains(node))
                    Count++;
                else
                    Vertices.Remove(node);
            }
        }
    }
}
