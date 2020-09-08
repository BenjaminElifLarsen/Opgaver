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
        static private int loggedInID; 
        
        static public string GetUserName { get => loggedInUser; }
        static public string SetUserName { set => loggedInUser = value; }
        static public int GetUserID { get => FindID(loggedInUser); }
        static UserDirectory()
        {
            loggedInUser = "Guest";
        }

        static public bool DoesLoginExist(string login) //should check the database and find the different usernames
        {
            Result result = SQLet.GetResult($"Select Distinct UserName From User_Information");
            for (int i = 0; i < result.Count; i++)
            {
                if(login == result[i]["UserName"])
                        return false;
            }
            return true;
        }

        static private int FindID(string login)
        {
            if(GetUserName != "Guest")
            { 
                string[][] IDs = SQLet.GetArray($"Select UserID From User_Information where UserName = '{login}'");
                return int.Parse(IDs[0][0]);
            }
            return 0;
        }

        static public void AlterUser()
        {
            Console.Clear();
            string[] users = SQLControl.SQLGetUsers();
            foreach (string user in users)
                Console.Write(user + '\t');
            Console.Write(Environment.NewLine + "Enter Username: ");
            string username = "";
            do
            {
                username = Console.ReadLine();


            } while (username == "");
            string[] options = new string[] {"Admin Level", "User name", "Password" };
            byte answer = Menu.MenuRun(options);
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
                    sql = $"Update User_Information Set {command} = {response} where UserName = '{username}'";
                    break;

                case 1:
                    Console.Write("Enter New User Name: ");
                    response = Console.ReadLine();
                    command = "UserName";
                    sql = $"Update User_Information Set {command} = '{response}' where UserName = '{username}'";
                    break;

                case 2:
                    Console.WriteLine("Enter New Password: ");
                    response = Console.ReadLine();
                    command = "UserPassword";
                    sql = $"Update User_Information Set {command} = '{response}' where UserName = '{username}'";
                    break;
            }
            
            Debug.WriteLine(sql);
            SQLControl.SQLAlterUser(sql);
        }

        static public void RemoveUser()
        {
            Console.Clear();
            string[] users = SQLControl.SQLGetUsers();
            foreach (string user in users)
                Console.Write(user + '\t');
            Console.Write(Environment.NewLine + "Enter Username: ");
            string username = "";
            do
            {
                username = Console.ReadLine();


            } while (username == "");
            SQLControl.SQLRemoveUser(username);
        }

        static public void ShowUser()
        {
            Console.Clear();
            string[] users = SQLControl.SQLGetUsers();
            foreach (string user in users)
                Console.Write(user + '\t');
            Console.ReadKey();
        }

    }
}
