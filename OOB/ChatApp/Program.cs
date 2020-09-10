using System;

namespace ChatApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLControl.SQLConnect("master");
            SQLControl.SQLCreateDatabase();
            Run();
        }

        static public void Run()
        {
            Menu.MainMenu();
        }
    }
}
