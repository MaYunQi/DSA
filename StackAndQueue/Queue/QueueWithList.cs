
using DSA.List;
using DSA.StackAndQueue.Interfaces;

namespace DSA.StackAndQueue.Queue
{
    public class QueueWithList<T> : IQueue<T> where T : IComparable<T>, IEquatable<T>
    {
        private MyLinkedList<T>? list;

        public QueueWithList()
        {
            list = new MyLinkedList<T>();
        }
        public void Clear()
        {
            list = null;
        }
        public T Dequeue()
        {
            if(list==null||list.Head==null)
                throw new ArgumentNullException("Queue is null.");
            T value = list.Head.Value;
            list.DeleteFirst();
            return value;
        }
        public void Enqueue(T value)
        {
            ListNode<T> node = new ListNode<T>(value);
            list.AddLast(node);
        }
        public uint GetLength()
        {
            if (list == null)
                return 0;
            return list.Length;
        }

        public bool IsEmpty()
        {
            if(list==null)
                return true;
            return list.Length== 0;
        }
        public T Peek()
        {
            if (list == null)
                throw new ArgumentNullException("Queue is null.");
            return list.GetFirst().Value;
        }
    }
}
