using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLCode
{
    class SQLControl
    {
        /// <summary>
        /// Name of the database.
        /// </summary>
        private static string database;
        /// <summary>
        /// The SQLConnection to a database.
        /// </summary>
        private static SqlConnection sqlConnection;
        /// <summary>
        /// The other entries are the tables and the inner entries are the columns of each outher entry.
        /// </summary>
        private static string[][] tablesAndColumns;

        /// <summary>
        /// Gets and sets the SQL Connection to a specific database.
        /// </summary>
        /// <value></value>
        public static SqlConnection SQLConnection { get => sqlConnection; set => sqlConnection = value; }

        /// <summary>
        /// Gets and sets the name of the database.
        /// </summary>
        /// <value></value>
        public static string DataBase { get => database; set => database = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static string[][] TablesAndColumns { get => tablesAndColumns; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static bool DatabaseInUse { get; set; }

        /// <summary>
        /// Sanitises <paramref name="queryToSanitise"/> and returns it. 
        /// </summary>
        /// <param name="queryToSanitise"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string SanitiseSingleQuotes(string queryToSanitise)
        {
            if (queryToSanitise == null)
                throw new NullReferenceException();
            List<char> sanitised = new List<char>();
            foreach (char chr in queryToSanitise)
            {
                sanitised.Add(chr);
                if (chr == '\'')
                {
                    sanitised.Add('\'');
                }
            }

            return new string(sanitised.ToArray());
        }

        /// <summary>
        /// Creates a trusted connection string and returns it.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string CreateConnectionString(string server, string database)
        {
            if (server == null || database == null)
                throw new NullReferenceException();
            SqlConnectionStringBuilder sqlCnt = new SqlConnectionStringBuilder();
            sqlCnt["Server"] = server;
            sqlCnt.InitialCatalog = database;
            sqlCnt.IntegratedSecurity = true;
            return sqlCnt.ToString();
        }

        /// <summary>
        /// Creates a connection string and returns it.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static string CreateConnectionString(string server, string username, string password, string database)
        {
            if (server == null || username == null || password == null || database == null)
                throw new NullReferenceException();
            SqlConnectionStringBuilder sqlCnt = new SqlConnectionStringBuilder();
            sqlCnt["Server"] = server;
            sqlCnt["User Id"] = username;
            sqlCnt["Password"] = password;
            sqlCnt.InitialCatalog = database;

            return sqlCnt.ToString();
        }

        /// <summary>
        /// Runs a query and reads the output.
        /// </summary>
        /// <param name="query"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        private static void CommandAndRead(string query)
        {
            if (query == null)
                throw new NullReferenceException();
            SqlCommand command = new SqlCommand(query, SQLConnection);
            SQLConnection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int colAmount = reader.FieldCount;
                    for (int i = 0; i < colAmount; i++)
                        Console.Write(reader[i] + " ");
                    Console.Write(Environment.NewLine);
                }
            }
            SQLConnection.Close();
        }

        /// <summary>
        /// Removes a ware from from the database.
        /// </summary>
        /// <param name="sqlRemove"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static void RemoveWare(string table, string sqlRemove)
        {
            if(table == null || sqlRemove == null)
                throw new NullReferenceException();
            string sqlCommand = $"Use {database}; Delete From {table} Where {sqlRemove}";
            RunCommand(sqlCommand);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="obj"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        public static void AddWare(string table, Dictionary<string, object> obj)
        {
            if (table == null || obj == null)
                throw new NullReferenceException();
            string[] sqlColumns = new string[obj.Count];
            string[] sqlValues = new string[obj.Count];
            int pos = 0;
            foreach(KeyValuePair<string,object> entry in obj)
            {
                sqlColumns[pos] = entry.Key;
                sqlValues[pos] = entry.Value.ToString();
                pos++;
            }

            AddWare(table, sqlColumns, sqlValues);
        }

        /// <summary>
        /// Adds a ware to the database
        /// </summary>
        /// <param name="sqlColumn"></param>
        /// <param name="sqlAddValues"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static void AddWare(string table, string[] sqlColumn, string[] sqlAddValues) //needs to deal with exceptions, like violating the primary key or ware already existing
        {
            if(table == null || sqlColumn == null || sqlAddValues == null)
                throw new NullReferenceException();
            string columns = $"Use {database}; Insert Into {table} (";
            for(int i = 0; i < sqlColumn.Length; i++)
            {
                columns += sqlColumn[i];
                if (i != sqlColumn.Length - 1)
                    columns += ",";
            }
            columns += ") Values (";
            for(int i = 0; i < sqlAddValues.Length; i++)
            {
                columns += sqlAddValues[i];
                if (i != sqlAddValues.Length - 1)
                    columns += ",";
            }
            columns += ");";

            string sqlCommand = columns;
            RunCommand(sqlCommand);
        }

        /// <summary>
        /// Runs a query.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static void RunCommand(string sqlCommand)
        {
            try { 
                SqlCommand command = new SqlCommand(sqlCommand, SQLConnection);
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch(SqlException e)
            {
                sqlConnection.Close();
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="obj"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static bool ModifyWare(string table, Dictionary<string, object> obj, string whereCondition)
        {
            if(table == null || obj == null || whereCondition == null)
                throw new NullReferenceException();
            string[] sqlColumns = new string[obj.Count];
            string[] sqlValues = new string[obj.Count];
            int pos = 0;
            foreach (KeyValuePair<string, object> entry in obj) //function this and the one in AddWare(string,dictionary<string,object>)
            {
                sqlColumns[pos] = entry.Key;
                sqlValues[pos] = entry.Value.ToString();
                pos++;
            }
            try { 
                return ModifyWare(table, sqlColumns, sqlValues, whereCondition);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Modifies one or more ware(s).
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnsToUpdate"></param>
        /// <param name="valuesToUpdateToo"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static bool ModifyWare(string table, string[] columnsToUpdate, string[] valuesToUpdateToo, string whereCondition)
        {
            if (columnsToUpdate.Length != valuesToUpdateToo.Length)
                return false;
            string sqlCommand = $"Use {database}; Update {table} Set "; //instead of this, call the function in StoredProcedures, maybe just in this case this is better (as long time there is more than one column to update)
            for(int n = 0; n <columnsToUpdate.Length; n++)
            {
                sqlCommand += $"{columnsToUpdate[n]} = {valuesToUpdateToo[n]}";
                if (n != columnsToUpdate.Length - 1)
                    sqlCommand += ",";
            }
            sqlCommand += $"Where {whereCondition}";
            try
            {
                RunCommand(sqlCommand);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Selects a ware and writes it out. 
        /// </summary>
        /// <param name="sqlSelect"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static void SelectWare(string sqlSelect)
        {
            string sqlCommand = sqlSelect;
            try
            {
                CommandAndRead(sqlCommand);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Tries to establish a connection to the database in <paramref name="connectionString"/>. Returns the connection if it could connect, else null.
        /// </summary>
        /// <param name="connectionString">The sql datbase connection string</param>
        /// <returns>Returns the sql connection</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private static SqlConnection CreateConnection(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                connection.Close();
                SQLConnection = connection;
                    return connection;
                
            }
            catch (Exception e)
            {
                sqlConnection = null;
                //Console.WriteLine(e);
                StorageSystemCore.Reporter.Report(e);
                throw e;
            }
        }

        /// <summary>
        /// Tries to establish a connection to the database out from the data in <paramref name="sqlInfo"/> and <paramref name="window"/>. Returns the connection if it could connect, else null.
        /// </summary>
        /// <param name="sqlInfo">Contains the information needed to create a database connection string</param>
        /// <param name="window">If true the connection will be using Window login, else SQL Server login</param>
        /// <returns>Returns true if it could establish a connection, else false.</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool CreateConnection(string[] sqlInfo, bool window) 
        {
            try
            {
                string connect;
                if (window)
                    connect = CreateConnectionString(sqlInfo[0], sqlInfo[1]);
                else
                    connect = CreateConnectionString(sqlInfo[0], sqlInfo[1], sqlInfo[2], sqlInfo[3]);
                CreateConnection(connect);
                return true;
            }
            catch (Exception e)
            {
                StorageSystemCore.Reporter.Report(e);
                throw e;
            }
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static List<List<string>> GetValuesMultiWares(string query)
        {
            try { 
                List<List<string>> information = new List<List<string>>();
                SqlCommand command = new SqlCommand(query, SQLConnection);
                SQLConnection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        information.Add(new List<string>());
                        int colAmount = reader.FieldCount;
                        for (int i = 0; i < colAmount; i++)
                            information[information.Count - 1].Add(reader[i].ToString());
                    }
                }
                SQLConnection.Close();
                return information;

            }
            catch (SqlException e)
            {
                StorageSystemCore.Reporter.Report(e);
                throw e;
            }
        }

        /// <summary>
        /// Gets the value(s) in <paramref name="sqlColumn"/> from the object with the specific <paramref name="ID"/>.
        /// </summary>
        /// <param name="sqlColumn"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static List<string> GetValuesSingleWare(string[] sqlColumn, string ID)
        {
            if (sqlColumn == null || ID == null)
                throw new NullReferenceException();
            List<string> information = new List<string>();
            string query = $"Use {database}; Select ";
            for (int i = 0; i < sqlColumn.Length; i++)
            {
                query += sqlColumn[i];
                if (i != sqlColumn.Length - 1)
                    query += ", ";
            }
            query += $" From Inventory Where id = {ID};";
            try { 
                SqlCommand command = new SqlCommand(query, SQLConnection);
                SQLConnection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int colAmount = reader.FieldCount;
                        for (int i = 0; i < colAmount; i++)
                            information.Add(reader[i].ToString());
                    }
                }
                SQLConnection.Close();
                return information;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlColumn"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static List<List<string>> GetValuesAllWare(string[] sqlColumn)
        {
            if (sqlColumn == null)
                throw new NullReferenceException();
            try 
            { 
                List<List<string>> information = new List<List<string>>();
                string query = $"Use {database}; Select ";
                for (int i = 0; i < sqlColumn.Length; i++)
                {
                    query += sqlColumn[i];
                    if (i != sqlColumn.Length - 1)
                        query += ", ";
                }
                query += $" From Inventory;";
                SqlCommand command = new SqlCommand(query, SQLConnection);
                SQLConnection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        information.Add(new List<string>());
                        int colAmount = reader.FieldCount;
                        for (int i = 0; i < colAmount; i++)
                            information[information.Count-1].Add(reader[i].ToString());
                    }
                }
                SQLConnection.Close();
                return information;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static List<string> GetColumnNames(string query) //needs try catch
        {
            List<string> information = new List<string>();
            SqlCommand command = new SqlCommand(query, SQLConnection);
            SQLConnection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int colAmount = reader.FieldCount;
                    for (int i = 0; i < colAmount; i++)
                        information.Add(reader[i].ToString());
                }
            }
            SQLConnection.Close();
            return information;
        }


        /// <summary>
        /// Runs <paramref name="query"/> in the sql database and returns a string list with the values.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static List<string> GetValuesSingleWare(string query)
        {
            try { 
                List<string> information = new List<string>(); 
                SQLConnection.Open();
                SqlCommand command = new SqlCommand(query, SQLConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int colAmount = reader.FieldCount;
                        for (int i = 0; i < colAmount; i++)
                            information.Add(reader[i].ToString());
                    }
                }
                return information;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static string[] GetColumnNamesAndTypes(string query, out string[] types)
        {
            try
            {
                List<string> columnsList = new List<string>();
                List<string> typesList = new List<string>();
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(query, SQLConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columnsList.Add(reader[1].ToString());
                        typesList.Add(reader[0].ToString());
                    }
                }
                types = typesList.ToArray();
                return columnsList.ToArray();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlInfo"></param>
        /// <param name="masterConnection"></param>
        /// <param name="window"></param>
        /// <returns></returns>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static bool InitalitionOfDatabase(string[] sqlInfo, string masterConnection, bool window)
        {
            try
            {
                StorageSystemCore.Reporter.Log($"Initalising database creation.");
                CreateConnection(masterConnection);

                //create database
                DatabaseCreation.InitialiseDatabase();

                CreateConnection(sqlInfo, window); //creates the main connection that is connected directly to the database.
                StoredProcedures.CreateAllStoredProcedures();
                DatabaseCreation.CreateDefaultEntries();
                StorageSystemCore.Reporter.Log("Database created.");
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            { //log the error in the reporter
                throw e;
                //return false;
            }
        }

    }
}
