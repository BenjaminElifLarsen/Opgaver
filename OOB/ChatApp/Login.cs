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
                {
                    if (EnterPassword(username))
                        UserDirectory.SetUserName = username;
                    else
                        UserDirectory.SetUserName = "Guest";
                }
                else
                    UserDirectory.SetUserName = "Guest";
            } while (username == "");
        }

        static private bool EnterPassword(string login)
        {
            Console.Clear();
            Console.WriteLine("Please Enter the Password");
            string password = Console.ReadLine();
            password = HashConverter.StringToHash(password);
            string[][] passwordOfUser = SQLet.GetArray($"Select * From User_Information Where UserName = '{login}' And UserPassword = '{password}'");
            
            if (passwordOfUser.Length > 0)
                return true;
            return false;
        }

        static public string CreateLogin()
        {
            string username = "";
            do
            {
                do
                {
                    do
                    {
                        Console.Write("Username: ");
                        username = Console.ReadLine().Trim();
                        Console.Clear();
                    } while (!ValidUserName(username));
                } while (!UserDirectory.DoesLoginExist(username));
            } while (username == "");

            string password = "";
            do
            {
                Console.Clear();
                Console.Write("Password: ");
                password = Console.ReadLine().Trim();
            } while (password == "");

            SQLet.Execute($"Use {SQLControl.GetDatabaseName} Insert Into User_Information(UserName,UserPassword) Values('{username}','{HashConverter.StringToHash(password)}')");

            return username;
        }

        static private bool ValidUserName(string IDToCheck)
        {
            if (!RegexControl.IsValidLength(IDToCheck))
            {
                Console.WriteLine("Invalid: Wrong Length, min = 6, max = 16");
                return false;
            }
            if (!RegexControl.IsValidValues(IDToCheck))
            {
                Console.WriteLine("Invalid: No numbers");
                return false;
            }
            if (!RegexControl.IsValidLettersLower(IDToCheck))
            {
                Console.WriteLine("Invalid: No lowercase letters");
                return false;
            }
            if (!RegexControl.IsValidLettersUpper(IDToCheck))
            {
                Console.WriteLine("Invalid: No uppercase letters");
                return false;
            }
            if (!RegexControl.IsValidSpecial(IDToCheck))
            {
                Console.WriteLine("Invalid: No special symbols: {0}", RegexControl.GetSpecialSigns);
                return false;
            }
            return true;
        }

    }
}
