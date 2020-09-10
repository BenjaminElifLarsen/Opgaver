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
            string password = "";
            Console.Clear();
            Console.WriteLine("Please Enter the Password");
            password = Console.ReadLine();
            
            password = HashConverter.StringToHash(password);
            string[][] passwordOfUser = SQLet.GetArray($"Select * From User_Information Where UserName = '{login}' And UserPassword = '{password}'");
            
            if (passwordOfUser.Length > 0)
                return true;
            return false;
        }

        static public string CreateLogin()
        {
            Console.Clear();
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
            Console.Clear();
            string password = "";
            do
            {
                do
                {
                    Console.WriteLine("Please Enter the Password");
                    password = Console.ReadLine();
                    Console.Clear();
                } while (!ValidPassword(password));
            } while (password == "");

            SQLet.Execute($"Use {SQLControl.GetDatabaseName} Insert Into User_Information(UserName,UserPassword) Values('{username}','{HashConverter.StringToHash(password)}')");

            return username;
        }

        static private bool ValidPassword(string passwordToCheck)
        {
            if (!RegexControl.IsValidPasswordLength(passwordToCheck))
            {
                Console.WriteLine("Invalid: Wrong Length, min = 8, max = 26");
                return false;
            }
            if (!RegexControl.IsValidValues(passwordToCheck))
            {
                Console.WriteLine("Invalid: No numbers");
                return false;
            }
            if (!RegexControl.IsValidLettersLower(passwordToCheck))
            {
                Console.WriteLine("Invalid: No lowercase letters");
                return false;
            }
            if (!RegexControl.IsValidLettersUpper(passwordToCheck))
            {
                Console.WriteLine("Invalid: No uppercase letters");
                return false;
            }
            if (!RegexControl.IsValidSpecial(passwordToCheck))
            {
                Console.WriteLine("Invalid: No special symbols: {0}", RegexControl.GetSpecialSigns);
                return false;
            }
            return true;

        }

        static private bool ValidUserName(string usernameToCheck)
        {
            if (!RegexControl.IsValidLength(usernameToCheck))
            {
                Console.WriteLine("Invalid: Wrong Length, min = 4, max = 16");
                return false;
            }
            if (!RegexControl.IsValidLettersLower(usernameToCheck))
            {
                Console.WriteLine("Invalid: No lowercase letters");
                return false;
            }
            if (!RegexControl.IsValidLettersUpper(usernameToCheck))
            {
                Console.WriteLine("Invalid: No uppercase letters");
                return false;
            }
            return true;
        }

    }
}
