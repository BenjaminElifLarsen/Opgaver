using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TECHCOOL;

namespace ChatApp
{
    static class UserDirectory
    {
        static private Dictionary<string, User> users = new Dictionary<string, User>();
        static private string loggedInUser;

        static public string GetUserName { get => loggedInUser; }
        static public string SetUserName { set => loggedInUser = value; }
        static public int GetUserID { get => FindID(loggedInUser); }
        static UserDirectory()
        {
            loggedInUser = "Guest";
        }

        static public bool DoesLoginExist(string login) //should check the database and find the different usernames
        {
            if (!RegexControl.ContainsDrop(login))
            {
                Result result = SQLet.GetResult($"Use {SQLControl.GetDatabaseName}; Select Distinct UserName From User_Information");
                for (int i = 0; i < result.Count; i++)
                {
                    if (login == result[i]["UserName"])
                        return false;
                }
                return true;
            }
            else
                Support.FoundForbiddenWord();
            return false;
        }

        static private int FindID(string login)
        {

            if(GetUserName != "Guest")
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
                        response = Console.ReadLine();
                        command = "Admin_level";
                        sql = $"Use {SQLControl.GetDatabaseName}; Update User_Information Set {command} = {response} where UserName = '{username}'";
                        break;

                    case 1:
                        Console.Write("Enter New User Name: ");
                        do
                        {
                            response = Console.ReadLine();
                        } while (!Login.ValidUserName(response));
                        command = "UserName";
                        sql = $"Use {SQLControl.GetDatabaseName}; Update User_Information Set {command} = '{response}' where UserName = '{username}'";
                        break;

                    case 2:
                        Console.Write("Enter New Password: ");
                        response = Login.HiddenText();
                        command = "UserPassword";
                        sql = $"Use {SQLControl.GetDatabaseName}; Update User_Information Set {command} = '{HashConverter.StringToHash(response)}' where UserName = '{username}'";
                        break;
                }
                if(!RegexControl.ContainsDrop(sql))
                    SQLControl.SQLAlterUser(sql);
                else
                    Support.FoundForbiddenWord();

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
