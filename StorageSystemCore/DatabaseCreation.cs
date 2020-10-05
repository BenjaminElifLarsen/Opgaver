using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLCode
{
    static class DatabaseCreation
    {
        private static void CreateDatabase()
        {
            SQLControl.RunCommand($"Use Master; CREATE DATABASE {SQLControl.DataBase}");
        }

        private static void CreateTable()
        {
            string sqlString =
                $"Use {SQLControl.DataBase}; " +
                    "Create Table Inventory " +
                    "(id NVARCHAR(16) Not null Primary Key, " +
                    "name NVARCHAR(40) Not null , " +
                    "amount INT Not null , " +
                    "type NVARCHAR(40) Not null, " +
                    "dangerCategory int null, " +
                    "flashPoint float null, " +
                    "minTemp float null," +
                    "boilingPoint float null, " +
                    "volatile bit null," +
                    "information NVARCHAR(2048) null);";
            SQLControl.RunCommand(sqlString);
        }

        private static void CreateDefaultEntries()
        {
            string[] columns = new string[] { };
            string[] values = new string[] { };
            string columnsString = "id, name, amount, type";
            string valuesString = "'ID-55t','Water',25,'Liquid'";
            StoredProcedures.InsertWareSP(columnsString, valuesString);
            columnsString = "id, name, amount, type";
            valuesString = "'ID-123q','Toaster',25,'Electronic'";
            StoredProcedures.InsertWareSP(columnsString, valuesString);
        }

        public static void InitialiseDatabase()
        {
            CreateDatabase();
            CreateTable();
            CreateDefaultEntries();
        }
    }
}
