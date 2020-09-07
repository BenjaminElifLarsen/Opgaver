using System;
using System.Collections.Generic;
using System.Text;
using TECHCOOL;

namespace ChatApp
{
    static class Login
    {

        static public void RunLogin()
        {
            bool run = true;
            string[] options = new string[] { "New Login", "Old Login", "Back" };
            do
            {
                byte answer = Menu.MenuRun(options, UserDirectory.GetUserName);
                switch (answer)
                {
                    case 0:
                        UserDirectory.SetUserName = CreateLogin();
                        break;

                    case 1:
                        SetLogin();
                        break;

                    case 2:
                        run = false;
                        break;
                }
            } while (run);
        }

        static public void SetLogin()
        {
            string username = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Login");
                username = Console.ReadLine();
                if (!UserDirectory.DoesLoginExist(username))
                    UserDirectory.SetUserName = username;
                else
                    UserDirectory.SetUserName = "Guest";
            } while (username == "");
        }

        static public string CreateLogin()
        {
            string username = "";
            do
            {
                do
                {
                    Console.Clear();
                    Console.Write("Username: ");
                    username = Console.ReadLine().Trim();
                    SQLet.Execute($"Use Chat Insert Into User_Information(UserName,UserPassword) Values('{username}','Test123.')");
                } while (!UserDirectory.DoesLoginExist(username));
            } while (username == "");

            return username;
        }

    }
}
