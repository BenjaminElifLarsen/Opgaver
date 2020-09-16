using System;
using System.Data.SqlClient;

namespace SQLTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string sqlTest = SQLControl.CreateConnectionString();
            Console.WriteLine(sqlTest);
            SQLControl.DataBase = "StorageDB";
            SqlConnection sqlConnection = SQLControl.CreateConnection(sqlTest);
            SQLControl.SelectWare($"Use {SQLControl.DataBase}; Select * From Inventory");
            SQLControl.AddWare(new string[] {"ID","name","amount","Type"}, new string[] { "'ID-234q2378'", "'Test'", "10", "'TestType'" });
            SQLControl.RemoveWare("ID = 'ID-234q237'");
        }
    }
}
