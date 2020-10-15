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
        /// <exception cref="ThreadStateException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public static void RunInputThread()
        {
            Thread inputThread = new Thread(InputRun);
            inputThread.Name = "Input Thread";
            try
            {
                inputThread.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// If a key is pressed, activate an event and transmit the key. 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        static private void InputRun()
        {
            try
            {
                do
                {
                    if (Console.KeyAvailable)
                    {
                        //ConsoleKey key = Console.ReadKey(true).Key;
                        ConsoleKey key = GetKey().Key;
                        Publisher.PubKey.PressKey(key);
                        Support.BufferFlush();
                    }

                } while (true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public delegate string InputStringDelegate();
        public static InputStringDelegate GetString = getInput;
        private static string getInput()
        {
            return Console.ReadLine();
        }

        public delegate ConsoleKeyInfo InputSingleKeyDelegate();
        public static InputSingleKeyDelegate GetKey = getKey;
        private static ConsoleKeyInfo getKey()
        {
            return Console.ReadKey(true);
        }

        public delegate bool KeyAvaliable();
        public static KeyAvaliable IskeyAvaliable = keyAvaliable;
        private static bool keyAvaliable()
        {
            return Console.KeyAvailable;
        }

    }
}
