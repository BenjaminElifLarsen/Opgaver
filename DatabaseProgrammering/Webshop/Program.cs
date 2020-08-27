using System;
using TECHCOOL;

namespace Webshop
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLet.ConnectSQLite("Webshop.db");
            SQLet.Execute(@"INSERT INTO Log (Message,Type,Date) 
                          Values('First!',1,'01-01-2001');");
            
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
