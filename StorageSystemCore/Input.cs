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
        /// Enum that contains the keys that are used in the software. 
        /// </summary>
        public enum Keys
        {
            Enter = 13,
            BackSpace = 8,
            UpArray = 38,
            DownArray = 40
        }

        private static ConsoleKeyInfo key;

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
                        key = KeyInput();
                        BufferFlush();
                    }

                } while (true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Reads and return a key without displaying it.
        /// </summary>
        /// <returns>Reads and return a key.</returns>
        private static ConsoleKeyInfo KeyInput()
        {
            return Console.ReadKey(true);
        }

        public delegate string InputStringDelegate();
        /// <summary>
        /// 
        /// </summary>
        public static InputStringDelegate GetString = getInput;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string getInput()
        {
            return Console.ReadLine();
        }

        public delegate ConsoleKeyInfo InputSingleKeyInfoDelegate();
        /// <summary>
        /// 
        /// </summary>
        public static InputSingleKeyInfoDelegate GetKeyInfo = KeyInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static ConsoleKeyInfo KeyInfo()
        {
            if (key == new ConsoleKeyInfo()) 
                return new ConsoleKeyInfo();
            ConsoleKeyInfo key_ = key;
            key = new ConsoleKeyInfo();
            return key_;
        }

        public delegate ConsoleKey InputSingleKeyDelegate();
        /// <summary>
        /// 
        /// </summary>
        public static InputSingleKeyDelegate InputSingleKey = Key;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static ConsoleKey Key()
        {
            if (key == new ConsoleKeyInfo())
                return new ConsoleKey();
            ConsoleKey key_ = key.Key;
            key = new ConsoleKeyInfo();
            return key_;
        }

        public delegate bool KeyAvaliable();
        public static KeyAvaliable IskeyAvaliable = keyAvaliable;
        private static bool keyAvaliable()
        {
            return Console.KeyAvailable;
        }

        /// <summary>
        /// Flushes the Console.Key buffer.
        /// </summary>
        private static void BufferFlush()
        {
            while (IskeyAvaliable())
                KeyInput();
        }

        public delegate bool KeyCompareDelegate(ConsoleKey pressedKey, Keys key_);
        /// <summary>
        /// 
        /// </summary>
        public static KeyCompareDelegate KeyCompare = KeyComparision;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key_"></param>
        /// <returns></returns>
        private static bool KeyComparision(ConsoleKey pressedKey, Keys key_)
        {
            bool result = (int)key_ == (int)pressedKey;
            //BufferFlush();
            return result;
        }

    }
}
