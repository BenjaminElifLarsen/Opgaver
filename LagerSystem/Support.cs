using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    /// <summary>
    /// Support class that contains DeepCopy and checking if an ID is unique or not //rewrite later on
    /// </summary>
    public static class Support
    {
        public static List<T> DeepCopy<T>(List<T> wares) 
        {
            List<T> newList = new List<T>();
            for (int i = 0; i < wares.Count; i++)
                newList.Add(wares[i]);
            return newList;
        }

        /// <summary>
        /// Checks if <paramref name="IDToCheck"/> is already in use. Returns false if it does else false.
        /// </summary>
        /// <param name="IDToCheck">The ID to check against other wares' ID.</param>
        /// <returns></returns>
        public static bool UniqueID(string IDToCheck)
        {
            List<string[]> information = WareInformation.GetWareInformation();
            foreach (string[] specificWare in information)
            {
                if (specificWare[1] == IDToCheck)
                    return false;
            }
            return true;
        }

        public static void WaitOnKeyInput()
        {
            Console.ReadKey(true);
            BufferFlush();
        }

        private static void BufferFlush()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public static void ActiveCursor()
        {
            Console.CursorVisible = true;
        }

        public static void DeactiveCursor()
        {
            Console.CursorVisible = false;
        }

        public static uint EnterAmount(string message)
        {
            uint value;
            string valueString;
            Console.Clear();
            Console.WriteLine(message);
            ActiveCursor();
            do
            {
                valueString = Console.ReadLine();
            } while (!uint.TryParse(valueString, out value));
            DeactiveCursor();
            return value;
        }

        public static string CollectString(string message)
        {
            string name;
            Console.Clear();
            Console.WriteLine(message);
            ActiveCursor();
            do
            {
                name = Console.ReadLine().Trim();
            } while (name == null || name == "");
            DeactiveCursor();
            return name;
        }

    }
}
