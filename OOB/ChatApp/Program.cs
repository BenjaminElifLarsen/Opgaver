using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatApp
{
    class Program
    {
        static void Main(string[] args) //docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123." -p 1435:1433 --name ChatSQL2 -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04
        {
            //TcpListener test = new TcpListener(8081); //it all works for Simon, so it might be EndPoint Security that is causing a problem since it has run out. 
            //test.Start();
            //var v = test.LocalEndpoint;
            //var test2 = Dns.GetHostAddresses(Dns.GetHostName());
            if(args.Length > 0)
            {
                bool windowLogin = args[0] == "BENJAMIN-ELIF-L\\MSSQLSERVER02";
                SQLControl.SQLConnect("master", args[0], windowLogin);
                RequestHandler requestHandler = new RequestHandler();
                if (args.Length == 2)
                    requestHandler.SetHost = new string[] { args[1] };
                else
                    requestHandler.SetHost = new string[] { "http://localHost:80/" };

                requestHandler.Start();
            }
            else
            {
                string[] options = new string[] { "DESKTOP-OF6BBIM"/*"BENJAMIN-ELIF-L\\MSSQLSERVER02"*/, "localHost,1435", "Write Self" };
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
                } while (!SQLControl.SQLCreateDatabase());
                Run();
            }
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
