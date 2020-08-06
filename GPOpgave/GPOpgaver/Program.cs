using System;

namespace GPOpgaver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[] { 1, 3, 4, 5, 6, 8, 9, 11 };
            Console.WriteLine(Opgaver.StepsInBinarySearch(test, 0, test.Length, 3)) ;
            Opgaver.PowerRanger(2, 49, 65);
            Console.WriteLine(Opgaver.IncrementString("e24z2d13foobar01002"));
        }
    }
}
