using System;
using System.Security.Cryptography;
using System.Text;

namespace SHA256Test
{
    class Program
    {
        static void Main(string[] args)
        {

            string firstString = Console.ReadLine();
            string secondString = Console.ReadLine();
            byte[] firstArray = Encoding.ASCII.GetBytes(firstString);
            byte[] secondArray = Encoding.ASCII.GetBytes(secondString);
            byte[] hashValueFirst;
            byte[] hashValueSecond;
            using (SHA256 test = SHA256.Create())
            {
                hashValueFirst = test.ComputeHash(firstArray);
                hashValueSecond = test.ComputeHash(secondArray);

            }
            Console.Clear();
            for (int n = 0; n < hashValueFirst.Length; n++)
            {
                Console.CursorTop = n;
                Console.CursorLeft = 0;
                Console.Write(hashValueFirst[n]);
            }

            for (int n = 0; n < hashValueSecond.Length; n++)
            {
                Console.CursorTop = n;
                Console.CursorLeft = 5;
                Console.Write(hashValueSecond[n]);
            }
            Console.WriteLine(Environment.NewLine + "First Lenght: " + hashValueFirst.Length);
            Console.WriteLine("Second Length: " + hashValueSecond.Length);
        }
    }
}
