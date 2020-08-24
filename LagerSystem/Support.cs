using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    /// <summary>
    /// Support class ...
    /// </summary>
    public static class Support
    {
        /// <summary>
        /// Deep copy for lists
        /// </summary>
        /// <typeparam name="T">Generic data type.</typeparam>
        /// <param name="wares">The list to do deep copy of.</param>
        /// <returns>Returns an identical version of <paramref name="wares"/> but referencing to different memory.</returns>
        public static List<T> DeepCopy<T>(List<T> wares) 
        {
            List<T> newList = new List<T>();
            for (int i = 0; i < wares.Count; i++)
                newList.Add(wares[i]);
            return newList;
        }

        public static T[] DeepCopy<T>(T[] wares)
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// Checks if <paramref name="IDToCheck"/> exist and returns true if it does. 
        /// </summary>
        /// <param name="IDToCheck">The ID to check if its exist.</param>
        /// <returns>Returns true if ID exist, else false.</returns>
        public static bool IDExist(string IDToCheck)
        {
            return !UniqueID(IDToCheck);
        }

        /// <summary>
        /// Waits on a key is pressed.
        /// </summary>
        public static void WaitOnKeyInput()
        {
            Console.ReadKey(true);
            BufferFlush();
        }

        /// <summary>
        /// Flushes the Console.Key buffer.
        /// </summary>
        public static void BufferFlush()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Activate the cursor visibility.
        /// </summary>
        public static void ActiveCursor()
        {
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Deactivate the cursor visibility.
        /// </summary>
        public static void DeactiveCursor()
        {
            Console.CursorVisible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int EnterAmount(string message)
        {
            int value;
            string valueString;
            Console.Clear();
            Console.WriteLine(message);
            ActiveCursor();
            do
            {
                valueString = Console.ReadLine();
            } while (!int.TryParse(valueString, out value));
            DeactiveCursor();
            return value;
        }

        /// <summary>
        /// Aks the user to collect a string and returns it if it is not null or empty.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
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
