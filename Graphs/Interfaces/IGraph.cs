using DSA.Graphs.Entities;

namespace DSA.Graphs.Interfaces
{
    public interface IGraph<T> where T : IEquatable<T>
    {
        List<Vertex<T>> GetAllVerticsByValue(T value);
        List<Vertex<T>> GetAllNeighbors(Vertex<T> vertex);
        Vertex<T> GetVertex(Vertex<T> vertex);
        Vertex<T> GetFirstVerticsByValue(T value);
        void AddVertex(Vertex<T> vertex);
        void AddValue(T value);
        void AddEdge(Vertex<T> left, Vertex<T> right);
        void LinkVertexToGenesisVertex(Vertex<T> node);
        void RemoveVertex(Vertex<T> vertex);
        void RemoveValue(T value);
        void RemoveEdge(Vertex<T> left, Vertex<T> right);
        void Clear();
        bool ContainsVertex(Vertex<T> vertex);
        bool ContainsValue(T value);
        bool IsEmpty();
        List<T> GetAllValueByDFS();
        List<Vertex<T>> GetAllVerticesByDFS();
        List<T> GetAllValueByBFS();
        List<Vertex<T>> GetAllVerticesByBFS();
    }
}
