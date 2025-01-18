using DSA.Tree.Nodes;

namespace DSA.Tree.BinaryTree
{
    public class MinHeap<T> where T: IComparable<T>,IEquatable<T>
    {
        public TreeNode<T>? Root { get; private set; }
        public uint Count { get; private set; }
        public MinHeap() { }
        public MinHeap(T value)
        {
            Root = new TreeNode<T>(value);
            Count = 1;
        }
        public void Insert(T value)
        {
            if(Root==null)
            {
                Root = new TreeNode<T>(value);
                Count = 1;
                return;
            }
            TreeNode<T> nodeToInsert=new TreeNode<T>(value);
            TreeNode<T> parent = GetParentNodeOfInsertNode();
            if(parent.Left==null)
                parent.Left = nodeToInsert;
            else
                parent.Right = nodeToInsert;
            if (nodeToInsert.Value.CompareTo(parent.Value) < 0)
                BubbleUp(nodeToInsert, parent);
            Count++;
        }
        private void BubbleUp(TreeNode<T> node, TreeNode<T> parent)
        {
            while(parent!=null&&node.Value.CompareTo(parent.Value)<0)
            {
                TreeNode<T> sibling=node==parent.Left?parent.Right:parent.Left;
                SwapNodeValue(node, parent);
                node = parent;
                if (sibling != null && sibling.Value.CompareTo(node.Value) < 0)
                    SwapNodeValue(sibling, node);
                parent = GetParentNode(node);
            }
        }
        public void Remove(T value) 
        {
            if (value == null || Root == null)
                return;
            if (value.CompareTo(Root.Value) < 0)
                return;
            Stack<TreeNode<T>> stack=GetNodeStackByValue(value);
            while(stack.Count>0)
            {
                TreeNode<T> nodeToRemove=stack.Pop();
                TreeNode<T> lastNode = GetLastNode();
                SwapNodeValue(nodeToRemove, lastNode);
                TreeNode<T> parentOfLast = GetParentNode(lastNode);
                if(lastNode== parentOfLast.Right)
                    parentOfLast.Right = null;
                else
                    parentOfLast.Left = null;
                TreeNode<T> child = GetSmallerChildNode(nodeToRemove);
                if(nodeToRemove.Value.CompareTo(child.Value)>0)
                    ShiftDown(nodeToRemove, child);
                Count--;
            }
        }
        public void RemoveRoot()
        {
            if (Root == null)
                return;
            TreeNode<T> nodeToRemove= Root;
            TreeNode<T> lastNode=GetLastNode();
            SwapNodeValue(nodeToRemove, lastNode);
            TreeNode<T> parent = GetParentNode(lastNode);
            if (lastNode == parent.Right)
                parent.Right = null;
            else
                parent.Left = null;
            TreeNode<T> child = GetSmallerChildNode(nodeToRemove);
            if (nodeToRemove.Value.CompareTo(child.Value) > 0)
                ShiftDown(nodeToRemove, child);
            Count--;
        }
        private void ShiftDown(TreeNode<T> node, TreeNode<T> child)
        {
            while(child!=null&&child.Value.CompareTo(node.Value)<0)
            {
                SwapNodeValue(node, child);
                node = child;
                child = GetSmallerChildNode(node);
            }
        }
        private TreeNode<T> GetSmallerChildNode(TreeNode<T> node)
        {
            if (node.Left == null && node.Right == null)
                return null;
            else if (node.Right == null)
                return node.Left;
            else
                return node.Right.Value.CompareTo(node.Left.Value)<0?node.Right:node.Left;
        }
        public TreeNode<T> GetLastNode()
        {
            if (Root == null)
                return null;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            TreeNode<T> current = Root;
            queue.Enqueue(current);
            while(queue.Count>0)
            {
                current = queue.Dequeue();
                if(current.Left!=null)
                    queue.Enqueue(current.Left);
                if(current.Right!=null)
                    queue.Enqueue(current.Right);
            }
            return current;
        }
        private TreeNode<T> GetParentNode(TreeNode<T> node) 
        {
            if (Root == null)
                return null;
            if (node.IsIdenticalTo(Root))
                return null;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            TreeNode<T> current = Root;
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                if(current.Left.IsIdenticalTo(node)||current.Right.IsIdenticalTo(node))
                    return current;
                if (current.Left != null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
            return null;
        }
        private void SwapNodeValue(TreeNode<T> a, TreeNode<T> b)
        {
            T temp=b.Value;
            b.Value=a.Value;
            a.Value=temp;
        }
        public Stack<TreeNode<T>> GetNodeStackByValue(T value)
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            if (Root == null)
                return stack;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            TreeNode<T> current=Root;
            queue.Enqueue(current);
            while (queue.Count>0)
            {
                current = queue.Dequeue();
                if(current.Value.Equals(value))
                    stack.Push(current);
                if(current.Left!=null)
                    queue.Enqueue(current.Left);
                if(current.Right!=null)
                    queue.Enqueue(current.Right);
            }
            return stack;
        }
        private TreeNode<T> GetParentNodeOfInsertNode()
        {
            if (Root == null)
                return null;
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            TreeNode<T> current = Root;
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                if(current.Left==null||current.Right==null)
                    return current;
                if (current.Left != null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
            return null;
        }
        public bool Contains(T value)
        {
            if(Root==null)
                return false;
            if(value.CompareTo(Root.Value)<0)
                return false;
            Queue<TreeNode<T>> queue=new Queue<TreeNode<T>>();
            TreeNode<T> current=Root;
            queue.Enqueue(current);
            while(queue.Count > 0)
            {
                current = queue.Dequeue();
                if(current.Value.Equals(value))
                    return true;
                if(current.Left!=null)
                    queue.Enqueue(current.Left);
                if(current.Right!=null)
                    queue.Enqueue(current.Right);
            }
            return false;
        }
        public uint GetHeapHeight()
        {
            if (Root == null)
                return 0;
            TreeNode<T> current = Root;
            uint height = 1;
            while(current.Left!=null)
            {
                current = current.Left; 
                height++;
            }
            return height;
        }
        public T GetMinValue()
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
                    Console.Write(current.Value + "    ");
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
