﻿using System;

namespace ChatApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLControl.SQLConnect();
            SQLControl.SQLCreateDatabase();
            Run();
        }

        static public void Run()
        {
            string[] options = new string[] { "Login", "Add Message","Update Message", "Remove Message", "See Message(s)","Update User", "Remove User", "Show Users", "Exit" };
            do
            {
                byte selected = Menu.MenuRun(options, UserDirectory.GetUserName);
                switch (selected)
                {
                    case 0:
                        Login.RunLogin();
                    break;
                    
                    case 1:
                        Message.AddMessage();
                    break;

                    case 2:
                        Message.UpdateMessage();
                    break;

                    case 3:
                        Message.RemoveMessage();
                    break;

                    case 4:
                        Message.SeeMessage();
                    break;

                    case 5:
                        UserDirectory.AlterUser();
                        break;

                    case 6:
                        UserDirectory.RemoveUser();
                        break;

                    case 7:
                        UserDirectory.ShowUser();
                        break;

                    case 8:
                        Environment.Exit(0);
                    break;
                }
            } while (true);
        }
    }
}
