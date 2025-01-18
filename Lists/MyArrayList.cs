
namespace DSA.Lists
{
    public class MyArrayList<T> where T : IComparable<T>,IEquatable<T>
    {
        private T[] array;
        public uint Capacity { get; private set; } = 20;
        public uint Count { get; private set; }
        public MyArrayList()
        {
            array = new T[Capacity];
        }
        public MyArrayList(uint capacity)
        {
            array=new T[capacity];
            Capacity = capacity;
        }
        public MyArrayList(T value,uint capacity) :this(capacity)
        {
            array[0] = value;
            Count++;
        }
        /// <summary>
        /// Return the first element of the array.
        /// </summary>
        /// <returns>First element</returns>
        public T GetFirst()
        {
            return array[0];
        }
        /// <summary>
        /// Return the last element of the array.
        /// </summary>
        /// <returns>Last Element</returns>
        public T GetLast()
        {
            return array[Count - 1];
        }
        /// <summary>
        /// Return the element at the index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Element</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T GetItemByIndex(uint index)
        {
            if(index>Count)
                throw new IndexOutOfRangeException($"The index {index} is out of range.");
            return array[index];
        }
        /// <summary>
        /// Return the index by a item value, return -1 if not found.
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Index</returns>
        public int GetIndexOfItem(T value)
        {
             for(uint i=0;i<Count;i++)
            {
                if (array[i].Equals(value))
                    return (int)i;
            }
            return -1;
        }
        /// <summary>
        /// Add new element to the end of the array
        /// </summary>
        /// <param name="value">Value</param>
        public void AddLast(T value)
        {
            if (Count >= Capacity)
                ExtendArray();
            array[Count] = value;
            Count++;
        }
        /// <summary>
        /// Insert new value to the head of the array
        /// </summary>
        /// <param name="value">Value</param>
        public void AddFirst(T value)
        {
            if (Count >= Capacity)
                ExtendArray();
            for(uint i=Count;i>0;i--)
            {
                array[i]=array[i-1];
            }
            array[0] = value;
            Count++;
        }
        /// <summary>
        /// Insert the item at the index.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="index">Index</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void InsertAt(T value, uint index)
        {
            if (index > Count)
                throw new IndexOutOfRangeException($"The index {index} is out of range.");
            if (Count >= Capacity) 
                ExtendArray();
            for(uint i=Count;i>index;i--)
            {
                array[i]=array[i-1];
            }
            array[index]=value;
            Count++;
        }
        /// <summary>
        /// Replace the value at the index
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="index">Index</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void ReplaceAt(T value,uint index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException($"The index {index} is out of range.");
            array[index]=value; 
        }
        /// <summary>
        /// Delete the first item with the value
        /// </summary>
        /// <param name="value">Value</param>
        public void DeleteFirstItem(T value)
        {
            for(uint i=0;i<Count;i++)
            {
                if (array[i].Equals(value))
                {
                    for(uint j=i;j<Count;j++)
                    {
                        array[j]=array[j+1];
                    }
                    array[Count - 1] = default;
                    Count--;
                    if (Count < Capacity / 3)
                        ShrinkArray();
                    return;
                }
            }
        }
        /// <summary>
        /// Delete all items with the value
        /// </summary>
        /// <param name="value">Value</param>
        public void DeleteAllItemWithValue(T value)
        {
            uint shift = 0;
            for(uint i=0;i<Count;i++)
            {
                if (array[i].Equals(value))
                    shift++;
                else if(shift>0)
                    array[i-shift]=array[i];
            }
            for(uint i=Count-shift;i<Count;i++)
            {
                array[i]=default;
            }
            Count-= shift;
            if (Count < Capacity / 3)
                ShrinkArray();
        }
        /// <summary>
        /// Delete the item at the index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void DeleteAt(uint index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException($"The index {index} is out of range.");
            for(uint i=index;i<Count;i++)
            {
                array[i]=array[i+1];
            }
            array[Count - 1] = default;
            Count--;
            if (Count < Capacity / 3)
                ShrinkArray();
        }
        /// <summary>
        /// Return true if the array is empty.
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }
        /// <summary>
        /// Return true if the arraylist is sorted with ascending order.
        /// </summary>
        /// <returns>True or false</returns>
        public bool IsSorted()
        {
            for(int i = 0;i < Count;i++)
            {
                if (array[i].CompareTo(array[i+1])>0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Set the array list to the default state.
        /// </summary>
        public void Clear()
        {
            Count = 0;
            Capacity = 20;
            array = new T[Capacity];
        }
        /// <summary>
        /// Reverse the array list
        /// </summary>
        public void Reverse()
        {
            for (uint i = 0; i < Count/2;i++)
            {
                T temp=array[i];
                array[i]=array[Count-1-i];
                array[Count-1-i]=temp;
            }
        }
        /// <summary>
        /// Extend the array
        /// </summary>
        private void ExtendArray()
        {
            Capacity = Capacity * 2;
            T[] newArray = new T[Capacity];
            for(int i=0;i<Count;i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
        }
        /// <summary>
        /// Shrink the array
        /// </summary>
        private void ShrinkArray()
        {
            Capacity = Capacity/2;
            T[] newArray = new T[Capacity];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
        }
        public T this[uint index]
        {
            get 
            { 
                if(index>=Count)
                    throw new IndexOutOfRangeException($"The index {index} is out of range.");
                return array[index];
            }
            set 
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException($"The index {index} is out of range.");
                array[index]=value;
            }
        }
    }
}
