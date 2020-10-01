﻿using System;
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
        public static SqlConnection SQLConnection { get => sqlConnection; set => sqlConnection = value; }

        /// <summary>
        /// Gets and sets the name of the database.
        /// </summary>
        public static string DataBase { get => database; set => database = value; }

        public static string[][] TablesAndColumns { get => tablesAndColumns; }

        /// <summary>
        /// Sanitises <paramref name="queryToSanitise"/> and returns it. 
        /// </summary>
        /// <param name="queryToSanitise"></param>
        /// <returns></returns>
        public static string SanitiseSingleQuotes(string queryToSanitise)
        {
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
        public static string CreateConnectionString(string server, string database)
        {
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
        public static string CreateConnectionString(string server, string username, string password, string database)
        {
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
        private static void CommandAndRead(string query)
        {
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
        public static void RemoveWare(string table, string sqlRemove)
        {
            string sqlCommand = $"Use {database}; Delete From {table} Where {sqlRemove}";
            RunCommand(sqlCommand);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="obj"></param>
        public static void AddWare(string table, Dictionary<string, object> obj)
        {
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
        public static void AddWare(string table, string[] sqlColumn, string[] sqlAddValues) //needs to deal with exceptions, like violating the primary key or ware already existing
        {
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
        private static void RunCommand(string sqlCommand)
        {
            SqlCommand command = new SqlCommand(sqlCommand, SQLConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="obj"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public static bool ModifyWare(string table, Dictionary<string, object> obj, string whereCondition)
        {
            string[] sqlColumns = new string[obj.Count];
            string[] sqlValues = new string[obj.Count];
            int pos = 0;
            foreach (KeyValuePair<string, object> entry in obj) //function this and the one in AddWare(string,dictionary<string,object>)
            {
                sqlColumns[pos] = entry.Key;
                sqlValues[pos] = entry.Value.ToString();
                pos++;
            }

            return ModifyWare(table, sqlColumns, sqlValues, whereCondition);
        }

        /// <summary>
        /// Modifies one or more ware(s).
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnsToUpdate"></param>
        /// <param name="valuesToUpdateToo"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public static bool ModifyWare(string table, string[] columnsToUpdate, string[] valuesToUpdateToo, string whereCondition)
        {
            if (columnsToUpdate.Length != valuesToUpdateToo.Length)
                return false;
            string sqlCommand = $"Use {database}; Update {table} Set ";
            for(int n = 0; n <columnsToUpdate.Length; n++)
            {
                sqlCommand += $"{columnsToUpdate[n]} = {valuesToUpdateToo[n]}";
                if (n != columnsToUpdate.Length - 1)
                    sqlCommand += ",";
            }
            sqlCommand += $"Where {whereCondition}";

            RunCommand(sqlCommand);
            return true;
        }

        /// <summary>
        /// Selects a ware and writes it out. 
        /// </summary>
        /// <param name="sqlSelect"></param>
        public static void SelectWare(string sqlSelect)
        {
            string sqlCommand = sqlSelect;
            CommandAndRead(sqlCommand);
        }

        /// <summary>
        /// Tries to establish a connection to the database in <paramref name="connectionString"/>. Returns the connection if it could connect, else null.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static SqlConnection CreateConnection(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                connection.Close();
                SQLConnection = connection;
                    return connection;
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static List<List<string>> GetValues(string query)
        {
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

        /// <summary>
        /// Gets the value(s) in <paramref name="sqlColumn"/> from the object with the specific <paramref name="ID"/>.
        /// </summary>
        /// <param name="sqlColumn"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        private static List<string> GetValues(string[] sqlColumn, string ID)
        {
            List<string> information = new List<string>();
            string query = $"Use {database}; Select ";
            for (int i = 0; i < sqlColumn.Length; i++)
            {
                query += sqlColumn[i];
                if (i != sqlColumn.Length - 1)
                    query += ", ";
            }
            query += $" From Inventory Where id = {ID};";
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

    }
}