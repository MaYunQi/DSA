using DSA.Tree.Nodes;

namespace DSA.Tree.Interfaces
{
    public interface IBinaryTree<T> where T : IComparable<T>, IEquatable<T>
    {
        bool Insert(T value);
        bool Remove(T value);
        void Clear();
        void ReverseTree();
        TreeNode<T> GetNode(T value);
        TreeNode<T> GetMinNode();
        TreeNode<T> GetMaxNode();
        List<TreeNode<T>> GetAllNodes();
        List<T> GetAllNodesValue();
        List<TreeNode<T>> GetAllLeafNodes();
        List<T> GetAllLeafNodesValue();
        List<TreeNode<T>> GetLeftViewNodes();
        List<T> GetLeftViewNodesValue();
        List<TreeNode<T>> GetRightViewNodes();
        List<T> GetRightViewNodesValue();
        bool Contains(T value);
        bool IsLeaf(TreeNode<T> node);
        bool IsLeafValue(T value);
        bool IsEmpty();
        uint GetTreeHeight();
        int GetNodeDepth(TreeNode<T> node);
        int GetNodeHeight(TreeNode<T> node);
        void InOrderTraversal();
    }
}
