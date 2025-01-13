namespace DSA.StackAndQueue.Interfaces
{
    public interface IStack<T>
    {
        T Peek();
        void Push(T value);
        void Pop();
        uint GetLength();
        bool IsEmpty();
        void Clear();
    }
}
