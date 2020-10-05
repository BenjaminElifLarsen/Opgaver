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
                    "(id NVARCHAR(16) Not null, " +
                    "idValue INT Not null Identity(1,1), " +
                    "name NVARCHAR(40) Not null , " +
                    "amount INT Not null , " +
                    "type NVARCHAR(40) Not null, " +
                    "dangerCategory int null, " +
                    "flashPoint float null, " +
                    "minTemp float null," +
                    "boilingPoint float null, " +
                    "volatile bit null," +
                    "information NVARCHAR(2048) null, " +
                    "Primary Key(id, idValue) );";
            SQLControl.RunCommand(sqlString);
        }

        public static void CreateDefaultEntries()
        {
            StoredProcedures.InsertWareSP("'ID-55t'","'Water'","25","'Liquid'");
            StoredProcedures.InsertWareSP("'ID-123q'", "'Toaster'", "25", "'Electronic'");
            StoredProcedures.InsertWareSP("'MO.92z'", "'CiF3'", "1", "'Combustible Liquid'", "'Danger'", "4", null, null, null, null);
        }

        public static void InitialiseDatabase()
        {
            CreateDatabase();
            CreateTable();
        }
    }
}
