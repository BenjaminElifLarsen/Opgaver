using System;

namespace GPOpgaver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[] { 1, 3, 4, 5, 6, 8, 9, 11 };
            //Console.WriteLine(Opgaver.StepsInBinarySearch(test, 0, test.Length, 3)) ;
            Opgaver.PowerRanger(2, 49, 65);
            Console.WriteLine(Opgaver.IncrementString("foobar01002"));
            Console.WriteLine(Opgaver.StepsInBinarySearch(new int[] { 1, 2, 3, 4, 5 }, 0, 5, 3));
        }
    }
}
