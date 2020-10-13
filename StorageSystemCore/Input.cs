using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    static public class Input
    {
        /// <summary>
        /// Ensures that the input system is always working by running it on another thread. 
        /// </summary>
        public static void RunInputThread()
        {
            Thread inputThread = new Thread(InputRun);
            inputThread.Name = "Input Thread";
            inputThread.Start();
        }

        /// <summary>
        /// If a key is pressed, activate an event and transmit the key. 
        /// </summary>
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
