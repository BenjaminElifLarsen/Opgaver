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


        public static void RunInput()
        {
            Thread inputThread = new Thread(InputRun);
        }

        static private void InputRun()
        {
            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    Publisher.PubKey.PressKey(key);
                    Support.BufferFlush();
                }

            } while (true);


        }

    }
}
