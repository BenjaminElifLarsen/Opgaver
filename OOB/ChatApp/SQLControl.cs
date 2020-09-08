using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TECHCOOL;

namespace ChatApp
{
    static class SQLControl
    {
        public static string GetDatabaseName { get => "ChatTest"; }
        public static void SQLConnect()
        {
            SQLet.ConnectSqlServer(GetDatabaseName, "BENJAMIN-ELIF-L\\MSSQLSERVER02");
        }

        public static void SQLCreateDatabase()
        {
            SQLet.Execute($@"
                If Not Exists(Select 1 From sys.databases Where name = '{GetDatabaseName}')
                Begin 
                    Create Database {GetDatabaseName};
                End
            ");
            SQLet.Execute($@"
                If Exists(Select 1 From sys.databases Where name = '{GetDatabaseName}')
                Begin
                    Use {GetDatabaseName}
                    iF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES Where Table_Type='BASE Table' And Table_Name='User_Information')
                    Begin
                        Create Table User_Information
                        (UserID Int Not Null Primary Key Identity(1,1), 
                         UserName NVARCHAR(16) Not Null Unique,
                         UserPassword NVARCHAR(24) Not null, Admin_level Int null);
                    End
                    iF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES Where Table_Type='BASE Table' And Table_Name='Message_Information')
                    BEGIN
                         Create Table Message_Information
                        (MessageID Int Not Null Primary Key IDENTITY(1,1), 
                         UserID Int Not Null Constraint FK_userID_message FOREIGN KEY REFERENCES User_Information(UserID) On Delete Cascade On Update Cascade,
                         UserName NVARCHAR(16) Not Null Constraint FK_username_message FOREIGN KEY REFERENCES User_Information(UserName), 
                         Message NVARCHAR(255) Not Null, Time NVARCHAR(255) Not Null);

                         Insert Into User_Information(UserName,UserPassword,Admin_level)
                         Values('Benjamin','Test123.',9)
                    END
                End
            ");
        }

        public static void SQLAddMessage(string message, string time)
        {
            string sql = $"Use {GetDatabaseName} Insert into Message_Information(UserName, Message, Time, UserID) Values('{UserDirectory.GetUserName}','{message}','{time}','{UserDirectory.GetUserID}')";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        public static void SQLGetMessages(string column)
        {
            string[][] result = SQLet.GetArray($"Select {column} From Message_Information");
            DisplaySelect(result, column);
        }

        static void DisplaySelect(string[][] text, string message)
        {
            int pos = 1;
            Console.WriteLine(message);
            foreach (string[] stringArray in text)
            {
                //Console.WriteLine("Row {0}", pos++);
                Console.Write("| ");
                foreach (string str in stringArray)
                {
                    Console.Write(str + " | ");
                }
                Console.Write(Environment.NewLine);
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
