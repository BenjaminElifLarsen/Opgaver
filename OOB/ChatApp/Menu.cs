using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ChatApp
{
    static class Menu
    {
        private static bool webThreadRunning = false;
        public static bool SetWebThreadRunning { set => webThreadRunning = value; }
        public static void MainMenu()
        {
            string[] options = new string[] { "Login", "Public Control", "Admin Control", "Show Users", "Exit", "Test" };
            do
            {
                Reporter.Log($"Entered main menu");
                byte selected = MenuVisual.MenuRun(options, UserDirectory.User.Name);
                switch (selected)
                {
                    case 0:
                        Reporter.Log($"Entered login menu");
                        Login.RunLogin();
                        break;

                    case 1:
                        Reporter.Log($"Entered message menu");
                        MessageMenu();
                        break;

                    case 2:
                        Reporter.Log($"Entered Admin menu");
                        if (GotPermission(UserDirectory.User.Name, 1))
                            UserMenu();
                        else
                        {
                            Console.Clear();
                            Reporter.Log(UserDirectory.User.Name + " had not permission for admin menu");
                            Console.WriteLine("You do not have permission");
                            Console.Read();
                        }
                        break;

                    case 3:
                        Reporter.Log($"Entered show users with admin level less than 9");
                        UserDirectory.ShowUser(9);
                        break;

                    case 4:
                        Reporter.Log("Program Shutdown");
                        Environment.Exit(0);
                        break;

                    case 5:
                        Reporter.Log($"Entered server menu");
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
                                requestHandler.SetHost = new string[] { "http://localhost:80/" };
                            webThread.Name = "Web RequestHandler Thread";
                            try 
                            { 
                                webThread.Start();
                                webThreadRunning = true;
                            }
                            catch (Exception e)
                            {
                                Console.Clear();
                                Reporter.Report(e);
                                Console.WriteLine("Could not start up the server. For error see " + Reporter.ErrorLocation);
                                Console.Read();
                            }
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
                            Reporter.Log("Guest tried to enter a message");
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
