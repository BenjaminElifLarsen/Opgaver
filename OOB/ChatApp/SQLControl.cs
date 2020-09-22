using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using TECHCOOL;

namespace ChatApp
{
    static class SQLControl
    {
        public static string GetDatabaseName { get => "ChatTest"; }

        public static void SQLConnect(string database, string servername = "BENJAMIN-ELIF-L\\MSSQLSERVER02", bool windowLogin = true)
        {
            if(windowLogin)
                SQLet.ConnectSqlServer(database, servername);
            else
                SQLet.ConnectSqlServer(database, servername,"SA","Password123.");

        }

        public static bool SQLCreateDatabase() //can encounter the exception of connection force closed by the host of the connection
        {
            try { 
                SQLet.Execute($@"
                        If Not Exists(Select 1 From sys.databases Where name = '{GetDatabaseName}')
                        Begin 
                            Create Database {GetDatabaseName};
                        End
                    ");
                //string databaseCreationSQL = String.Format(@"If Not Exists(Select 1 From sys.databases Where name = '{0}') Begin Create Database {0}; End", GetDatabaseName);
                //SQLet.Execute(databaseCreationSQL);

                string password = HashConverter.StringToHash("admin");
                string cmd = $@"
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


                        iF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES Where Table_Type='BASE Table' And Table_Name='Channel_Information')
                        BEGIN
                             Create Table Channel_Information
                            (ChannelID Int Not Null Primary Key IDENTITY(1,1), 
                             Name NVARCHAR(255) Not Null);
                        END

                        iF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES Where Table_Type='BASE Table' And Table_Name='Message_Information')
                        BEGIN
                             Create Table Message_Information
                            (MessageID Int Not Null Primary Key IDENTITY(1,1), 
                             UserID Int Not Null Constraint FK_userID_message FOREIGN KEY REFERENCES User_Information(UserID) On Delete Cascade On Update Cascade,
                             Message NVARCHAR(255) Not Null, Time NVARCHAR(255) Not Null,
                             ChannelID Int Null Constraint FK_channelID_message FOREIGN KEY REFERENCES channel_Information(ChannelID) On Delete Cascade On Update Cascade);
                            
                            Insert Into User_Information(UserName,UserPassword,Admin_level)
                            Values('Admin','{password}',9);
                        END;

                        IF Object_ID('dbo.LatestMessages') is null
                        Begin
                            DECLARE @v_ViewCreateStatement VARCHAR(MAX) = '
                            Create View LatestMessages as Select Top 30 Time, UserName, Message, MessageID, Message_Information.UserID From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;
                            '
                            EXEC(@v_ViewCreateStatement)
                        End
                   
                    End
                ";
                Console.WriteLine(cmd);
                SQLet.Execute(cmd);



                return true;
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                Console.WriteLine("Could not establish connection to the database, please retry.");
                Debug.WriteLine(e);
                Console.ReadKey();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erorr");
                Debug.WriteLine(e);
                Console.ReadKey();
                return false;
            }
        }

        public static string[] SQLGetUsers(int adminLevel)
        {
            string[][] array = SQLet.GetArray($"Use {GetDatabaseName}; Select UserName From User_Information Where Admin_Level < {adminLevel} Or Admin_level IS Null Order By UserName ASC"); 
            string[] usernames = new string[array.GetLength(0)];
            for (int n = 0; n < usernames.Length; n++)
                usernames[n] = array[n][0];
            return usernames;
        }

        public static List<User> SQLGetUsers()
        {
            string[][] array = SQLet.GetArray($"Use {GetDatabaseName}; Select UserName, UserID From User_Information Where Admin_Level < {9} Or Admin_level IS Null Order By UserName ASC");
            List<User> usernames = new List<User>();
            foreach (string[] strings in array)
                usernames.Add(new User(strings[0], int.Parse(strings[1])));
            return usernames;
        }

        public static User SQLGetUser(string username)
        {
            string[][] array = SQLet.GetArray($"Use {GetDatabaseName}; Select UserName, UserID From User_Information Where Username = '{username}'");
            User user = new User(array[0][0], int.Parse(array[0][1]));
                
            return user;
        }

        public static User SQLGetUser(int userID)
        {
            string[][] array = SQLet.GetArray($"Use {GetDatabaseName}; Select UserName, UserID From User_Information Where UserID = '{userID}'");
            User user = new User(array[0][0], int.Parse(array[0][1]));

            return user;
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

        public static void SQLAddMessage(string message, int id)
        {
            string time = DateTime.UtcNow.ToString("G", new CultureInfo("da-DK"));
            if (RegexControl.ContainsSingleQuouteMark(message))
                message = Support.SanitiseSingleQuotes(message);
            string sql = $"Use {GetDatabaseName}; Insert into Message_Information(Message, Time, UserID) Values('{message}','{time}','{id}')";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);

        }

        public static void SQLGetMessagesAndDisplay() //change at some point
        {
            string[][] result = SQLet.GetArray($"Use {GetDatabaseName}; Select Time, UserName, Message From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;");
            Support.DisplaySelect(result, "|Time | User | Message |");
        }

        public static List<Message> SQLGetMessages()
        {
            string[][] messagesString = SQLet.GetArray($"Use {GetDatabaseName}; Select Time, UserName, Message, MessageID, Message_Information.UserID From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;"); ;
            List<Message> messageList = new List<Message>();
            foreach (string[] message in messagesString)
                messageList.Add(new Message(new User(message[1], int.Parse(message[4])), message[2], message[0], int.Parse(message[3])));

            return messageList;//SQLet.GetArray($"Use {GetDatabaseName}; Select Time, UserName, Message From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;");
        }

        public static void SQLGetMessages(string column)
        {
            string[][] result = SQLet.GetArray($"Use {GetDatabaseName}; Select {column} From Message_Information");
            Support.DisplaySelect(result, column);
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

        public static void CreateUser(string username, string password)
        {
            SQLet.Execute($"Use {GetDatabaseName}; Insert Into User_Information(UserName,UserPassword) Values('{username}','{HashConverter.StringToHash(password)}')");
        }
    }
}
