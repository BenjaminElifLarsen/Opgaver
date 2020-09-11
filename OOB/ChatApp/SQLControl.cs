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

        public static void SQLConnect(string database, string servername = "BENJAMIN-ELIF-L\\MSSQLSERVER02")
        {
            SQLet.ConnectSqlServer(database, servername,"SA","Password123.");
        }

        public static void SQLCreateDatabase()
        {
            SQLet.Execute($@"
                    If Not Exists(Select 1 From sys.databases Where name = '{GetDatabaseName}')
                    Begin 
                        Create Database {GetDatabaseName};
                    End
                ");
            //string databaseCreationSQL = String.Format(@"If Not Exists(Select 1 From sys.databases Where name = '{0}') Begin Create Database {0}; End", GetDatabaseName);
            //SQLet.Execute(databaseCreationSQL);

            string password = HashConverter.StringToHash("admin");
            SQLet.Execute($@"
                If Exists(Select 1 From sys.databases Where name = '{GetDatabaseName}')
                Begin
                    Use {GetDatabaseName}
                    iF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES Where Table_Type='BASE Table' And Table_Name='User_Information')
                    Begin
                        Create Table User_Information
                        (UserID Int Not Null Primary Key Identity(1,1), 
                         UserName NVARCHAR(16) Not Null Unique,
                         UserPassword NVARCHAR(256) Not null, Admin_level Int null);
                    End
                    iF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES Where Table_Type='BASE Table' And Table_Name='Message_Information')
                    BEGIN
                         Create Table Message_Information
                        (MessageID Int Not Null Primary Key IDENTITY(1,1), 
                         UserID Int Not Null Constraint FK_userID_message FOREIGN KEY REFERENCES User_Information(UserID) On Delete Cascade On Update Cascade,
                         Message NVARCHAR(255) Not Null, Time NVARCHAR(255) Not Null);

                         Insert Into User_Information(UserName,UserPassword,Admin_level)
                         Values('Admin','{password}',9)
                    END
                End
            ");
        }

        public static string[] SQLGetUsers(int adminLevel)
        {
            string[][] array = SQLet.GetArray($"Use {GetDatabaseName}; Select UserName From User_Information Where Admin_Level < {adminLevel} Or Admin_level IS Null"); 
            string[] usernames = new string[array.GetLength(0)];
            for (int n = 0; n < usernames.Length; n++)
                usernames[n] = array[n][0];
            return usernames;
        }

        public static void SQLRemoveUser(string username)
        {
            string sql = $"Use {GetDatabaseName}; Delete from User_Information where UserName = '{username}'";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        public static void SQLAlterUser(string sql)
        {
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        public static void SQLAddMessage(string message, string time)
        {
            string sql = $"Use {GetDatabaseName}; Insert into Message_Information(Message, Time, UserID) Values('{message}','{time}','{UserDirectory.GetUserID}')";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        public static void SQLGetMessages()
        {
            string[][] result = SQLet.GetArray($"Use {GetDatabaseName}; Select Time, UserName, Message From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;");
            DisplaySelect(result, "|Time | User | Message |");
        }

        public static void SQLGetMessages(string column)
        {
            string[][] result = SQLet.GetArray($"Use {GetDatabaseName}; Select {column} From Message_Information");
            DisplaySelect(result, column);
        }

        static void DisplaySelect(string[][] text, string message)
        {
            //int pos = 1;
            Console.WriteLine(message);

            for(int n = 0; n < text.Length; n++)
            {
                string[] dateTimeParts = text[n][0].Split(' '); //needs to ensure the familiy is the same 
                string[] dateParts = dateTimeParts[0].Split('-');
                string[] timeParts = dateTimeParts[1].Split(':');
                DateTime oldTime = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]), int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
                string time = oldTime.ToLocalTime().ToString();
                int days = (int)(DateTime.Now - oldTime).TotalDays;
                if (days > 1)
                    text[n][0] = days + " days at " + time.Split(' ')[1];
                else
                    text[n][0] = time;
                //Console.WriteLine("Row {0}", pos++);
                Console.Write("| ");
                foreach (string str in text[n])
                {
                    Console.Write(str + " | ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        public static void SQLRemoveMessage(string data)
        {
            string sql = $"Use {GetDatabaseName}; Delete from Message_Information where {data}";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }

        public static void SQLAlterMessage(string column, string value, string condition)
        {
            try 
            { 
                string sql = $"Use {GetDatabaseName}; Update Message_Information Set {column} = {value} where {condition}";
                Thread SQLTread = new Thread(ThreadedControl);
                SQLTread.Start(sql);
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.Read();
            }
        }

        private static void ThreadedControl(object sql)
        {
            SQLet.Execute((string)sql);
        }

        public static string[][] GetUserInformation(string username, string info)
        {
            return SQLet.GetArray($"Use {GetDatabaseName}; Select {info} from User_Information where UserName = {username};");
        }
    }
}
