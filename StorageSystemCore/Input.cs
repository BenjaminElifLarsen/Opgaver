using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LagerSystem
{
    static public class Input
    {

        public static void RunInputThread()
        {
            Thread inputThread = new Thread(InputRun);
            inputThread.Name = "Input Thread";
            inputThread.Start();
        }

        static private void InputRun()
        {
            do
            {
                if (Console.KeyAvailable) //something somewhere causes a minor problem, confermation message at remove ware and message if a ware does not exist and trying to remove units from it
                {//requires two key entries rather than 1, this has not happened while debuggering. 
                    ConsoleKey key = Console.ReadKey(true).Key;
                    Publisher.PubKey.PressKey(key);
                    Support.BufferFlush();
                }

            } while (true);


        }

    }
}
