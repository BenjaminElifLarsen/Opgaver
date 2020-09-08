using System;
using System.Collections.Generic;
using TECHCOOL;

namespace Webshop_New
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLet.ConnectSqlServer("WebShop_New", "BENJAMIN-ELIF-L\\MSSQLSERVER02");
            ShowBasket();
        }

        static void ShowBasket()
        {
            string[][] result = SQLet.GetArray("Select * From Basket");
            Console.WriteLine("ID \t Quantity \t ItemID \t CustomerID");
            foreach(string[] row in result)
            {
                foreach(string key in row)
                {
                    Console.Write(key + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
