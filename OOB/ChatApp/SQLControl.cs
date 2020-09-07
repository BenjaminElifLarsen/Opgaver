using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TECHCOOL;

namespace ChatApp
{
    static class SQLControl
    {
        public static void SQLConnect()
        {
            SQLet.ConnectSqlServer("Chat", "BENJAMIN-ELIF-L\\MSSQLSERVER02");
        }

        public static void SQLAddMessage(string message, string time)
        {
            string sql = $"Use Chat Insert into Message_Information(UserName, Message, Time) Values('{UserDirectory.GetUserName}','{message}','{time}')";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        public static void SQLGetMessages(string column)
        {
            Result result = SQLet.GetResult($"Select {column} From Message_Information");
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i][column]);
            }
        }

        public static void SQLRemoveMessage(string data)
        {
            string sql = $"Delete from Message_Information where {data}";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        public static void SQLAlterMessage(string column, string value, string condition)
        {
            string sql = $"Update Message_Information Set {column} = {value} where {condition}";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        private static void ThreadedControl(object sql)
        {
            SQLet.Execute((string)sql);
        }
    }
}
