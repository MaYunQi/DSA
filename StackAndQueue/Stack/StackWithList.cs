using DSA.Lists;
using DSA.StackAndQueue.Interfaces;

namespace DSA.StackAndQueue.Stack
{
    public class StackWithList<T> : IStack<T> where T:IComparable<T>, IEquatable<T>
    {
        private MyLinkedList<T> list;
        public StackWithList()
        {
            list = new MyLinkedList<T>();
        }
        public void Clear()
        {
            list = null;
        }
        public uint GetLength()
        {
            return list.Length;
        }
        public bool IsEmpty()
        {
            return list.Length == 0;
        }
        public T Peek()
        {
            return list.GetLast().Value;
        }
        public void Pop()
        {
            list.DeleteLast();
        }
        public void Push(T value)
        {
            list.AddLast(value);
        }
    }
}
