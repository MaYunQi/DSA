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
            int[] array = new int[10];
            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();
                array[i] = random.Next(1,100);
                //array[i] = i;
            }
            foreach(int i in array)
            {
                Console.Write(i+" ");
            }
            Console.WriteLine();
            SortHelper<int>.MergeSort(array);
            foreach (int i in array)
            {
                Console.Write(i + " ");
            }
        }
    }
}
