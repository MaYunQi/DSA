using DSA.Tree.Interfaces;

namespace DSA.Tree.BinaryTree
{
    public class BinarySearchTree<T>:IBinaryTree<T> where T : IComparable<T>, IEquatable<T>
    {
        public TreeNode<T>? Root { get; set; }
        public uint Count { get; private set; }
        public BinarySearchTree()
        {
            Root=null;
            Count=0;
        }
        public BinarySearchTree(TreeNode<T> node)
        {
            Root = node;
            Count = 1;
        }
        public BinarySearchTree(T value)
        {
            Root = new TreeNode<T>(value);
            Count = 1;
        }
        public void Add(T value)
        {
            TreeNode<T> node = new TreeNode<T>(value);
            if(Count==0)
                Root = node;
            else
            {
                TreeNode<T> current= Root;
                TreeNode<T> parent = null;
                while(current!=null)
                {
                    if (current == node)
                        return;
                    parent = current;
                    if (node > current)
                        current = current.Right;
                    else
                        current = current.Left;
                }
                if(node>parent)
                    parent.Right = node;
                else
                    parent.Left = node;
            }
            Count++;
        }
        public void AddRecursively(T value)
        {
            AddRecursively(Root, value);
            Count++;
        }
        private TreeNode<T> AddRecursively(TreeNode<T> root, T value)
        {
            TreeNode<T> node= new TreeNode<T> (value);
            if(root==null)
            {
                root = node;
                return root;
            }
            if(node==root)
                return root;
            else if (node > root)
                root.Right = AddRecursively(root.Right, value);
            else
                root.Left = AddRecursively(root.Left, value);
            return root;
        }
        public void Remove(T value)
        {
            throw new NotImplementedException();
        }
        public void RemoveRecursively(T value)
        {
            RemoveRecursively(Root, value);
        }
        private TreeNode<T> RemoveRecursively(TreeNode<T> root,T value)
        {
            if (root == null)
                return null;
            if (root.Value.CompareTo(value) < 0)
                root.Right = RemoveRecursively(root.Right, value);
            else if(root.Value.CompareTo(value) >0)
                root.Left= RemoveRecursively(root.Left, value);
            else
            {
                if (root.Left == null && root.Right == null)
                    return null;
                else if (root.Left == null)
                    return root.Right;
                else if(root.Right==null)
                    return root.Left;
                else
                {
                    TreeNode<T> minNode=GetMinNode(root.Right);
                    root.Value=minNode.Value;
                    root.Right=RemoveRecursively(root.Right, minNode.Value);
                }
            }
            return root;
        }
        public TreeNode<T> GetNode(T value)
        {
            if (Count == 0)
                return null;
            TreeNode<T> node=new TreeNode<T> (value);
            TreeNode<T> current = Root;
            while(current!=null)
            {
                if(current==node)
                    return current;
                if(current>node)
                    current=current.Left;
                else
                    current=current.Right;
            }
            return null;
        }
        public TreeNode<T> GetNodeRecursively(T value)
        {
            return GetNodeRecursively(Root, value);
        }
        private TreeNode<T> GetNodeRecursively(TreeNode<T> root, T value)
        {
            if (root == null)
                return null;
            TreeNode<T> node = new TreeNode<T>(value);
            if(node == root)
                return node;
            if (node > root)
                return GetNodeRecursively(root.Right, value);
            if(node<root)
                return GetNodeRecursively(root.Left, value);
            return null;
        }
        public TreeNode<T> GetMinNode()
        {
            if (Root == null)
                return null;
            TreeNode<T> current=Root;
            while(current.Left!=null)
            {
                current = current.Left;
            }
            return current;
        }
        private TreeNode<T> GetMinNode(TreeNode<T> root)
        {
            if (root == null)
                return null;
            while(root.Left!=null)
            {
                root = root.Left;
            }
            return root;
        }
        public TreeNode<T> GetMaxNode()
        {
            if (Root == null)
                return null;
            TreeNode<T> current = Root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current;
        }
        private TreeNode<T> GetMaxNode(TreeNode<T> root)
        {
            if (root == null)
                return null;
            while (root.Right != null)
            {
                root = root.Right;
            }
            return root;
        }
        public List<TreeNode<T>> GetAllNodes()
        {
            List<TreeNode<T>> result= new List<TreeNode<T>>();
            if (Root == null)
                return null;
            Stack<TreeNode<T>> stack=new Stack<TreeNode<T>>();
            TreeNode<T> current = Root;
            while(current!=null||stack.Count>0)
            {
                while(current!=null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                current=stack.Pop();
                result.Add(current);
                current=current.Right;
            }
            return result;
        }
        public List<TreeNode<T>> GetAllNodesRecursively()
        {
            if (Count == 0)
                return null;
            List<TreeNode<T>> result = new List<TreeNode<T>>();
            GetAllNodesPreOrder(Root, result);
            return result;
        }
        private void GetAllNodesPreOrder(TreeNode<T> root, List<TreeNode<T>> list)
        {
            if (root == null)
                return;
            list.Add(root);
            GetAllNodesPreOrder(root.Left, list);
            GetAllNodesPreOrder(root.Right,list);
        }
        public List<T> GetAllNodesValue()
        {
            if (Root == null)
                return null;
            List<T> list = new List<T>();
            Stack<TreeNode<T>> stack= new Stack<TreeNode<T>>();
            TreeNode<T> current = Root;
            while(current!=null||stack.Count>0)
            {
                while(current!=null)
                {
                    stack.Push(current);
                    current=current.Left;
                }
                current=stack.Pop();
                current=current.Right;
            }
            return list;
        }
        public List<T> GetAllNodesValueRecursively()
        {
            if (Count == 0)
                return null;
            List<T> result = new List<T>();
            GetAllNodesValuePreOrder(Root, result);
            return result;
        }
        private void GetAllNodesValuePreOrder(TreeNode<T> root, List<T> list)
        {
            if(root==null)
                return;
            list.Add(root.Value);
            GetAllNodesValuePreOrder(root.Left,list);
            GetAllNodesValuePreOrder(root.Right,list);
        }
        public List<TreeNode<T>> GetAllLeafNodes()
        {
            if (Count == 0)
                return null;
            List<TreeNode<T>> result = new List<TreeNode<T>>();
            TreeNode<T> current=Root;
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(current);
            while(stack.Count>0)
            {
                current = stack.Pop();
                if (current.Left == null && current.Right == null)
                    result.Add(current);
                if(current.Left!=null)
                    stack.Push(current.Left);
                if(current.Right!=null)
                    stack.Push(current.Right);
            }
            return result;
        }
        public List<TreeNode<T>> GetAllLeafNodesRecursively()
        {
            List<TreeNode<T>> list = new List<TreeNode<T>>();
            GetAllLeafNodesRecursively(Root, list);
            return list;
        }
        private void GetAllLeafNodesRecursively(TreeNode<T> root, List<TreeNode<T>> list)
        {
            if(root==null)
                return;
            if(root.Left==null&&root.Right==null)
                list.Add(root);
            GetAllLeafNodesRecursively(root.Left,list);
            GetAllLeafNodesRecursively(root.Right, list);
        }
        public List<T> GetAllLeafNodesValue()
        {
            if (Root == null)
                return null;
            List<T> list = new List<T>();
            Stack<TreeNode<T>> stack=new Stack<TreeNode<T>>();
            TreeNode<T> current = Root;
            stack.Push(current);
            while(stack.Count>0)
            {
                current = stack.Pop();
                if (current.Left == null && current.Right == null)
                    list.Add(current.Value);
                if(current.Left!=null)
                    stack.Push(current.Left);
                if(current.Right!=null)
                    stack.Push(current.Right);
            }
            return list;
        }
        public List<T> GetAllLeafNodesValueRecursively()
        {
            List<T> result = new List<T>();
            GetAllLeafNodesValueRecursively(Root, result);
            return result;
        }
        private void GetAllLeafNodesValueRecursively(TreeNode<T> root, List<T> list)
        {
            if(root==null)
                return;
            if(root.Left==null&&root.Right==null)
                list.Add(root.Value);
            GetAllLeafNodesValueRecursively(root.Left, list);
            GetAllLeafNodesValueRecursively(root.Right, list);
        }
        public List<TreeNode<T>> GetLeftViewNodes()
        {
            if (Root == null)
                return null;
            List<TreeNode<T>> result= new List<TreeNode<T>>();
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(Root);
            while (queue.Count > 0) 
            { 
                int levelNodeCount=queue.Count;
                for(int i=0;i<levelNodeCount;i++)
                {
                    TreeNode<T> current = queue.Dequeue();
                    if(i==0)
                        result.Add(current);
                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if (current.Right != null)
                        queue.Enqueue(current.Right);
                }
            }
            return result;
        }
        public List<TreeNode<T>> GetLeftViewNodesRecursively()
        {
            List<TreeNode<T>> list = new List<TreeNode<T>>();
            GetLeftViewNodesRecursively(Root, 0, list);
            return list;
        }
        private void GetLeftViewNodesRecursively(TreeNode<T> root, uint level, List<TreeNode<T>> list)
        {
            if(root==null)
                return;
            if(list.Count==level)
                list.Add(root);
            GetLeftViewNodesRecursively(root.Left, level + 1, list);
            GetLeftViewNodesRecursively(root.Right, level + 1, list);
        }
        public List<T> GetLeftViewNodesValue()
        {
            if (Root == null)
                return null;
            List<T> result = new List<T>();
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                int levelNodeCount = queue.Count;
                for (int i = 0; i < levelNodeCount; i++)
                {
                    TreeNode<T> current = queue.Dequeue();
                    if (i ==0)
                        result.Add(current.Value);
                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if (current.Right != null)
                        queue.Enqueue(current.Right);
                }
            }
            return result;
        }
        public List<T> GetLeftViewNodesValueRecursively()
        {
            List<T> list = new List<T>();
            GetLeftViewNodesValueRecursively(Root, 0, list);
            return list;
        }
        private void GetLeftViewNodesValueRecursively(TreeNode<T> root, uint level, List<T> list)
        {
            if (root == null)
                return;
            if (list.Count == level)
                list.Add(root.Value);
            GetLeftViewNodesValueRecursively(root.Left, level + 1, list);
            GetLeftViewNodesValueRecursively(root.Right, level + 1, list);
        }
        public List<TreeNode<T>> GetRightViewNodes()
        {
            if (Root == null)
                return null;
            List<TreeNode<T>> result = new List<TreeNode<T>>();
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                int levelNodeCount = queue.Count;
                for (int i = 0; i < levelNodeCount; i++)
                {
                    TreeNode<T> current = queue.Dequeue();
                    if (i == levelNodeCount - i)
                        result.Add(current);
                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if (current.Right != null)
                        queue.Enqueue(current.Right);
                }
            }
            return result;
        }
        public List<TreeNode<T>> GetRightViewNodesRecursively()
        {
            List<TreeNode<T>> list = new List<TreeNode<T>>();
            GetRightViewNodesRecursively(Root,0, list);
            return list;
        }
        private void GetRightViewNodesRecursively(TreeNode<T> root, uint level, List<TreeNode<T>> list)
        {
            if (root == null)
                return;
            if(list.Count==level)
                list.Add(root);
            GetRightViewNodesRecursively(root.Right, level + 1, list);
            GetRightViewNodesRecursively(root.Left,level+1,list);
        }
        public List<T> GetRightViewNodesValue()
        {
            if (Root == null)
                return null;
            List<T> result = new List<T>();
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                int levelNodeCount = queue.Count;
                for (int i = 0; i < levelNodeCount; i++)
                {
                    TreeNode<T> current = queue.Dequeue();
                    if (i == levelNodeCount - i)
                        result.Add(current.Value);
                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if (current.Right != null)
                        queue.Enqueue(current.Right);
                }
            }
            return result;
        }
        public List<T> GetRightViewNodesValueRecursively()
        {
            List<T> list = new List<T>();
            GetRightViewNodesValueRecursively(Root, 0, list);
            return list;
        }
        private void GetRightViewNodesValueRecursively(TreeNode<T> root, uint level, List<T> list)
        {
            if (root == null)
                return;
            if (list.Count == level)
                list.Add(root.Value);
            GetRightViewNodesValueRecursively(root.Right, level + 1, list);
            GetRightViewNodesValueRecursively(root.Left, level + 1, list);
        }
        public bool IsLeaf(TreeNode<T> node)
        {
            if(Contains(node.Value)&&node.Left==null&&node.Right==null)
                return true;
            return false;
        }
        public bool IsLeafValue(T value)
        {
            if(Root==null)
                return false;
            Queue<TreeNode<T>> queue= new Queue<TreeNode<T>>();
            queue.Enqueue(Root);
            while(queue.Count > 0)
            {
                TreeNode<T> current= queue.Dequeue();
                if(current.Left==null&&current.Right==null&&current.Value.Equals(value))
                    return true;
                if(current.Left!=null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
            return false;
        }
        public bool IsLeafValueRecursively(T value)
        {
            return IsLeafValueRecursively(Root, value);
        }
        private bool IsLeafValueRecursively(TreeNode<T> root, T value)
        {
            if(root==null)
                return false;
            if(root.Left==null&&root.Right==null&&root.Value.Equals(value))
                return true;
            bool left=IsLeafValueRecursively(root.Left, value);
            bool right=IsLeafValueRecursively(root.Right, value);
            return left||right;
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
        public void Clear()
        {
            Root = null;
            Count = 0;
        }
        public bool Contains(T value)
        {
            if(Count==0)
                return false;
            TreeNode<T> node=new TreeNode<T>(value);
            TreeNode<T> current = Root;
            while(current!=null)
            {
                if (current == node)
                    return true;
                else if (current > node)
                    current = current.Left;
                else
                    current=current.Right;
            }
            return false;
        }
        public bool ContainsRecursively(T value)
        {
            return ContainsRecursively(Root, value);
        }
        private bool ContainsRecursively(TreeNode<T> root, T value)
        {
            if(root==null)
                return false;
            TreeNode<T> node = new TreeNode<T>(value);
            if(root==node)
                return true;
            if (root > node)
                return ContainsRecursively(root.Left,value);
            else
                return ContainsRecursively(root.Right, value);
        }
        public uint GetTreeHeight()
        {
            if(Count==0)
                return 0;
            Queue<TreeNode<T>> queue= new Queue<TreeNode<T>>();
            queue.Enqueue(Root);
            uint height = 0;
            while(queue.Count>0)
            {
                int levelNodeCount=queue.Count;
                for(int i=0;i< levelNodeCount; i++)
                {
                    TreeNode<T> current=queue.Dequeue();
                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if(current.Right!=null)
                        queue.Enqueue(current.Right);
                }
                height++;
            }
            return height;
        }
        public uint GetTreeHeightRecursively(TreeNode<T> root)
        {
            if(root==null)
                return 0;
            uint heightLeft,heightRight;
            heightLeft = GetTreeHeightRecursively(root.Left);
            heightRight =  GetTreeHeightRecursively(root.Right);
            return 1+Math.Max(heightLeft,heightRight);
        }
    }
}
