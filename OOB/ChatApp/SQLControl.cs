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
        public static string GetDatabaseName { get => "Chat"; }

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
                             ChannelID Int Null Constraint FK_channelID_message FOREIGN KEY REFERENCES Channel_Information(ChannelID) On Delete Cascade On Update Cascade,
                             RecipientID Int Null);
                            
                            Insert Into User_Information(UserName,UserPassword,Admin_level)
                            Values('Admin','{password}',9);
                        END;

                        IF Object_ID('dbo.LatestMessages') is null
                        Begin
                            DECLARE @v_ViewCreateStatement VARCHAR(MAX) = '
                            Create View LatestMessages as Select Top 30 Time, Sender.UserID, Sender.UserName, Message, MessageID, Message_Information.RecipientID, Recipient.UserName as RecipientName
                            From Message_Information 
                            Inner Join User_Information As Sender on Sender.UserID = Message_Information.UserID
                            Left Join User_Information As Recipient On Message_Information.RecipientID = Recipient.UserID
                            '
                            EXEC(@v_ViewCreateStatement)
                        End
                   
                    End
                ";
                //Console.WriteLine(cmd);
                SQLet.Execute(cmd);


                Reporter.Log($"Established connection to the database");
                return true;
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                Reporter.Log($"Could not establish connection to the database.");
                Reporter.Report(e);
                Console.WriteLine("Could not establish connection to the database, please retry.");
                Debug.WriteLine(e);
                Console.ReadKey();
                return false;
            }
            catch (Exception e)
            {
                Reporter.Log($"Encountered error: " + e.Message);
                Reporter.Report(e);
                Console.WriteLine("Error");
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
            string[][] array = SQLet.GetArray($"Use {GetDatabaseName}; Select UserName, UserID From User_Information Where UserID = {userID}");
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

        public static void SQLAddMessage(string message, int userID, int recipID)
        {
            string time = DateTime.UtcNow.ToString("G", new CultureInfo("da-DK"));
            if (RegexControl.ContainsSingleQuouteMark(message))
                message = Support.SanitiseSingleQuotes(message);
            string sql = $"Use {GetDatabaseName}; Insert into Message_Information(Message, Time, UserID,RecipientID) Values('{message}','{time}',{userID},{recipID})";
            Thread SQLTread = new Thread(ThreadedControl);
            SQLTread.Start(sql);
        }
        

        public static void SQLGetMessagesAndDisplay() //change at some point
        {
            string[][] result = SQLet.GetArray($"Use {GetDatabaseName}; Select Time, UserName, Message From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;");
            Support.DisplaySelect(result, "|Time | User | Message |");
            //SQLGetMessages();
        }

        public static List<Message> SQLGetMessages()
        {
            string[][] messagesString = SQLet.GetArray($@"Use {GetDatabaseName}; Select * From LatestMessages
                where (RecipientID = 0 || RecipientID Is Null)"/*$"Use {GetDatabaseName}; Select Time, UserName, Message, MessageID, Message_Information.UserID From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;"*/); ;
            List<Message> messageList = new List<Message>();
            foreach (string[] message in messagesString)
            {
                if(message[5] != "NULL")
                    messageList.Add(new Message(new User(message[2], int.Parse(message[1])), new User(message[6], int.Parse(message[5])), message[3], message[0], int.Parse(message[4])));
                else
                    messageList.Add(new Message(new User(message[2], int.Parse(message[1])), message[3], message[0], int.Parse(message[4])));
            }

            return messageList;//SQLet.GetArray($"Use {GetDatabaseName}; Select Time, UserName, Message From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;");
        }

        public static List<Message> SQLGetMessages(User user)
        {
            string[][] messagesString = SQLet.GetArray($@"Use {GetDatabaseName}; Select * From LatestMessages
                where (RecipientID = 0 || RecipientID Is Null) or RecipientID = {user.ID} or UserID = {user.ID}"/*$"Use {GetDatabaseName}; Select Time, UserName, Message, MessageID, Message_Information.UserID From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;"*/); ;
            List<Message> messageList = new List<Message>();
            foreach (string[] message in messagesString)
            {
                if (message[5] != "NULL")
                    messageList.Add(new Message(new User(message[2], int.Parse(message[1])), new User(message[6], int.Parse(message[5])), message[3], message[0], int.Parse(message[4])));
                else
                    messageList.Add(new Message(new User(message[2], int.Parse(message[1])), message[3], message[0], int.Parse(message[4])));
            }

            return messageList;//SQLet.GetArray($"Use {GetDatabaseName}; Select Time, UserName, Message From Message_Information inner join User_Information on User_Information.UserID = Message_Information.UserID;");
        }

        public static void SQLGetMessages(string column)
        {
            string[][] result = SQLet.GetArray($"Use {GetDatabaseName}; Select {column} From Message_Information");
            Support.DisplaySelect(result, column);
        }


        public static void SQLRemoveMessage(string data)
        {
            try
            {
                string sql = $"Use {GetDatabaseName}; Delete from Message_Information where {data}";
                Thread SQLTread = new Thread(ThreadedControl);
                SQLTread.Start(sql);
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                Reporter.Report(e);
                Console.WriteLine("Could not delete data, see "+Reporter.ErrorLocation);
            }
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
                Reporter.Report(e);
                Console.WriteLine("Could not update. For error see " + Reporter.ErrorLocation);
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Reporter.Report(e);
                Console.WriteLine("Encountered error, see " + Reporter.ErrorLocation);
                Console.Read();
            }
        }

        private static void ThreadedControl(object sql)
        {
            try
            {
                Reporter.Log("SQL query was sent to the SQL database: " + Environment.NewLine + (string)sql);
                SQLet.Execute((string)sql);
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                Reporter.Log($"Failed running the SQL query");
                Reporter.Log("Database failed running the SQL query:" + Environment.NewLine + sql);
                Reporter.Report(e);
            }
        }

        public static string[][] GetUserInformation(string username, string info)
        {
            try
            {
                Reporter.Log($"Accessed information for {username} about {info}");
                return SQLet.GetArray($"Use {GetDatabaseName}; Select {info} from User_Information where UserName = {username};");
            }
            catch (Microsoft.Data.SqlClient.SqlException e)
            {
                Reporter.Log($"Failed at getting information {info} about user {username}");
                Reporter.Report(e);
                Console.WriteLine("Could not arquier data");
                Console.Read();
                return null;
            }
        }

        public static void CreateUser(string username, string password)
        {
            try
            {
                Reporter.Log($"Creating user {username}");
                SQLet.Execute($"Use {GetDatabaseName}; Insert Into User_Information(UserName,UserPassword) Values('{username}','{HashConverter.StringToHash(password)}')");
            }
            catch(Microsoft.Data.SqlClient.SqlException e)
            {
                Reporter.Log($"Failed at creating user {username}");
                Reporter.Report(e);
            }
        }
    }
}
