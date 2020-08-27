using System;
using TECHCOOL;

namespace Webshop
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLet.ConnectSQLite("Webshop.db");
            string text = "Bob";
            int value = 3;
            SQLet.Execute($@"INSERT INTO Log (Message,Type,Date) 
                          Values('{text}',{value},'{DateTime.Now}');");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
