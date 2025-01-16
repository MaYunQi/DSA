using DSA.Tree.Nodes;

namespace DSA.Tree.BinaryTree
{
    public class RedBlackTree<T> where T : IComparable<T>,IEquatable<T>
    {
        private RedBlackNode<T> Nil;
        public RedBlackNode<T>? Root { get;private set; }
        public uint Count { get;private set; }
        public RedBlackTree() 
        {
            Nil = new RedBlackNode<T>();
            Nil.Color=Color.Black;
        }
        public RedBlackTree(T value)
        {
            Root = new RedBlackNode<T>(value);
            Nil = new RedBlackNode<T>(default);
            Nil.Color= Color.Black;
            Root.Color = Color.Black;
            Root.Left = Nil;
            Root.Right = Nil;
            Count = 1;
        }
        public bool Insert(T value)
        {
            RedBlackNode<T> node=new RedBlackNode<T>(value);
            node.Color= Color.Red;
            node.Left=node.Right = Nil;
            RedBlackNode<T> current = Root;
            RedBlackNode<T> parent = null;
            while(current!=null&&!current.IsIdenticalTo(Nil))
            {
                parent = current;
                if (node == current)
                    return false;
                if (node > current)
                    current = current.Right;
                else if(node < current)
                    current = current.Left;
            }
            node.Parent = parent;
            if (parent == null)
                Root = node;
            else if(node>parent)
                parent.Right = node;
            else
                parent.Left = node;
            Count++;
            FixInsert(node);
            return true;
        }
        private void FixInsert(RedBlackNode<T> node)
        {
            //Case 1: Insert the Root node. Change the color to black and return.
            if(node.Parent==null)
            {
                node.Color = Color.Black;
                return;
            }
            // Color of parent node of inserted node is red, break the property that a red node can't have a red child.
            while(node.Parent.Color==Color.Red)
            {
                if (node.Parent == node.Parent.Parent.Left) // Left side
                {
                    RedBlackNode<T> uncle = node.Parent.Parent.Right;
                    //Case 2: The uncle node is red. So the inserted node, the parent node and the uncle node are all red.
                    //This case the tree is balanced. No need to rotate.
                    if (uncle.Color == Color.Red)
                    {
                        node.Parent.Color = Color.Black; // Change the parent node to black.
                        uncle.Color = Color.Black; // Change the uncle node to black.
                        node.Parent.Parent.Color=Color.Red; //Change the grandparent node to red.
                        node = node.Parent.Parent; //Point the node to grandparent node for further adjust.
                    }
                    //Case 3: The uncle node is black. So the inserted node, the parent node are red.
                    //This case, the tree is unbalanced, need to rotate.
                    else
                    {
                        if(node==node.Parent.Right)
                        {
                            // LR pattern, left rotate first, to LL pattern.
                            node = node.Parent;
                            LeftRotation(node);
                        }
                        //LL pattern
                        node.Parent.Color = Color.Black;
                        node.Parent.Parent.Color = Color.Red;
                        RightRotation(node.Parent.Parent);
                    }
                }
                else //Right side
                {
                    RedBlackNode<T> uncle = node.Parent.Parent.Left;
                    if (uncle.Color == Color.Red) // Uncle is red, so the tree is balance, just change the color.
                    {
                        node.Parent.Color = Color.Black;
                        uncle.Color= Color.Black;
                        node.Parent.Parent.Color=Color.Red;
                        node = node.Parent.Parent;
                    }
                    else // Uncle is black, so the tree is Not balance, need rotation and changing color.
                    {
                        if(node==node.Parent.Left) //RL Pattern
                        {
                            node= node.Parent;
                            RightRotation(node); // Rotation to RR pattern
                        }
                        node.Parent.Color = Color.Black;
                        node.Parent.Parent.Color =Color.Red;
                        LeftRotation(node.Parent.Parent);
                    }
                }
                if (node == Root)
                    break;
            }
            Root.Color=Color.Black;
        }
        public bool Remove(T value)
        {
            if(Root==null)
                return true;
            RedBlackNode<T> nodeToDelete = GetNode(value);
            if (nodeToDelete == null)
                return true;
            RemoveNode(nodeToDelete);
            Count--;
            return true;
        }
        private void RemoveNode(RedBlackNode<T> nodeToDelete)
        {
            RedBlackNode<T> y = nodeToDelete;
            Color yOriginalColor = y.Color;
            RedBlackNode<T> replacedNode;
            if(nodeToDelete.Left.IsIdenticalTo(Nil))
            {
                replacedNode = nodeToDelete.Right;
                Transplant(nodeToDelete, nodeToDelete.Right);
            }
            else if (nodeToDelete.Right.IsIdenticalTo(Nil))
            {
                replacedNode = nodeToDelete.Left;
                Transplant(nodeToDelete,nodeToDelete.Left);
            }
            else
            {
                y=GetMinNode(nodeToDelete.Right);
                yOriginalColor = y.Color;
                replacedNode = y.Right;
                if(y.Parent==nodeToDelete)
                    replacedNode.Parent = y;
                else
                {
                    Transplant(y, y.Right);
                    y.Right = nodeToDelete.Right;
                    y.Right.Parent = y;
                }
                Transplant(nodeToDelete, y);
                y.Left = nodeToDelete.Left;
                y.Left.Parent = y;
                y.Color = nodeToDelete.Color;
            }
            if (yOriginalColor == Color.Black)
            {
                FixDeletion(replacedNode);
            }
        }
        private void FixDeletion(RedBlackNode<T> replacedNode)
        {
            while(replacedNode!=Root&&replacedNode.Color==Color.Black)
            {
                if(replacedNode==replacedNode.Parent.Left)
                {
                    RedBlackNode<T> sibling=replacedNode.Parent.Right;
                    if(sibling==null||sibling.IsIdenticalTo(Nil))
                    {
                        replacedNode = replacedNode.Parent;
                        continue;
                    }
                    //Case 1: Sibling node is Red
                    if(sibling.Color==Color.Red)
                    {
                        sibling.Color = Color.Black;
                        replacedNode.Parent.Color=Color.Red;
                        LeftRotation(replacedNode.Parent);
                        sibling=replacedNode.Parent.Right;
                    }
                    //Case 2: Sibling has two black child node.
                    if(sibling.Left.Color==Color.Black&&sibling.Right.Color==Color.Black)
                    {
                        sibling.Color= Color.Red;
                        replacedNode=replacedNode.Parent;
                    }
                    else
                    {
                        //Case 3: Sibling right child is black, left child is red
                        if(sibling.Right.Color==Color.Black)
                        {
                            sibling.Left.Color = Color.Black;
                            sibling.Color=Color.Red;
                            RightRotation(sibling);
                            sibling=replacedNode.Parent.Right;
                        }
                        //Case 4: Sibling's right child is red.
                        sibling.Color = replacedNode.Parent.Color;
                        replacedNode.Parent.Color= Color.Black;
                        sibling.Right.Color= Color.Black;
                        LeftRotation(replacedNode.Parent);
                        replacedNode = Root;
                    }
                }
                else
                {
                    RedBlackNode<T> sibling = replacedNode.Parent.Left;
                    if(sibling==null||sibling.IsIdenticalTo(Nil))
                    {
                        replacedNode = replacedNode.Parent;
                        continue;
                    }
                    if (sibling.Color==Color.Red)
                    {
                        sibling.Color = Color.Black;
                        replacedNode.Parent.Color = Color.Red;
                        RightRotation(replacedNode.Parent);
                        sibling=replacedNode.Parent.Left;
                    }
                    if(sibling.Right.Color==Color.Black&&sibling.Left.Color==Color.Black)
                    {
                        sibling.Color = Color.Red;
                        replacedNode = replacedNode.Parent;
                    }
                    else
                    {
                        if(sibling.Left.Color==Color.Black)
                        {
                            sibling.Right.Color = Color.Black;
                            sibling.Color= Color.Red;
                            LeftRotation(sibling);
                            sibling=replacedNode.Parent.Left;
                        }
                        sibling.Color = replacedNode.Parent.Color;
                        replacedNode.Parent.Color=Color.Black;
                        sibling.Left.Color= Color.Black;
                        RightRotation(replacedNode.Parent);
                        replacedNode = Root;
                    }
                }
            }
            replacedNode.Color = Color.Black;
        }
        private void Transplant(RedBlackNode<T> u, RedBlackNode<T> v)
        {
            if (u.Parent == null)
                Root = v;
            else if(u==u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            v.Parent = u.Parent;
        }
        private void LeftRotation(RedBlackNode<T> node)
        {
            RedBlackNode<T> y = node.Right;
            node.Right=y.Left;
            if (!y.Left.IsIdenticalTo(Nil))
                y.Left.Parent = node;
            y.Parent = node.Parent;
            if (node.Parent == null)
                Root = y;
            else if (node == node.Parent.Left)
                node.Parent.Left = y;
            else
                node.Parent.Right = y;
            y.Left = node;
            node.Parent = y;
        }
        private void RightRotation(RedBlackNode<T> node)
        {
            RedBlackNode<T> y = node.Left;
            node.Left=y.Right;
            if(!y.Right.IsIdenticalTo(Nil))
                y.Right.Parent = node;
            y.Parent = node.Parent;
            if (node.Parent == null)
                Root = y;
            else if(node==node.Parent.Left)
                node.Parent.Left = y;
            else 
                node.Parent.Right = y;
            y.Right = node;
            node.Parent = y;
        }
        public void Clear()
        {
            Root = null;
            Count = 0;
        }
        public void ReverseTree()
        {
            ReverseTree(Root);
        }
        private void ReverseTree(RedBlackNode<T> root)
        {
            if (root == null)
                return;
            RedBlackNode<T> temp = root.Left;
            root.Left=root.Right;
            root.Right=temp;
            ReverseTree(root.Left);
            ReverseTree(root.Right);
        }
        public RedBlackNode<T> GetNode(T value)
        {
            if (Root == null)    
                return null;
            if (value == null)
                return null;
            RedBlackNode<T> node=new RedBlackNode<T>(value);
            RedBlackNode<T> current = Root;
            while(!current.IsIdenticalTo(Nil))
            {
                if(node==current)
                    return current;
                if(node>current)
                    current=current.Right;
                if(node<current)
                    current = current.Left;
            }
            return null;
        }
        public RedBlackNode<T> GetMinNode()
        {
            if (Root == null)
                return null;
            RedBlackNode<T> current = Root;
            while(!current.Left.IsIdenticalTo(Nil))
            {
                current = current.Left;
            }
            return current;
        }
        private RedBlackNode<T> GetMinNode(RedBlackNode<T> node)
        {
            if (node == null)
                return null;
            RedBlackNode<T> current = node;
            while(!current.Left.IsIdenticalTo(Nil))
            {
                current = current.Left;
            }
            return current;
        }
        public RedBlackNode<T> GetMaxNode()
        {
            if (Root == null)
                return null;
            RedBlackNode<T> current = Root;
            while (!current.Right.IsIdenticalTo(Nil))
            {
                current = current.Right;
            }
            return current;
        }
        public List<RedBlackNode<T>> GetAllNodes()
        {
            if (Root == null)
                return null;
            List<RedBlackNode<T>> list=new List<RedBlackNode<T>>();
            Stack<RedBlackNode<T>> stack = new Stack<RedBlackNode<T>>();
            RedBlackNode <T> current = Root;
            stack.Push(current);
            while (stack.Count > 0)
            {
                RedBlackNode<T> node = stack.Pop();
                list.Add(node);
                if (!node.Left.IsIdenticalTo(Nil))
                    stack.Push(node.Left);
                if(!node.Right.IsIdenticalTo(Nil))
                    stack.Push(node.Right);
            }
            return list;
        }
        public List<T> GetAllNodesValue()
        {
            if (Root == null)
                return null;
            List<T> list = new List<T>();
            Stack<RedBlackNode<T>> stack=new Stack<RedBlackNode<T>>();
            RedBlackNode<T> current= Root;
            stack.Push(current);
            while (stack.Count > 0) 
            {
                RedBlackNode<T> node= stack.Pop();
                list.Add(node.Value);
                if(!node.Left.IsIdenticalTo(Nil))
                    stack.Push(node.Left);
                if(!node.Right.IsIdenticalTo(Nil))
                    stack.Push(node.Right);
            }
            return list;
        }
        public List<RedBlackNode<T>> GetAllLeafNodes()
        {
            if (Root == null)
                return null;
            List<RedBlackNode<T>> list = new List<RedBlackNode<T>>();
            Stack<RedBlackNode<T>> stack = new Stack<RedBlackNode<T>>();
            RedBlackNode<T> current = Root;
            stack.Push(current);
            while (stack.Count > 0)
            {
                RedBlackNode<T> node = stack.Pop();
                if(node.Left.IsIdenticalTo(Nil) &&node.Right.IsIdenticalTo(Nil))
                    list.Add(node);
                if (!node.Left.IsIdenticalTo(Nil))
                    stack.Push(node.Left);
                if (!node.Right.IsIdenticalTo(Nil))
                    stack.Push(node.Right);
            }
            return list;
        }
        public List<T> GetAllLeafNodesValue()
        {
            if (Root == null)
                return null;
            List<T> list = new List<T>();
            Stack<RedBlackNode<T>> stack = new Stack<RedBlackNode<T>>();
            RedBlackNode<T> current = Root;
            stack.Push(current);
            while (stack.Count > 0)
            {
                RedBlackNode<T> node = stack.Pop();
                if (node.Left.IsIdenticalTo(Nil) && node.Right.IsIdenticalTo(Nil))
                    list.Add(node.Value);
                if (!node.Left.IsIdenticalTo(Nil))
                    stack.Push(node.Left);
                if (!node.Right.IsIdenticalTo(Nil))
                    stack.Push(node.Right);
            }
            return list;
        }
        public List<RedBlackNode<T>> GetLeftViewNodes()
        {
            if (Root == null)
                return null;
            List<RedBlackNode<T>> list= new List<RedBlackNode<T>>();
            Queue<RedBlackNode<T>> queue = new Queue<RedBlackNode<T>>();
            RedBlackNode<T> current = Root;
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                int levelNodeCount=queue.Count;
                for(int i = 0; i < levelNodeCount; i++)
                {
                    RedBlackNode <T> node = queue.Dequeue();
                    if (i==0)
                        list.Add(node);
                    if(!node.Left.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Left);
                    if(!node.Right.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Right);
                }
            }
            return list;
        }
        public List<T> GetLeftViewNodesValue()
        {
            if (Root == null)
                return null;
            List<T> list = new List<T>();
            Queue<RedBlackNode<T>> queue = new Queue<RedBlackNode<T>>();
            RedBlackNode<T> current = Root;
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                int levelNodeCount = queue.Count;
                for (int i = 0; i < levelNodeCount; i++)
                {
                    RedBlackNode<T> node = queue.Dequeue();
                    if (i == 0)
                        list.Add(node.Value);
                    if (!node.Left.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Left);
                    if (!node.Right.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Right);
                }
            }
            return list;
        }
        public List<RedBlackNode<T>> GetRightViewNodes()
        {
            if (Root == null)
                return null;
            List<RedBlackNode<T>> list = new List<RedBlackNode<T>>();
            Queue<RedBlackNode<T>> queue = new Queue<RedBlackNode<T>>();
            RedBlackNode<T> current = Root;
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                int levelNodeCount = queue.Count;
                for (int i = 0; i < levelNodeCount; i++)
                {
                    RedBlackNode<T> node = queue.Dequeue();
                    if (i == levelNodeCount-1)
                        list.Add(node);
                    if (!node.Left.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Left);
                    if (!node.Right.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Right);
                }
            }
            return list;
        }
        public List<T> GetRightViewNodesValue()
        {
            if (Root == null)
                return null;
            List<T> list = new List<T>();
            Queue<RedBlackNode<T>> queue = new Queue<RedBlackNode<T>>();
            RedBlackNode<T> current = Root;
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                int levelNodeCount = queue.Count;
                for (int i = 0; i < levelNodeCount; i++)
                {
                    RedBlackNode<T> node = queue.Dequeue();
                    if (i == levelNodeCount - 1)
                        list.Add(node.Value);
                    if (!node.Left.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Left);
                    if (!node.Right.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Right);
                }
            }
            return list;
        }
        public bool Contains(T value)
        {
            if(Root==null)
                return false;
            RedBlackNode<T> node=new RedBlackNode<T>(value);
            RedBlackNode<T> current = Root;
            while(!current.IsIdenticalTo(Nil))
            {
                if(current==node)
                    return true;
                if (node > current)
                    current = current.Right;
                if(node<current)
                    current = node.Left;
            }
            return false;
        }
        public bool IsLeaf(RedBlackNode<T> node)
        {
            if(Contains(node.Value))
            {
                if (node.Left.IsIdenticalTo(Nil) && node.Right.IsIdenticalTo(Nil))
                    return true;
            }
            return false;
        }
        public bool IsLeafValue(T value)
        {
            RedBlackNode<T> node = new RedBlackNode<T>(value);
            return IsLeaf(node);
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }
        public uint GetTreeHeight()
        {
            if(Root==null)
                return 0;
            Queue<RedBlackNode<T>> queue = new Queue<RedBlackNode<T>>();
            RedBlackNode<T> current = Root;
            queue.Enqueue(current);
            uint height = 0;
            while(queue.Count>0)
            {
                int leveNodeCount=queue.Count;
                for(int i=0;i<leveNodeCount;i++)
                {
                    RedBlackNode<T> node = queue.Dequeue();
                    if (!node.Left.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Left);
                    if(!node.Right.IsIdenticalTo(Nil))
                        queue.Enqueue(node.Right);
                }
                height++;
            }
            return height;
        }
        public int GetNodeDepth(RedBlackNode<T> node)
        {
            RedBlackNode<T> current = Root;
            int depth = 0;
            while(!current.IsIdenticalTo(Nil))
            {
                if(current==node)
                    return depth;
                if(node>current)
                    current = current.Right;
                else
                    current=current.Left;
                depth++;
            }
            return -1;
        }
        public int GetNodeHeight(RedBlackNode<T> node)
        {
            if (node == null)
                return -1;
            if (node.IsIdenticalTo(Nil))
                return -1;
            int left = GetNodeHeight(node.Left);
            int right = GetNodeHeight(node.Right);
            return Math.Max(left, right)+1;
        }
        public List<RedBlackNode<T>> InOrderTraversal()
        {
            List<RedBlackNode<T>> list= new List<RedBlackNode<T>>();
            InOrderTraversal(Root, list);
            return list;
        }
        private void InOrderTraversal(RedBlackNode<T> root, List<RedBlackNode<T>> list)
        {
            if (root == null||root.IsIdenticalTo(Nil))
                return;
            InOrderTraversal(root.Left, list);
            list.Add(root);
            InOrderTraversal(root.Right, list);
        }
        public void PrintTheTree()
        {
            if(Root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            RedBlackNode<T> node = Root;
            Queue<RedBlackNode<T>> queue=new Queue<Nodes.RedBlackNode<T>> ();
            queue.Enqueue(node);
            while (queue.Count > 0) 
            {
                int leveNodeCount=queue.Count;
                for(int i=0;i<leveNodeCount;i++)
                {
                    RedBlackNode<T> current = queue.Dequeue();
                    Console.Write(current.Value + " : " + current.Color+"\t");
                    if (!current.Left.IsIdenticalTo(Nil))
                        queue.Enqueue(current.Left);
                    if (!current.Right.IsIdenticalTo(Nil))
                        queue.Enqueue(current.Right);
                }
                Console.WriteLine();
            }
        }
    }
}
