using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SQLCode
{
    /// <summary>
    /// Contains and creates stored procedures for the database
    /// </summary>
    class StoredProcedures
    {
        public static void CreateAllStoredProcedures()
        {
            MethodInfo[] methods = Type.GetType(typeof(StoredProcedures).ToString()).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            foreach(MethodInfo method in methods)
            {
                string sql = (string)method.Invoke(null, null);
                //SQLControl.RunCommand(sql);
            }
        }


        private static string CreateFullView() //needs to call the sqlControl and runs these as some point
        {
            return
                "CREATE PROCEDURE SelectAllData " +
                    "AS " +
                    $"Use {SQLControl.DataBase}; " +
                    "SELECT * FROM Inventory;";
        }

        private static string CreatePartlyView()
        {
            return
                "CREATE PROCEDURE SelectAllData @Columns nvarchar(512) " +
                    "AS " +
                    $"Use {SQLControl.DataBase}; " +
                    "SELECT @Columns FROM Inventory;";
        }

        private static string CreateInsertParts() //the variables should be in string and not list/array
        {
            return //not fully sure if this approach will work out regarding having the vlaues as nvarchar
                "CREATE PROCEDURE SelectAllData @Columns nvarchar(512), @Values nvarchar(4096) " +
                    "AS " +
                    $"Use {SQLControl.DataBase}; " + 
                    "INSERT INTO Inventory (@Columns)" +
                    "Values (@Values);";
        }

        private static string CreateDeleteWare()
        {
            return
                "CREATE PROCEDURE DeleteWare @WareToDelete nvarchar(16) " +
                    "AS " +
                    $"Use {SQLControl.DataBase}; " +
                    "DELETE FROM Inventory WHERE id = @WareToDelete;";
        }

        private static string CreateFindWareType()
        {
            return
                "CREATE PROCEDURE FindWareType @WareID nvarchar(16) " +
                    "As " +
                    $"Use {SQLControl.DataBase}; " +
                    "SELECT type WHERE id = @WareID";
        }

        private static string CreateSelectOneWareValues()
        {
            return
                "CREATE PROCEDURE SelectValuesFromOneWare  @WareID nvarchar(16)" +
                    $"Use {SQLControl.DataBase}; " +
                    "SELECT * Where id = @WareID";
        }

        private static string CreateSelectPartValuesOfOneWare()
        {
            return
                "";
        }




        public static void RunDeleteWareSP(string id)
        {
            string sqslString =
                $"EXEC DeleteWare @WareToDelete = {id};";
            SQLControl.RunCommand(sqslString);
        }

    }
}
