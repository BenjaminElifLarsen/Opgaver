﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLCode
{
    /// <summary>
    /// Database creation class.
    /// </summary>
    static class DatabaseCreation
    {
        /// <summary>
        /// Creates the database
        /// </summary>
        private static void CreateDatabase()
        {
            SQLControl.RunCommand($"Use Master; CREATE DATABASE {SQLControl.DataBase}");
        }

        /// <summary>
        /// Creates the table and columns.
        /// </summary>
        private static void CreateTableAndColumns()
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

        /// <summary>
        /// Creates default wares. 
        /// </summary>
        public static void CreateDefaultEntries()
        {
            StoredProcedures.InsertWareSP("'ID-55t'","'Water'",25,"'Liquid'");
            StoredProcedures.InsertWareSP("'ID-123q'", "'Toaster'", 25, "'Electronic'");
            StoredProcedures.InsertWareSP("'MO.92z'", "'CiF3'", 1, "'Combustible Liquid'", "'Danger'", "4", null, null, null, null);
        }

        /// <summary>
        /// Creates the databae, its tables and their columns.
        /// </summary>
        public static void InitialiseDatabase()
        {
            CreateDatabase();
            CreateTableAndColumns();
        }
    }
}
