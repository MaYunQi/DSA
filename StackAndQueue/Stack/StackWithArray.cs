
using DSA.StackAndQueue.Interfaces;

namespace DSA.StackAndQueue.Stack
{
    public class StackWithArray<T> : IStack<T>
    {
        private T[] array;
        private uint length;
        private uint capacity=20;
        public StackWithArray() 
        {
            array = new T[capacity];
            length = 0;
        }
        public StackWithArray(T value):this() 
        {
            array[0] = value;
            length++;
        }
        public void Clear()
        {
            capacity = 20;
            array=new T[capacity];
            length=0;
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
            if(length==0)
                throw new ArgumentNullException("Stack is empty.");
            return array[length-1];
        }

        public void Pop()
        {
            if (length == 0)
                throw new ArgumentNullException("Stack is empty.");
            array[length - 1] = default;
            length--;
            if (length < capacity / 3)
                ShrinkArray();
        }
        public void Push(T value)
        {
            if (length >= capacity)
                ExtendArray();
            array[length] = value;
            length++;
        }
        private void ExtendArray()
        {
            capacity = capacity * 2;
            T[] local = new T[capacity];
            for(int i=0;i<length;i++)
            {
                local[i] = array[i];
            }
            array = local;
        }
        private void ShrinkArray()
        {
            capacity = Math.Max(capacity / 2,20);
            T[] local = new T[capacity];
            for (int i = 0; i < length; i++)
            {
                local[i] = array[i];
            }
            array = local;
        }
    }
}
