using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericBinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var druvo = new BinaryTree<int>(50);

            var numbers = new int[] { 25, 75, 12, 37, 63, 87};

            for (int i = 0; i < numbers.Length; i++)
            {
                druvo.Insert(numbers[i]);
            }

            druvo.DisplayTree(1);
        }
        static Random rnd =  new Random();
        static int GetRandomNumberBalancedAround(int pivot, int seed)
        {
            return pivot + rnd.Next() * (seed % 2 == 0 ? -1 : 1);
        }

        static int ProjectNumberIntoRange(int number, int maxValue)
        {
            return number % maxValue;
        }
    }
}
