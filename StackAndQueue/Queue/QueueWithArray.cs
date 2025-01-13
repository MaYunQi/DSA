
using DSA.StackAndQueue.Interfaces;

namespace DSA.StackAndQueue.Queue
{
    public class QueueWithArray<T> : IQueue<T>
    {
        private T[] array;
        private uint length;
        private uint capacity;
        private uint front;
        private uint back;
        public QueueWithArray()
        {
            capacity = 20;
            array = new T[capacity];
            length = 0;
            front = 0;
            back = 0;
        }
        public QueueWithArray(T value):this() 
        {
            array[0] = value;
            length = 1;
            back = 1;
        }
        public void Clear()
        {
            capacity = 20;
            array = new T[capacity] ;
            length = 0;
            front = 0;
            back = 0;
        }
        public T Dequeue()
        {
            if(length==0)
                throw new ArgumentNullException("The queue is null.");
            T value = array[front];
            front=(front+1)%capacity;
            length--;
            if (capacity>20&&length < capacity / 3)
                ShrinkArray();
            return value;
        }
        public void Enqueue(T value)
        {
            if(length>=capacity)
                ExtendArray();
            array[back] = value;
            back=(back+1)%capacity;
            length++;
        }
        public uint GetLength()
        {
            return length;
        }
        public bool IsEmpty()
        {
            return length == 0;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty!");
            return array[front];
        }

        private void ExtendArray()
        {
            uint newcapacity=capacity*2;
            T[] local = new T[newcapacity];
            for(uint i=0;i<length;i++)
            {
                local[i] = array[(front + i) % capacity];
            }
            capacity = newcapacity;
            array = local;
            front=0;
            back = length;
        }
        private void ShrinkArray()
        {
            uint newcapacity=Math.Max(capacity/2,20);
            T[] local = new T[newcapacity];
            for (uint i = 0; i < length; i++)
            {
                local[i] = array[(front + i) % capacity];
            }
            capacity = newcapacity;
            front=0;
            back=length;
            array = local;
        }
    }
}
