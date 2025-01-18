using DSA.Algorithms.Sort;
using DSA.Tree;
using DSA.Tree.BinaryTree;
using DSA.Tree.Interfaces;
using DSA.Tree.Nodes;

namespace DSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            MinHeap<int> heap = new MinHeap<int>();
            Random random = new Random();
            for (int i = 0; i < 31; i++) 
            {
                int number=random.Next(0,100);
                heap.Insert(number);
            }
            heap.PrintTheTree();
            Console.WriteLine();
            int remove=Convert.ToInt32(Console.ReadLine());
            heap.Remove(remove);
            heap.PrintTheTree();
        }
    }
}
