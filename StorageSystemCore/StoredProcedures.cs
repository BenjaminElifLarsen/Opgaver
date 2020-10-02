using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SQLCode
{
    /// <summary>
    /// Contains, creates annd runs stored procedures for the database
    /// </summary>
    class StoredProcedures
    { //https://www.w3schools.com/sql/sql_stored_procedures.asp
        /// <summary>
        /// Creates all stored procedures in the database that the program will need.
        /// </summary>
        public static void CreateAllStoredProcedures()
        {
            MethodInfo[] methods = Type.GetType(typeof(StoredProcedures).ToString()).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            foreach(MethodInfo method in methods)
            {
                string sql = (string)method.Invoke(null, null);
                try
                {
                    SQLControl.RunCommand(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed at running method {method.Name}: {e.Message}");
                    StorageSystemCore.Support.WaitOnKeyInput();
                }
            }
        }

        private static string CreateFullView()
        {
            return
                "CREATE PROCEDURE SelectAllData " +
                    "AS " +
                    $"USE {SQLControl.DataBase}; " +
                    "SELECT * FROM Inventory;";
        }

        private static string CreatePartlyView()
        {
            return
                "CREATE PROCEDURE SelectPartlyData @Columns nvarchar(512) " +
                    "AS " +
                    $"USE {SQLControl.DataBase}; " +
                    "SELECT @Columns FROM Inventory;";
        }

        private static string CreateInsertWares() //the variables should be in string and not list/array
        {
            return //not fully sure if this approach will work out regarding having the vlaues as nvarchar
                "CREATE PROCEDURE InsertWare @Columns nvarchar(512), @Values nvarchar(2048) " +
                    "AS " +
                    $"USE {SQLControl.DataBase}; " + 
                    "INSERT INTO Inventory (@Columns) " +
                    "Values (@Values);";
        }

        private static string CreateDeleteWare()
        {
            return
                "CREATE PROCEDURE DeleteWare @WareToDelete nvarchar(16) " +
                    "AS " +
                    $"USE {SQLControl.DataBase}; " +
                    "DELETE FROM Inventory WHERE id = @WareToDelete;";
        }

        private static string CreateFindWareType()
        {
            return
                "CREATE PROCEDURE FindWareType @WareID nvarchar(16) " +
                    "As " +
                    $"USE {SQLControl.DataBase}; " +
                    "SELECT type FROM Inventory WHERE id = @WareID;";
        }

        private static string CreateSelectOneWareValues()
        {
            return
                "CREATE PROCEDURE SelectValuesFromOneWare @WareID nvarchar(16) " +
                    "AS " +
                    $"USE {SQLControl.DataBase}; " +
                    "SELECT * FROM Inventory WHERE id = @WareID;";
        }

        private static string CreateSelectPartValuesOfOneWare()
        {
            return
                "CREATE PROCEDURE SelectPartValuesFromOneWare @WareID nvarchar(16) @Columns nvarchar(512) " +
                    "AS " +
                    $"USE {SQLControl.DataBase}; " +
                    "SELECT @Columns FROM Inventory WHERE id = @WareID;";
        }

        private static string CreateUpdateWare()
        {
            return
                "CREATE PROCEDURE UpdateWare @WareID nvarchar(16) @Column nvarchar(512) @NewValue nvarchar(2048) " +
                    "AS " +
                    $"USE {SQLControl.DataBase}; " +
                    "UPDATE Inventory " +
                    "SET @Column = @NewValue " +
                    "WHERE id = @WareID:";
        }

        /// <summary>
        /// Uses the update stored procedure to update the ware <paramref name="id"/>'s <paramref name="column"/> with the new value of <paramref name="newValue"/>.
        /// </summary>
        /// <param name="id">Ware ID to update</param>
        /// <param name="column">The Column to update</param>
        /// <param name="newValue">The new value</param>
        public static void UpdateWareSP(string id, string column, string newValue)
        {
            string sqlString =
                $"EXEC UpdateWare @WareID = {id}, @Column = {column}, @NewValue = {newValue}";
            try { 
                SQLControl.RunCommand(sqlString);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Could not update: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
            }
        }

        /// <summary>
        /// Uses the get type storage procedure to retrive the type of <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ware id to retrive the type of</param>
        /// <returns>Returns the type of <paramref name="id"/> in a string list.</returns>
        public static List<string> GetTypeSP(string id)
        {
            string sqlString =
                $"EXEC FindWareType @WareID = {id};";
            try
            {
                return SQLControl.GetValuesSingleWare(sqlString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not update: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
                return null;
            }
        }

        /// <summary>
        /// Uses the Select all data stored procedure to collect all ware information in the database.
        /// </summary>
        /// <returns>Returns all information of all wares in the database as a list of lists of strings.</returns>
        public static List<List<string>> GetAllInformation()
        {
            string sqlString =
                "EXEC SelectAllData;";
            try { 
                return SQLControl.GetValuesMultiWares(sqlString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not get information: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
                return null;
            }
        }

        /// <summary>
        /// Uses the Select partly data procedure to collect the data in <paramref name="columns"/> of all wares in the database.
        /// </summary>
        /// <param name="columns"></param>
        /// <returns>Returns information from <paramref name="columns"/> of all wares in the database as a list of lists of strings.</returns>
        public static List<List<string>> GetPartlyInformation(string columns)
        {
            string sqlString =
                $"EXEC SelectPartlyData @Columns = {columns};";
            try
            {
                return SQLControl.GetValuesMultiWares(sqlString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not get information: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
                return null;
            }
        }

        /// <summary>
        /// Uses the select values from one ware stored procedure to retrive all information of the ware with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id of the ware to collect information from</param>
        /// <returns>Returns a list of strings, each string containg a column of data.</returns>
        public static List<string> GetInformationFromOneWareSP(string id)
        {
            string sqlString =
                $"EXEC SelectValuesFromOneWare @WareID = {id};";
            try { 
                return SQLControl.GetValuesSingleWare(sqlString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not get information: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
                return null;
            }
        }

        /// <summary>
        /// Uses the select part values from one ware stored procedure to retive the information(s) of <paramref name="columns"/> of the ware with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID of the ware to collect information from. </param>
        /// <param name="columns">The column(s) to get information(s) from. </param>
        /// <returns>Returns a list of strings, each string containg a column of data.</returns>
        public static List<string> GetPartlyInformationFromOneWareSP(string id, string columns)
        {
            string sqlString =
                $"EXEC SelectPartValuesFromOneWare @WareID = {id}, @Columns = {columns};";
            try { 
                return SQLControl.GetValuesSingleWare(sqlString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not get information: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
                return null;
            }
        }

        /// <summary>
        /// Uses the delete ware stored procedure to delete the ware with the <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID of the ware to delete.</param>
        public static void RunDeleteWareSP(string id)
        {
            string sqslString =
                $"EXEC DeleteWare @WareToDelete = {id};";
            try
            {
                SQLControl.RunCommand(sqslString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not delete: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
            }
        }

        /// <summary>
        /// Uses the insert ware stored procedure to insert a new ware, into the database, with the <paramref name="columns"/> with the values from <paramref name="values"/> 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        public static void InsertWareSP(string columns, string values)
        {
            string sqlString =
                $"EXEC InsertWare @Columns = {columns}, @Values = {values}";
            try { 
            SQLControl.RunCommand(sqlString);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not insert: {e.Message}");
                StorageSystemCore.Support.WaitOnKeyInput();
            }
        }

    }
}
