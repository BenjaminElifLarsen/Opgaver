using System;
using TECHCOOL;

namespace Webshop
{
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            
            SQLet.ConnectSQLite("Webshop.db");
            Console.Write("Enter log message: ");
            InsertLogMessage(Console.ReadLine(), rnd.Next(1, 7));
            Console.Write("Enter log delete condition: Where ");
            DeleteLogMessage(Console.ReadLine());
            //string text = "Bob";
            //int value = rnd.Next(1,10);
            //SQLet.Execute($@"INSERT INTO Log (Message,Type,Date) 
            //              Values('{text}',{value},'{DateTime.Now}');");

            //Console.WriteLine("Hello World!");
            //Console.ReadKey();
            //string[] names = new string[] { "Benjamin", "Peter", "Jack", "Mads", "Simon" };

            //string besked = "Hey {0}. The clock is {1}.";
            //string sql = "Insert into Log ({0},{2},{4}) Values('{1}',{3},'{5}')";
            //string[] columns = new string[] { "Message", "Type", "Date" };

            //foreach (string name in names)
            //{
            //    //Console.WriteLine(string.Format(besked, name, DateTime.Now));
            //    //Console.WriteLine(sql, columns[0], name, columns[1], rnd.Next(1,7), columns[2], DateTime.Now);
            //    SQLet.Execute(String.Format(sql, columns[0], name, columns[1], rnd.Next(1, 7), columns[2], DateTime.Now));
            //}

            //string sqlDelete = "Delete from Log where Date Like '28-08-20%'";
            //SQLet.Execute(sqlDelete);

            //string sql = "Insert into Log ({0},{2},{4}) Values('{1}',{3},'{5}')";
            //string[] columns = new string[] { "Message", "Type", "Date" };

            //SQLet.Execute(String.Format(sql, columns[0], Console.ReadLine(), columns[1], rnd.Next(1, 7), columns[2], DateTime.Now));
            //Console.WriteLine(Console.ReadKey(true).Key);
        }

        static void DeleteLogMessage(string condition)
        {
            string sqlDelete = "Delete from Log where {0}";
            SQLet.Execute(String.Format(sqlDelete,condition));
        }

        static void InsertLogMessage(string besked, int type)
        {
            string sql = "Insert into Log ({0},{2},{4}) Values('{1}',{3},'{5}')";
            string[] columns = new string[] { "Message", "Type", "Date" };
            SQLet.Execute(String.Format(sql, columns[0], besked, columns[1], type, columns[2], DateTime.Now));
        }

    }
}
