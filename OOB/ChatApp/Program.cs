using System;

namespace ChatApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        static public void Run()
        {
            string[] options = new string[] { "Add Message", "Remove Message", "See Message(s)", "Exit" };
            do
            {
                byte selected = Menu.MenuRun(options);
                switch (selected)
                {
                    case 0:
                        Message.AddMessage();
                    break;

                    case 1:
                        Message.RemoveMessage();
                    break;

                    case 2:
                        Message.SeeMessage();
                    break;

                    case 3:
                        Environment.Exit(0);
                    break;
                }
            } while (true);
        }
    }
}
