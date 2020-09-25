using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TECHCOOL;

namespace ChatApp
{
    static class UserDirectory
    {
        //static private Dictionary<string, User> users = new Dictionary<string, User>();
        static private string loggedInUser;

        private static User user;

        static public string GetUserName { get => loggedInUser; }
        static public string SetUserName { set => loggedInUser = value; }
        static public int GetUserID { get => FindID(user.Name); }

        public static User User { get => user; set => user = value; }

        static UserDirectory()
        {
            user = new User("Guest", 0);
            loggedInUser = "Guest";
        }

        static public bool DoesLoginExist(string login)
        {
                Result result = SQLet.GetResult($"Use {SQLControl.GetDatabaseName}; Select Distinct UserName From User_Information");
                for (int i = 0; i < result.Count; i++)
                {
                    if (login == result[i]["UserName"])
                        return false;
                }
                return true;
        }

        static private int FindID(string login)
        {

            if(user.Name != "Guest")
            { 
                string[][] IDs = SQLet.GetArray($"Use {SQLControl.GetDatabaseName}; Select UserID From User_Information where UserName = '{login}'");
                return int.Parse(IDs[0][0]);
            }
            return 0;
        }

        static public void AlterUser()
        {
            Console.Clear();
            string[] users = SQLControl.SQLGetUsers(9);
            if(users.Length != 0)
            {
                DisplayUsers();
                string username = SelectUser();

                string[] options = new string[] { "Admin Level", "User name", "Password" };
                byte answer = MenuVisual.MenuRun(options);
                string response = "";
                string command = "";
                Console.Clear();
                string sql = "";
                switch (answer)
                {
                    case 0:
                        Console.Write("Enter Admin Level: ");
                        do {
                            response = Console.ReadLine();
                        } while (!int.TryParse(response, out _));
                        command = "Admin_level";
                        Reporter.Log($"User {User.Name} transmitted query to change {username}s admin level to {response}");
                        sql = $"Use {SQLControl.GetDatabaseName}; Update User_Information Set {command} = {response} where UserName = '{username}'";
                        break;

                    case 1:
                        Console.Write("Enter New User Name: ");
                        do
                        {
                            response = Console.ReadLine();
                        } while (!Login.ValidUserName(response));
                        command = "UserName";
                        Reporter.Log($"User {User.Name} transmitted query to change {username}'s username to {response}");
                        sql = $"Use {SQLControl.GetDatabaseName}; Update User_Information Set {command} = '{response}' where UserName = '{username}'";
                        break;

                    case 2:
                        Console.Write("Enter New Password: ");
                        response = Login.HiddenText();
                        command = "UserPassword";
                        Reporter.Log($"User {User.Name} transmitted query to change {username}'s password.");
                        sql = $"Use {SQLControl.GetDatabaseName}; Update User_Information Set {command} = '{HashConverter.StringToHash(response)}' where UserName = '{username}'";
                        break;
                }
                if (!RegexControl.ContainsForbiddenWords(sql))
                {
                    SQLControl.SQLAlterUser(sql);
                }
                else
                {
                    Reporter.Log($"Sql query contains forbidden words. Writting by user {User.Name}. {Environment.NewLine} SQL: {sql}");
                    Support.FoundForbiddenWord();
                }

            }
            else
                Console.WriteLine("No Users Permitted to be showned");

            void DisplayUsers()
            {
                foreach (string user in users)
                    Console.Write(user + '\t');
                Console.Write(Environment.NewLine + "Enter Username: ");
            }

            string SelectUser()
            {
                string selectedName = "";
                do
                {
                    selectedName = Console.ReadLine();
                } while (selectedName == "" || !UserExist(selectedName));
                return selectedName;
            }
        }

        static public void RemoveUser()
        {
            Console.Clear();
            string[] users = SQLControl.SQLGetUsers(9);
            if(users.Length != 0)
            {
                foreach (string user in users)
                {
                    Console.Write(user + '\t');
                    if (Console.CursorLeft >= Console.WindowWidth - 10)
                        Console.SetCursorPosition(0, Console.CursorTop++);
                }
                Console.Write(Environment.NewLine + "Enter Username: ");
                string username = "";
                do
                {
                    username = Console.ReadLine();
                } while (username == "" || !UserExist(username));
                SQLControl.SQLRemoveUser(username);
            }
            else
                Console.WriteLine("No Users Permitted to be showned");
        }

        static public void ShowUser(int adminlevel)
        {
            Console.Clear();
            string[] users = SQLControl.SQLGetUsers(adminlevel);
            if (users.Length != 0)
            {
                foreach (string user in users)
                {
                    Console.Write(user + '\t');
                    if (Console.CursorLeft >= Console.WindowWidth - 10)
                        Console.SetCursorPosition(0, Console.CursorTop++);
                }
            }
            else
                Console.WriteLine("No Users Permitted to be showned");
            Console.ReadKey();
        }

        static public bool UserExist(string username)
        {
            string[] users = SQLControl.SQLGetUsers(9);
            foreach (string user in users)
                if (user == username)
                    return true;
            return false;
        }

    }
}
