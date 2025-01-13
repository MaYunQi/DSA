
namespace DSA.List
{
    public class MyLinkedList<T> where T : IComparable<T>, IEquatable<T>
    {
        public ListNode<T>? Head { get; set; }
        public uint Length { get; private set; }
        public MyLinkedList() { }
        public MyLinkedList(T value)
        {
            Head=new ListNode<T>(value);
            Length=1;
        }
        public MyLinkedList(ListNode<T> head)
        {
            Head = head;
            Length = 1 ;
        }
        /// <summary>
        /// Find the node in the list
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Reference of the node in the list</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public ListNode<T> GetNode(ListNode<T> node)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if (node == null)
                throw new ArgumentNullException(nameof(node), "Node can't be null.");
            ListNode<T> current = Head;
            while(current!=null)
            {
                if(current==node)
                    return current;
                current = current.Next;
            }
            return null;
        }
        /// <summary>
        /// Return the head of the list.
        /// </summary>
        /// <returns>Head Node</returns>
        public ListNode<T> GetFirst()
        {
            return Head;
        }
        /// <summary>
        /// Return the last node of the list.
        /// </summary>
        /// <returns>Last Node</returns>
        public ListNode<T> GetLast()
        {
            return GetRearNode();
        }
        /// <summary>
        /// Return the middle node of the list
        /// </summary>
        /// <returns>Middle Node</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ListNode<T> GetMiddleNode()
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            ListNode<T> fast = Head;
            ListNode<T> slow = Head;
            while(fast.Next!=null)
            {
                fast = fast.Next.Next;
                slow = slow.Next;
            }
            return slow; 
        }
        /// <summary>
        /// Get Kth node from the list.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public ListNode<T> GetNodeByIndex(uint index)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            ListNode<T> current = Head;
            uint count = 0;
            while (current != null && count < index)
            {
                current = current.Next;
                count++;
            }
            if (current == null)
                throw new IndexOutOfRangeException($"The index {index} is out of the range of list.");
            return current;
        }
        /// <summary>
        /// Return the index of a node, return -1 if node has not found.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Index</returns>
        public int GetIndexOfNode(ListNode<T> node)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if (node == null)
                throw new ArgumentNullException(nameof(node), "Node can't be null.");
            int count = 0;
            ListNode<T> current = Head;
            while(current!=null)
            {
                if(current==node)
                    return count;
                count++;
                current = current.Next;
            }
            return -1;
        }
        /// <summary>
        /// Get node by a value, return null if value is not found in list
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>node</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ListNode<T> GetNodeByValue(T value)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            ListNode<T> current = Head;
            while (current != null) 
            {
                if(current.Value.Equals(value))
                    return current;
                current = current.Next;
            }
            return null;
        }
        /// <summary>
        /// Get the first index by the value, return -1 if value is not found
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Index</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int GetIndexByValue(T value)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            ListNode<T> current = Head;
            int count = 0;
            while (current != null)
            {
                if (current.Value.Equals(value))
                    return count;
                current = current.Next;
                count++;
            }
            return -1;
        }
        /// <summary>
        /// Return the rear node of the list.
        /// </summary>
        /// <returns>Rear Node</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ListNode<T> GetRearNode()
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            ListNode<T> current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            return current;
        }
        /// <summary>
        /// Return the previous node. Return null if node has not found.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>Previouse Node</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ListNode<T> GetPreviousNode(ListNode<T> node)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if (Head == node)
                return null;
            ListNode<T> current = Head;
            while(current!=null)
            {
                if (current.Next == node)
                    return current; 
                current = current.Next;
            }
            return null;
        }
        /// <summary>
        /// Get the previous node over a value, return null if the value has not found.
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Previous Node</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public ListNode<T> GetPreviousNodeByValue(T value)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if (Head.Value.Equals(value))
                return null;
            ListNode<T> current = Head;
            while (current != null)
            {
                if (current.Next.Value.Equals(value))
                    return current;
                current = current.Next;
            }
            return null;
        }
        /// <summary>
        /// Return the previous node if a index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Previous Node</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public ListNode<T> GetPreviousNodeByIndex(uint index)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if (index == 0)
                return null;
            if (index >= Length)
                throw new IndexOutOfRangeException($"The index {index} is out of the range of list.");
            ListNode<T> current = Head;
            int count = 0;
            while(count<index-1)
            {
                current=current.Next; 
                count++;
            }
            return current;
        }
        /// <summary>
        /// Test whether the list contains the node.
        /// </summary>
        /// <param name="node">Node</param>
        /// <returns>True of False</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Contains(ListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node), "Node can't be null.");
            ListNode<T> local = GetNode(node);
            return local == null?false:true;
        }
        /// <summary>
        /// Test whether the list contains the node with the value
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>True or False</returns>
        public bool Contains(T value)
        {
            ListNode<T> node=GetNodeByValue(value);
            return node==null?false:true;
        }
        /// <summary>
        /// Append the new node to the rear of the linked list.
        /// </summary>
        /// <param name="node">The node needs to be appended</param>
        public void Append(ListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node), "Node can't be null.");
            if (Head == null)
                Head = node;
            else
            {
                ListNode<T> rear = GetRearNode();
                rear.Next = node;
            }
            Length++;
        }
        /// <summary>
        /// Add a node in the head
        /// </summary>
        /// <param name="node">Node</param>
        public void AddFirst(ListNode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node), "Node can't be null.");
            if(Head==null)
                Head = node;
            else
            {
                node.Next = Head;
                Head = node;
            }
            Length++;
        }
        public void AddFirst(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value)+"is null");
            ListNode<T> node = new ListNode<T>(value);
            AddFirst(node);
        }
        /// <summary>
        /// Add a node in the rear.
        /// </summary>
        /// <param name="node">Node</param>
        public void AddLast(ListNode<T> node)
        {
            Append(node);
        }
        public void AddLast(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value) + "is null");
            ListNode<T> node = new ListNode<T>(value);
            Append(node);
        }
        /// <summary>
        /// Insert the node to the Kth index of the linked list
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="index">Index</param>
        public void Insert(ListNode<T> node, uint index)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if (node == null)
                throw new ArgumentNullException(nameof(node), "Node can't be null.");
            if (index == 0)
            {
                node.Next = Head;
                Head = node;
                Length++;
                return;
            }
            ListNode<T> previous = GetPreviousNodeByIndex(index);
            node.Next = previous.Next;
            previous.Next = node;
            Length++;
        }
        /// <summary>
        /// Delete a node.
        /// </summary>
        /// <param name="node">Node</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeleteNode(ListNode<T> node)
        {
            if(Head==null)
                throw new InvalidOperationException("The list is empty.");
            if (node == Head)
            {
                Head = Head.Next;
                Length--;
                return;
            }
            ListNode<T> previous= GetPreviousNode(node);
            if (previous == null)
                return;
            previous.Next = node.Next;
            Length--;
        }
        /// <summary>
        /// Delete a node with the value.
        /// </summary>
        /// <param name="value">Value</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeleteNodeByValue(T value)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if(Head.Value.Equals(value))
            {
                Head = Head.Next; 
                Length--;
                return;
            }
            ListNode<T> previous = GetPreviousNodeByValue(value);
            if (previous == null)
                return;
            previous.Next = previous.Next.Next;
            Length--;
        }
        /// <summary>
        /// Delete all node with the input value.
        /// </summary>
        /// <param name="value">Value</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeleteAllNodeWithValue(T value)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            while(Head!=null&&Head.Value.Equals(value))
            {
                Head = Head.Next;
                Length--;
            }
            if(Head==null)
                return;
            ListNode<T> current = Head;
            while(current.Next!=null)
            {
                if(current.Next.Value.Equals(value))
                {
                    current.Next = current.Next.Next;
                    Length--;
                }
                else
                    current = current.Next;
            }
        }
        /// <summary>
        /// Delete a node at the kth index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeleteNodeByIndex(uint index)
        {
            if (Head == null)
                throw new InvalidOperationException("The list is empty.");
            if(index==0)
            {
                Head = Head.Next;
                Length--;
                return;
            }
            ListNode<T> previous=GetPreviousNodeByIndex(index);
            previous.Next = previous.Next.Next;
            Length--;
        }
        /// <summary>
        /// Delete the first node of the list.
        /// </summary>
        public void DeleteFirst()
        {
            DeleteNodeByIndex(0);
        }
        /// <summary>
        /// Delete the last node of the the list.
        /// </summary>
        public void DeleteLast()
        {
            DeleteNodeByIndex(Length-1);
        }
        /// <summary>
        /// Reverse the list.
        /// </summary>
        public void Reverse()
        {
            if(Head==null)
                throw new InvalidOperationException("The list is empty.");
            ListNode<T> previous = null;
            ListNode<T> current = Head;
            while(current!=null)
            {
                ListNode<T> next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            Head = previous;
        }
        /// <summary>
        /// Recursivly reverse the list.
        /// </summary>
        /// <param name="node">Head Node.</param>
        /// <returns>Reversed List</returns>
        public ListNode<T> ReverseRecursivly(ListNode<T> node)
        {
            if (node == null || node.Next == null)
                return node;
            ListNode<T> reversed = ReverseRecursivly(node.Next);
            node.Next.Next = node;
            node.Next = null;
            return reversed;
        }
        /// <summary>
        /// Return true if the list is a cycle,
        /// </summary>
        /// <returns>True or false</returns>
        public bool IsCycle()
        {
            if(Head==null||Head.Next==null)
                return false;
            ListNode<T> fast = Head;
            ListNode<T> slow = Head;
            while(fast!=null&&fast.Next!=null)
            {
                slow = slow.Next;
                fast=fast.Next.Next;
                if(slow==fast)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Return whether the list is sorted with ascending order.
        /// </summary>
        /// <returns>True or False</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool IsSorted()
        {
            if(Head==null)
                throw new InvalidOperationException("The list is empty.");
            ListNode<T> current = Head;
            while(current.Next!=null)
            {
                if(current>current.Next)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Return true if the list is empty.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return Length == 0;
        }
        /// <summary>
        /// Clear the list.
        /// </summary>
        public void Clear()
        {
            Head = null; 
            Length = 0;
        }
    }
}
