using System;

namespace ChatApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLControl.SQLConnect();
            Run();
        }

        static public void Run()
        {
            string[] options = new string[] { "Login", "Add Message","Update Message", "Remove Message", "See Message(s)", "Exit" };
            do
            {
                byte selected = Menu.MenuRun(options);
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
                        Environment.Exit(0);
                    break;
                }
            } while (true);
        }
    }
}
