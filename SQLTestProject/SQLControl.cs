using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SQLTestProject
{
    class SQLControl
    {
        private static string database;
        private static SqlConnection sqlConnection; 

        public static SqlConnection SQLConnection { get => sqlConnection; set => sqlConnection = value; }
        public static string DataBase { get => database; set => database = value; }

        private static string Sanitise(string queryToSanitise)
        {
            throw new NotImplementedException();
        }

        public static string CreateConnectionString() //have a function like in the chatapp that creates the database and tables if they do not exist. 
            //So the user when starting should have 3 options, use non-sql "database", the program can create a database where they need to enter server or they can manually run the script and log into its database
        {
            SqlConnectionStringBuilder sqlCnt = new SqlConnectionStringBuilder();
            sqlCnt["Server"] = "localHost,1435";
            sqlCnt["User Id"] = "SA";
            sqlCnt["Password"] = "Password123.";
            return sqlCnt.ToString();
        }

        private static void Command(string query, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int colAmount = reader.FieldCount;
                    for (int i = 0; i < colAmount; i++)
                        Console.Write(reader[i] + " ");
                    Console.Write(Environment.NewLine);
                }
            }
            connection.Close();
        }

        private static void Command(string query)
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

        public static void RemoveWare(string sqlRemove)
        {
            string sqlCommand = $"Use {database}; Delete From Inventory Where {sqlRemove}";
            RunCommand(sqlCommand);
        }

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

        private static void RunCommand(string sqlCommand)
        {
            SqlCommand command = new SqlCommand(sqlCommand, SQLConnection);
            sqlConnection.Open();
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }

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

            Command(sqlCommand);
            return true;
        }

        public static void SelectWare(string sqlSelect)
        {
            string sqlCommand = sqlSelect;
            Command(sqlCommand);
        }


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
