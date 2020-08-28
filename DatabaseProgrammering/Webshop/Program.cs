using System;
using TECHCOOL;

namespace Webshop
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            SQLet.ConnectSQLite("Webshop.db");
            //string text = "Bob";
            //int value = rnd.Next(1,10);
            //SQLet.Execute($@"INSERT INTO Log (Message,Type,Date) 
            //              Values('{text}',{value},'{DateTime.Now}');");

            //Console.WriteLine("Hello World!");
            //Console.ReadKey();
            string[] names = new string[] { "Benjamin", "Peter", "Jack", "Mads", "Simon" };

            //string besked = "Hey {0}. The clock is {1}.";
            string sql = "Insert into Log ({0},{2},{4}) Values('{1}',{3},'{5}')";
            string[] columns = new string[] { "Message", "Type", "Date" };

            foreach (string name in names)
            {
                //Console.WriteLine(string.Format(besked, name, DateTime.Now));
                //Console.WriteLine(sql, columns[0], name, columns[1], rnd.Next(1,7), columns[2], DateTime.Now);
                SQLet.Execute(String.Format(sql, columns[0], name, columns[1], rnd.Next(1, 7), columns[2], DateTime.Now));
            }

            string sqlDelete = "Delete from Log where Date Like '28-08-20%'";
            SQLet.Execute(sqlDelete);
        
        }
    }
}
