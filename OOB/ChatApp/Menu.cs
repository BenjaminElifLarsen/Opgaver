﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    static class Menu
    {
        public static void MainMenu()
        {
            string[] options = new string[] { "Login", "Public Control", "Admin Control", "Show Users", "Exit" };
            do
            {
                byte selected = MenuVisual.MenuRun(options, UserDirectory.GetUserName);
                switch (selected)
                {
                    case 0:
                        Login.RunLogin();
                        break;

                    case 1:
                        MessageMenu();
                        break;

                    case 2:
                        if (GotPermission(UserDirectory.GetUserName, 1))
                            UserMenu();
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You do not have permission");
                            Console.Read();
                        }
                        break;

                    case 3:
                        UserDirectory.ShowUser();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        } 

        private static void MessageMenu()
        {
            bool run = true;
            string[] options = new string[] { "Add Message", "See Message(s)", "Back" };
            do
            {
                byte selected = MenuVisual.MenuRun(options, UserDirectory.GetUserName);
                switch (selected)
                {
                    
                    case 0:
                        if (GotPermission(UserDirectory.GetUserName, null))
                            Message.AddMessage();
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You do not have permission");
                            Console.Read();
                        }
                        break;

                    case 1:
                        Message.SeeMessage();
                        break;

                    case 2:
                        run = false;
                        break;
                }
            } while (run);
        }

        private static void UserMenu()
        {
            bool run = true;
            string[] options = new string[] {"Update Message", "Remove Message", "Update User", "Remove User", "Show Users", "Back" };
            do
            {
                byte selected = MenuVisual.MenuRun(options, UserDirectory.GetUserName);
                switch (selected)
                {
                    case 0:
                        Message.UpdateMessage();
                        break;

                    case 1:
                        Message.RemoveMessage();
                        break;

                    case 2:
                        UserDirectory.AlterUser();
                        break;

                    case 3:
                        UserDirectory.RemoveUser();
                        break;

                    case 4:
                        UserDirectory.ShowUser();
                        break;

                    case 5:
                        run = false;
                        break;
                }
            } while (run);
        }

        public static bool GotPermission(string username, int? neededPermissionLevel)
        {
            if(username != "Guest")
            {
                if (neededPermissionLevel == null)
                    return true;
                string[][] userInformation = SQLControl.GetUserInformation($"'{username}'", "Admin_level");
                int? adminLevel;
                if (userInformation[0][0].ToLower() == "null")
                    adminLevel = null;
                else
                    adminLevel = int.Parse(userInformation[0][0]);
                if (adminLevel >= neededPermissionLevel)
                    return true;
            }
            return false;
        }

    }
}
