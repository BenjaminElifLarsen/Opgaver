using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLTestProject
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
        /// Gets and sets the SQL Connection to a specific database.
        /// </summary>
        public static SqlConnection SQLConnection { get => sqlConnection; set => sqlConnection = value; }

        /// <summary>
        /// Gets and sets the name of the database.
        /// </summary>
        public static string DataBase { get => database; set => database = value; }

        /// <summary>
        /// Sanitises <paramref name="queryToSanitise"/> and returns it. 
        /// </summary>
        /// <param name="queryToSanitise"></param>
        /// <returns></returns>
        private static string Sanitise(string queryToSanitise)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a connection string and returns it.
        /// </summary>
        /// <returns></returns>
        public static string CreateConnectionString() //have a function like in the chatapp that creates the database and tables if they do not exist. 
            //So the user when starting should have 3 options, use non-sql "database", the program can create a database where they need to enter server and user/pasword or they can manually run the script and log into its database
        {
            SqlConnectionStringBuilder sqlCnt = new SqlConnectionStringBuilder();
            sqlCnt["Server"] = "localHost,1435";
            sqlCnt["User Id"] = "SA";
            sqlCnt["Password"] = "Password123.";
            return sqlCnt.ToString();
        }

        //private static void Command(string query, SqlConnection connection)
        //{
        //    SqlCommand command = new SqlCommand(query, connection);
        //    connection.Open();
        //    using(SqlDataReader reader = command.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            int colAmount = reader.FieldCount;
        //            for (int i = 0; i < colAmount; i++)
        //                Console.Write(reader[i] + " ");
        //            Console.Write(Environment.NewLine);
        //        }
        //    }
        //    connection.Close();
        //}

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
        public static void RemoveWare(string sqlRemove)
        {
            string sqlCommand = $"Use {database}; Delete From Inventory Where {sqlRemove}";
            RunCommand(sqlCommand);
        }

        /// <summary>
        /// Adds a ware to the database
        /// </summary>
        /// <param name="sqlColumn"></param>
        /// <param name="sqlAddValues"></param>
        public static void AddWare(string[] sqlColumn, string[] sqlAddValues) //needs to deal with exceptions, like violating the primary key
        {
            string columns = $"Use {database}; Insert Into Inventory (";
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
        /// Modifies one or more ware(s).
        /// </summary>
        /// <param name="columnsToUpdate"></param>
        /// <param name="valuesToUpdateToo"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public static bool ModifyWare(string[] columnsToUpdate, string[] valuesToUpdateToo, string whereCondition)
        {
            if (columnsToUpdate.Length != valuesToUpdateToo.Length)
                return false;
            string sqlCommand = $"Use {database}; Update Inventory Set ";
            for(int n = 0; n <columnsToUpdate.Length; n++)
            {
                sqlCommand += $"{columnsToUpdate[n]} = {valuesToUpdateToo[n]}";
                if (n != columnsToUpdate.Length - 1)
                    sqlCommand += ",";
            }
            sqlCommand += $"Where {whereCondition}";

            CommandAndRead(sqlCommand);
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

    }
}
