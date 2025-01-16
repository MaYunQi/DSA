using DSA.Tree;
using DSA.Tree.BinaryTree;
using DSA.Tree.Interfaces;

namespace DSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.PrintTheTree();
            for(int i=0;i<10;i++)
            {
                Random random = new Random();
                int a=random.Next(0,100);
                tree.Insert(i);    
            }
        }
    }
}
