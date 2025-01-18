using DSA.Tree.Nodes;

namespace DSA.Tree.BinaryTree
{
    public class MaxHeap<T> where T : IComparable<T>, IEquatable<T>
    {
        public TreeNode<T>? Root { get;private set; }
        public uint Count { get; private set; }
        public MaxHeap() { }
        public MaxHeap(T value)
        {
            Root = new TreeNode<T>(value);
            Count = 1;
        }
        public void Insert(T value)
        {
            TreeNode<T> node = new TreeNode<T>(value);
            if (Root == null)
            {
                Root = node;
                Count++;
                return;
            }
            TreeNode<T> parent = GetParentNodeOfInsertNode();
            if (parent.Left == null)
                parent.Left = node;
            else
                parent.Right = node;
            Count++;
            if (node.Value.CompareTo(parent.Value) > 0)
                BubbleUp(node, parent);
        }
        private void BubbleUp(TreeNode<T> node, TreeNode<T> parent)
        {
            while (parent != null && node.Value.CompareTo(parent.Value) > 0)
            {
                SwapNodeValue(node, parent);
                node = parent;
                parent = GetParentNode(node);
            }
        }
        private void SwapNodeValue(TreeNode<T> a, TreeNode<T> b)
        {
            T temp = a.Value;
            a.Value = b.Value;
            b.Value = temp;
        }
        public void Remove(T value)
        {
            if (value == null || Root == null)
                return;
            if (value.CompareTo(Root.Value) > 0)
                return;
            Stack<TreeNode<T>> nodeStack = GetNodeStackByValue(value);
            if (nodeStack == null || nodeStack.Count == 0)
                return;
            while (nodeStack.Count > 0)
            {
                TreeNode<T> nodeToRemove = nodeStack.Pop();
                TreeNode<T> lastNode = GetLastNode();
                SwapNodeValue(nodeToRemove, lastNode);
                TreeNode<T> parent = GetParentNode(lastNode);
                if (lastNode == parent.Left)
                    parent.Left = null;
                else
                    parent.Right = null;
                if (nodeToRemove.Left == null && nodeToRemove.Right == null)
                {
                    parent = GetParentNode(nodeToRemove);
                    if (nodeToRemove.Value.CompareTo(parent.Value) > 0)
                        BubbleUp(nodeToRemove, parent);
                }
                TreeNode<T> child = GetTheLargerChildNode(nodeToRemove);
                ShiftDown(nodeToRemove, child);
                Count--;
            }
        }
        public void RemoveRoot()
        {
            if(Root.Left==null&&Root.Right==null)
            {
                Root = null;
                Count--;
                return;
            }
            TreeNode<T> lastNode= GetLastNode();
            SwapNodeValue(Root, lastNode);
            TreeNode<T> parentOfLast= GetParentNode(lastNode);
            if(lastNode==parentOfLast.Left)
                parentOfLast.Left = null;
            else
                parentOfLast.Right = null;
            TreeNode<T> nodeToRemove = Root;
            TreeNode<T> child= GetTheLargerChildNode(nodeToRemove);
            ShiftDown(nodeToRemove, child);
            Count--;
        }
        private TreeNode<T> GetTheLargerChildNode(TreeNode<T> node)
        {
            if (node == null)
                return null;
            TreeNode<T> largerChild;
            if (node.Left != null && node.Right != null)
                largerChild = node.Left.Value.CompareTo(node.Right.Value) > 0 ? node.Left : node.Right;
            else if (node.Left == null && node.Right == null)
                largerChild = null;
            else
                largerChild = node.Left;
            return largerChild;
        }
        public void ShiftDown(TreeNode<T> node, TreeNode<T> child)
        {
            while (child != null && node.Value.CompareTo(child.Value) < 0)
            {
                SwapNodeValue(node, child);
                node = child;
                child=GetTheLargerChildNode(node);
            }
        }
        private TreeNode<T> GetLastNode()
        {
            if (Root == null)
                return null;
            TreeNode<T> current = Root;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                if (current.Left != null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
            return current;
        }
        private TreeNode<T> GetParentNode(TreeNode<T> node)
        {
            if (Root == null || node == null)
                return null;
            if (Root.IsIdenticalTo(node))
                return null;
            TreeNode<T> current = Root;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                if (current.Left.IsIdenticalTo(node) || current.Right.IsIdenticalTo(node))
                    return current;
                if (current.Left != null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
            return null;
        }
        private TreeNode<T> GetParentNodeOfInsertNode()
        {
            if (Root == null)
                return null;
            TreeNode<T> current = Root;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                TreeNode<T> node = queue.Dequeue();
                if (node.Left == null || node.Right == null)
                    return node;
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
            return null;
        }
        public int GetHeapHeight()
        {
            if (Root == null)
                return 0;
            TreeNode<T> current = Root;
            int height = 1;
            while (current.Left != null)
            {
                current = current.Left;
                height++;
            }
            return height;
        }
        public bool Contains(T value)
        {
            return GetNodeByValue(value) == null ? false : true;
        }
        public TreeNode<T> GetNodeByValue(T value)
        {
            if (value == null)
                return null;
            TreeNode<T> current = Root;
            Queue<TreeNode<T>> quque = new Queue<TreeNode<T>>();
            quque.Enqueue(current);
            while (quque.Count > 0)
            {
                TreeNode<T> node = quque.Dequeue();
                if (value.CompareTo(node.Value) > 0)
                    return null;
                else if (value.Equals(node.Value))
                    return node;
                else
                {
                    if (node.Left != null)
                        quque.Enqueue(node.Left);
                    if (node.Right != null)
                        quque.Enqueue(node.Right);
                }
            }
            return null;
        }
        public Stack<TreeNode<T>> GetNodeStackByValue(T value)
        {
            Stack<TreeNode<T>> nodeStack = new Stack<TreeNode<T>>();
            if (value == null || Root == null)
                return nodeStack;
            TreeNode<T> current = Root;
            Queue<TreeNode<T>> quque = new Queue<TreeNode<T>>();
            quque.Enqueue(current);
            while (quque.Count > 0)
            {
                TreeNode<T> node = quque.Dequeue();
                if (value.Equals(node.Value))
                    nodeStack.Push(node);
                if (node.Left != null)
                    quque.Enqueue(node.Left);
                if (node.Right != null)
                    quque.Enqueue(node.Right);
            }
            return nodeStack;
        }
        public T GetMaxValue()
        {
            if (Root == null)
                throw new NullReferenceException("Heap is null.");
            return Root.Value;
        }
        public void Clear()
        {
            Root = null;
            Count = 0;
        }
        public void PrintTheTree()
        {
            if (Root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            TreeNode<T> node = Root;
            Queue<TreeNode<T>> queue = new Queue<Nodes.TreeNode<T>>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                int leveNodeCount = queue.Count;
                for (int i = 0; i < leveNodeCount; i++)
                {
                    TreeNode<T> current = queue.Dequeue();
                    Console.Write(current.Value + "\t");
                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if (current.Right != null)
                        queue.Enqueue(current.Right);
                }
                Console.WriteLine();
            }
        }
    }
}
