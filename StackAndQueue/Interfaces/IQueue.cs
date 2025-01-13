
namespace DSA.StackAndQueue.Interfaces
{
    public interface IQueue<T>
    {
        T Peek();
        void Enqueue(T value);
        T Dequeue();
        uint GetLength();
        bool IsEmpty();
        void Clear();
    }
}
