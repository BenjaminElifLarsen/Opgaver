using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ChatApp
{
    static class Menu
    {
        public static void MainMenu()
        {
            bool webThreadRunning = false;
            string[] options = new string[] { "Login", "Public Control", "Admin Control", "Show Users", "Exit", "Test" };
            do
            {
                byte selected = MenuVisual.MenuRun(options, UserDirectory.User.Name);
                switch (selected)
                {
                    case 0:
                        Login.RunLogin();
                        break;

                    case 1:
                        MessageMenu();
                        break;

                    case 2:
                        if (GotPermission(UserDirectory.User.Name, 1))
                            UserMenu();
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You do not have permission");
                            Console.Read();
                        }
                        break;

                    case 3:
                        UserDirectory.ShowUser(9);
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    case 5:
                        if (!webThreadRunning) { 
                            RequestHandler requestHandler = new RequestHandler();
                            Thread webThread = new Thread(new ThreadStart(requestHandler.Start));
                            if(MenuVisual.MenuRun(new string[] { "Yes", "No" }, "Enter IP-address for port 80?") == 0)
                            {
                                Console.Write("http://");
                                string hostPart = Console.ReadLine();
                                requestHandler.SetHost = new string[] { "http://" + hostPart + ":80/" };
                            }
                            else
                                requestHandler.SetHost = new string[] { "http://localhost:8080/" };
                            webThread.Name = "Web RequestHandler Thread";
                            webThread.Start();
                            webThreadRunning = true;
                        }
                        //Webpage.GetHTML(SQLControl.SQLGetMessages());
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
                byte selected = MenuVisual.MenuRun(options, UserDirectory.User.Name);
                switch (selected)
                {
                    
                    case 0:
                        if (GotPermission(UserDirectory.User.Name, null))
                            Messages.AddMessage();
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You do not have permission");
                            Console.Read();
                        }
                        break;

                    case 1:
                        Messages.SeeMessage();
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
                byte selected = MenuVisual.MenuRun(options, UserDirectory.User.Name);
                switch (selected)
                {
                    case 0:
                        Messages.UpdateMessage();
                        break;

                    case 1:
                        Messages.RemoveMessage();
                        break;

                    case 2:
                        UserDirectory.AlterUser();
                        break;

                    case 3:
                        UserDirectory.RemoveUser();
                        break;

                    case 4:
                        UserDirectory.ShowUser(10);
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
