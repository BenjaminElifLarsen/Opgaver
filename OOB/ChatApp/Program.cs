﻿using System;

namespace ChatApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] options = new string[] { "BENJAMIN-ELIF-L\\MSSQLSERVER02", "localHost,1433", "Write Self" };
            do
            {
                byte answer = MenuVisual.MenuRun(options);
                string selected;
                bool windowLogin = false;
                Console.Clear();
                if (answer == 2)
                    selected = Console.ReadLine();
                else
                {
                    if (answer == 0)
                        windowLogin = true;
                    selected = options[answer];
                }
                SQLControl.SQLConnect("master", selected, windowLogin);
            }while (SQLControl.SQLCreateDatabase());
            Run();
        }

        static public void Run()
        {
            //bool test1 = RegexControl.ContainsDrop("Test");
            //bool test2 = RegexControl.ContainsDrop("Drop");
            //bool test3 = RegexControl.ContainsDrop("drop");
            //bool test4 = RegexControl.ContainsDrop("Drop Table");
            //bool test5 = RegexControl.ContainsDrop("where Username = 5; Drop Database");
            //bool test1 = RegexControl.ContainsForbiddenSigns(".,");
            //bool test2 = RegexControl.ContainsForbiddenSigns("123");
            //bool test3 = RegexControl.ContainsForbiddenSigns("ø");
            //bool test4 = RegexControl.ContainsForbiddenSigns("123 ø ");
            //bool test5 = RegexControl.ContainsForbiddenSigns(" Æble ");
            //bool test1 = RegexControl.ContainsSingleQuouteMark("\'");
            //bool test2 = RegexControl.ContainsSingleQuouteMark("\' \'");
            //bool test3 = RegexControl.ContainsSingleQuouteMark("\'\'");
            //bool test4 = RegexControl.ContainsSingleQuouteMark("d \' d \' d");
            //bool test5 = RegexControl.ContainsSingleQuouteMark("\' \' \'");

            Menu.MainMenu();
        }
    }
}
