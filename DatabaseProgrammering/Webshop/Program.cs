using System;
using System.Collections.Generic;
using System.Dynamic;
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
            Console.Write("Enter log entry delete condition: Where ");
            DeleteLogMessage(Console.ReadLine());
            Console.Write("Enter log column to update: ");
            string column = Console.ReadLine();
            Console.Write("Enter new value: ");
            string value = Console.ReadLine();
            Console.Write("Enter log update condition: Where");
            string condition = Console.ReadLine();
            UpdateLogMessage(column, value, condition);
            List<string> selectingColumns = new List<string>();
            ConsoleKey key;
            do
            {
                Console.WriteLine("Press {0} to exit entering columns", ConsoleKey.D1);
                key = Console.ReadKey(true).Key;
                if(key != ConsoleKey.D1)
                {
                    Console.Write("Enter column: ");
                    string text = Console.ReadLine();
                    if(text != "")
                        selectingColumns.Add(text);
                }
                Console.Write(Environment.NewLine);
            } while (key != ConsoleKey.D1);
            SelectLogMessage(selectingColumns.ToArray());
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

        static void SelectLogMessage(params string[] columns)
        {
            if(columns.Length != 0)
            {
                string selectedColumns = "";
                for (int i = 0; i < columns.Length; i++)
                {
                    selectedColumns += columns[i];
                    if (i != columns.Length - 1)
                        selectedColumns += ",";
                }

                string sqlSelect = "Select {0} From Log";
                //SQLet.Execute(String.Format(sqlSelect, selectedColumns));
                string[][] test = SQLet.GetArray(String.Format(sqlSelect, selectedColumns));
                DisplaySelect(test);
            }
        }

        static void SelectLogMessage2(string column)
        {
            string sqlSelect = "Select {0} From Log";
            Result result = SQLet.GetResult(String.Format(sqlSelect, column));
            Console.WriteLine(result[0][column]);
        }

        static void DisplaySelect(string[][] text)
        {
            int pos = 1;
            foreach(string[] stringArray in text)
            {
                Console.WriteLine("Row {0}", pos++);
                foreach(string str in stringArray)
                {
                    Console.Write(str + " | ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        static void UpdateLogMessage/*<T>*/(string column, string value, string condition)
        {
            if (column != "LOG_ID")
            {
                string sqlUpdate = "Update Log Set {0} = '{1}' where {2}";
                SQLet.Execute(String.Format(sqlUpdate, column, value, condition));
            }
               
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
