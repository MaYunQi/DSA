using DSA.Tree.Interfaces;

namespace DSA.Tree.BinaryTree
{
    public class AVLTree<T> : BinarySearchTree<T>, IBinaryTree<T> where T : IComparable<T>, IEquatable<T>
    {
        public override bool Add(T value)
        {
            bool result = base.Add(value);
            if (result)
                BalanceTree();
            return result;
        }
        public override bool AddRecursively(T value)
        {
            bool result = base.AddRecursively(value);
            if (result)
                BalanceTree();
            return result;
        }
        public override bool Remove(T value)
        {
            bool result = base.Remove(value);
            if (result)
                BalanceTree();
            return result;
        }
        public override bool RemoveRecursively(T value)
        {
            bool result = base.RemoveRecursively(value);
            if (result)
                BalanceTree();
            return result;
        }
        private Stack<TreeNode<T>> GetUnbalancedNode()
        {
            Stack<TreeNode<T>> unbanlancedNodes = new Stack<TreeNode<T>>(); 
            if (Root == null)
                return unbanlancedNodes;
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            TreeNode<T> current = Root;
            stack.Push(current);
            while (stack.Count > 0)
            {
                current = stack.Pop();
                int balanceFactor = Math.Abs(GetNodeHeight(current.Left) - GetNodeHeight(current.Right));
                if (balanceFactor > 1)
                    unbanlancedNodes.Push(current);
                if (current.Left != null)
                    stack.Push(current.Left);
                if (current.Right != null)
                    stack.Push(current.Right);
            }
            return unbanlancedNodes;
        }
        private int GetBalanceFactor(TreeNode<T> node)
        {
            if (node == null)
                return 0;
            return GetNodeHeight(node.Left) - GetNodeHeight(node.Right);
        }
        private void BalanceTree()
        {
            Stack<TreeNode<T>> unbalancedNodes = GetUnbalancedNode();
            if (unbalancedNodes.Count==0)
                return;
            while (unbalancedNodes.Count > 0)
            {
                TreeNode <T> unbalancedNode= unbalancedNodes.Pop();
                TreeNode <T> parentNode=GetParentNode(unbalancedNode);

                int balanceFactor = GetBalanceFactor(unbalancedNode);
                TreeNode<T> newSubtreeRoot = unbalancedNode;

                if (balanceFactor > 1 && GetBalanceFactor(unbalancedNode.Left) >= 0)
                {
                    newSubtreeRoot = RightRotation(unbalancedNode);
                }
                else if (balanceFactor > 1 && GetBalanceFactor(unbalancedNode.Left) < 0)
                {
                    unbalancedNode.Left = LeftRotation(unbalancedNode.Left);
                    newSubtreeRoot = RightRotation(unbalancedNode);
                }
                else if (balanceFactor < -1 && GetBalanceFactor(unbalancedNode.Right) <= 0)
                {
                    newSubtreeRoot = LeftRotation(unbalancedNode);
                }
                else if (balanceFactor < -1 && GetBalanceFactor(unbalancedNode.Right) > 0)
                {
                    unbalancedNode.Right = RightRotation(unbalancedNode.Right);
                    newSubtreeRoot = LeftRotation(unbalancedNode);
                }
                if(parentNode!=null)
                {
                    if (parentNode.Left == unbalancedNode)
                        parentNode.Left = newSubtreeRoot;
                    else
                        parentNode.Right = newSubtreeRoot;
                }
                else
                    Root = newSubtreeRoot;
            }
        }
        private TreeNode<T> GetParentNode(TreeNode<T> node)
        {
            if (node == null || Root == node||Root==null)
                return null;
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            stack.Push(Root);
            while (stack.Count > 0) 
            {
                TreeNode<T> current= stack.Pop();
                if(current.Left==node||current.Right==node)
                    return current;
                if(current.Left!=null)
                    stack.Push(current.Left);
                if(current.Right!=null)
                    stack.Push(current.Right);
            }
            return null;
        }
        private TreeNode<T> LeftRotation(TreeNode<T> node)
        {
            TreeNode<T> x = node.Right;
            TreeNode<T> y = x.Left;

            x.Left = node;
            node.Right = y;
            return x;
        }
        private TreeNode<T> RightRotation(TreeNode<T> node)
        {
            TreeNode<T> x = node.Left;
            TreeNode<T> y = x.Right;

            x.Right = node;
            node.Left = y;
            return x;
        }
        public void InOrderTraversal()
        {
            InOrderTraversal(Root);
        }
        private void InOrderTraversal(TreeNode<T> node)
        {
            if (node == null)
                return;
            InOrderTraversal(node.Left);
            Console.WriteLine(node.Value);
            InOrderTraversal(node.Right);
        }
    }
}
