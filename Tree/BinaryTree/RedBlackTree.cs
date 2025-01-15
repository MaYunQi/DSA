using DSA.Tree.Nodes;

namespace DSA.Tree.BinaryTree
{
    public class RedBlackTree<T> where T : IComparable<T>,IEquatable<T>
    {
        private RedBlackNode<T> Nil;
        public RedBlackNode<T> Root { get;private set; }
        public uint Count { get;private set; }
        public RedBlackTree() 
        {
            Nil = new RedBlackNode<T>(default);
            Nil.Color=Color.Black;
            Root = Nil;
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
            node.Left=node.Right = Nil;

            RedBlackNode<T> current = Root;
            RedBlackNode<T> parent = null;
            while(current!=Nil)
            {
                parent = current;
                if (node == current)
                    return false;
                if (node > current)
                    current = current.Right;
                else
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
            node.Color = Color.Red;
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
            }
            Root.Color=Color.Black;
        }
        public bool Remove(T value)
        {
            if(Root==null||Root==Nil)
                return true;
            RedBlackNode<T> node = GetNode(value);
            if (node == null)
                return true;
            //Leaf node
            if(node.Left==Nil&&node.Right==Nil)
            {
                //Case 1: Red leaf node, just delete.
                if(node.Color==Color.Red)
                {
                    RedBlackNode<T> parent=DeleteLeafNode(node);
                }
                //Case 2: Black leaf node, need to adjust the tree. Double Black
                else 
                {
                    RedBlackNode<T> parent = DeleteLeafNode(node);
                    //Fix Deletion
                }
            }
            // Node has two childs.
            else if(node.Left != Nil && node.Right != Nil)
            {
                RedBlackNode<T> minNode = GetMinNode(node.Right);
                Color originalColor = minNode.Color;
                RedBlackNode<T> minRightChild=minNode.Right;
                if (node.Parent == null) //If node is Root node
                {
                    Root = minNode;//Set the minNode to be the Root ndoe.
                    minNode.Left = node.Left;
                    if(node.Left!=Nil)
                        node.Left.Parent = minNode;
                    minNode.Parent = null;
                }
                else if (minNode.Parent == node) //If the minNode is the right child of node.
                {
                    minNode.Parent = node.Parent;
                    if (node == node.Parent.Left)
                        node.Parent.Left = minNode;
                    else
                        node.Parent.Right= minNode; //Attach the minNode to the parent node of The node.
                }
                else
                {
                    minNode.Parent = node.Parent;
                    if (node == node.Parent.Left)
                        node.Parent.Left = minNode;
                    else
                        node.Parent.Right = minNode;
                    minNode.Right = node.Right;
                    node.Right.Parent = minNode;
                }
                if (node.Right != Nil)
                {
                    node.Right.Left = minRightChild;
                    if (minRightChild != Nil)
                        minRightChild.Parent = node.Right;
                }
                node= null;
                if(minNode.Color==Color.Black)
                {
                    //Fix Deletion.
                }    
            }
            else // Node only has one child
            {
                // Replace the red node with the child node.
                if(node.Color==Color.Red)
                {
                    RedBlackNode<T> newNode=ReplaceWithOnlyChild(node);
                }
                else //Node is black with one child  Double Black
                {
                    RedBlackNode<T> newNode = ReplaceWithOnlyChild(node);
                    Color childColor=newNode.Color;
                    if(childColor==Color.Red)
                        newNode.Color = Color.Black;
                    else
                    {
                        //Fix Deletion
                    }
                }
            }
            Count--;
            return true;
        }
        private RedBlackNode<T> DeleteLeafNode(RedBlackNode<T> node)
        {
            RedBlackNode <T> parent=node.Parent;
            if (node == parent.Left)
                parent.Left = Nil;
            else
                parent.Right = Nil;
            node=null;
            return parent;
        }
        private RedBlackNode<T> ReplaceWithOnlyChild(RedBlackNode<T> node)
        {
            RedBlackNode<T> child = node.Left == Nil ? node.Right : node.Left;
            child.Parent = node.Parent;
            if (node == node.Parent.Left)
                node.Parent.Left = child;
            else
                node.Parent.Right = child;
            node = null;
            return child;
        }
        private void FixDeletion()
        {

        }
        private void LeftRotation(RedBlackNode<T> node)
        {
            RedBlackNode<T> y = node.Right;
            node.Right=y.Left;
            if (y.Left != Nil)
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
            if(y.Right!=Nil)
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
            if (Root == null || Root == Nil)
                return null;
            if (value == null)
                return null;
            RedBlackNode<T> node=new RedBlackNode<T>(value);
            RedBlackNode<T> current = Root;
            while(current!=Nil)
            {
                if(node==current)
                    return node;
                if(node>current)
                    current=current.Right;
                if(node<current)
                    current = current.Left;
            }
            return null;
        }
        public RedBlackNode<T> GetMinNode()
        {
            if (Root == null||Root==Nil)
                return null;
            RedBlackNode<T> current = Root;
            while(current.Left!=Nil)
            {
                current = current.Left;
            }
            return current;
        }
        private RedBlackNode<T> GetMinNode(RedBlackNode<T> node)
        {
            if (node == null || node == Nil)
                return null;
            RedBlackNode<T> current = node;
            while(current.Left!=Nil)
            {
                current = current.Left;
            }
            return current;
        }
        public RedBlackNode<T> GetMaxNode()
        {
            if (Root == null || Root == Nil)
                return null;
            RedBlackNode<T> current = Root;
            while (current.Right != Nil)
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
                if (node.Left!=Nil)
                    stack.Push(node.Left);
                if(node.Right!=Nil)
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
                if(node.Left!=Nil)
                    stack.Push(node.Left);
                if(node.Right!=Nil)
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
                if(node.Left==Nil&&node.Right== Nil)
                    list.Add(node);
                if (node.Left != Nil)
                    stack.Push(node.Left);
                if (node.Right != Nil)
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
                if (node.Left == Nil && node.Right == Nil)
                    list.Add(node.Value);
                if (node.Left != Nil)
                    stack.Push(node.Left);
                if (node.Right != Nil)
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
                    if(node.Left!= Nil)
                        queue.Enqueue(node.Left);
                    if(node.Right!= Nil)
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
                    if (node.Left != Nil)
                        queue.Enqueue(node.Left);
                    if (node.Right != Nil)
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
                    if (node.Left != Nil)
                        queue.Enqueue(node.Left);
                    if (node.Right != Nil)
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
                    if (node.Left != Nil)
                        queue.Enqueue(node.Left);
                    if (node.Right != Nil)
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
            while(current!= Nil)
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
                if (node.Left == Nil && node.Right == Nil)
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
                    if (node.Left!= Nil)
                        queue.Enqueue(node.Left);
                    if(node.Right!= Nil)
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
            while(current!= Nil)
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
            if (node == Nil)
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
            if (root == null||root==Nil)
                return;
            InOrderTraversal(root.Left, list);
            list.Add(root);
            InOrderTraversal(root.Right, list);
        }
    }
}
