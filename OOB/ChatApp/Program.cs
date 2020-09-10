﻿using System;

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
            //bool test1 = RegexControl.ContainsDrop("Test");
            //bool test2 = RegexControl.ContainsDrop("Drop");
            //bool test3 = RegexControl.ContainsDrop("drop");
            //bool test4 = RegexControl.ContainsDrop("Drop Table");
            //bool test5 = RegexControl.ContainsDrop("where Username = 5; Drop Database");

            Menu.MainMenu();
        }
    }
}
