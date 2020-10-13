using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// Support class. Contains functions designed to be used by other classes.
    /// </summary>
    public static class Support
    {
        private static ConsoleKey key;

        static Support()
        {
            Publisher.PubKey.RaiseKeyPressEvent += KeyEvnetHandler;
        }

        /// <summary>
        /// Deep copy for lists.
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

        /// <summary>
        /// Deep copy for arrays.
        /// </summary>
        /// <typeparam name="T">Generic data type.</typeparam>
        /// <param name="wares">The array to do deep copy of.</param>
        /// <returns>Returns an identical version of <paramref name="wares"/> but referencing to different memory.</returns>
        public static T[] DeepCopy<T>(T[] wares)
        {
            T[] newArray = new T[wares.Length];
            for (int i = 0; i < wares.Length; i++)
                newArray[i] = wares[i];
            return newArray;
        }

        /// <summary>
        /// Checks if <paramref name="IDToCheck"/> is already in use. Returns false if it does, else true.
        /// </summary>
        /// <param name="IDToCheck">The ID to check against other wares' ID.</param>
        /// <returns>Returns false if <paramref name="IDToCheck"/> is not unique else true.</returns>
        public static bool UniqueID(string IDToCheck, bool sql = false)
        {
            if (!sql) { 
                List<string[]> information = WareInformation.GetWareInformation();
                foreach (string[] specificWare in information)
                {
                    if (specificWare[1] == IDToCheck)
                        return false;
                }
            }
            else
            {
                List<List<string>> information = SQLCode.SQLControl.GetValuesAllWare(new string[] {"id" });
                foreach (List<string> list in information)
                    foreach (string str in list)
                        if (str == IDToCheck)
                            return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if <paramref name="IDToCheck"/> exist and returns true if it does. 
        /// </summary>
        /// <param name="IDToCheck">The ID to check if its exist.</param>
        /// <returns>Returns true if ID exist, else false.</returns>
        public static bool IDExist(string IDToCheck, bool sql = false)
        {
            return !UniqueID(IDToCheck, sql);
        }

        /// <summary>
        /// Waits on a key is pressed.
        /// </summary>
        public static void WaitOnKeyInput()
        {
            key = new ConsoleKey();
            while (key == new ConsoleKey()) ;
            key = new ConsoleKey();
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
        /// Removes any spaces between words for the ware type. 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string RemoveSpace(string type)
        {
            if (type.Split(' ').Length != 1) 
            {
                string[] split = type.Split(' ');
                type = "";
                foreach (string typing in split)
                    type += typing;
            }
            return type;
        }

        /// <summary>
        /// Ask the user to enter a value and returns it if it is not null or empty.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int CollectValue(string message)
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
        /// Ask the user to enter a string and returns it if it is not null or empty.
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

        /// <summary>
        /// Asks if an user is sure about their choice. If yes it returns true, else false.
        /// </summary>
        /// <returns>Returns true if the user acknowledge the confirmation, else false.</returns>
        public static bool Confirmation() 
        {
            string message = "Are you sure?";
            byte response = Visual.MenuRun(new string[] { "Yes", "No" }, message);
            return response == 0;
        }

        /// <summary>
        /// Functions that is subscribed to the KeyEvent.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>
        private static void KeyEvnetHandler(object sender, ControlEvents.KeyEventArgs e)
        {
            key = e.Key;
        }

        /// <summary>
        /// Returns the default value of the ValueType in <paramref name="type"/>, else null. The return type will be packed into dynamic
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static dynamic GetDefaultValueFromValueType(string type) //the unit testing indicates this is a fairly slow function. Around 62 ms for 4 calls
        {
            Type typeCheck = Type.GetType("System." + type);
            if (typeCheck.IsValueType == false)
                return null;
            if (Nullable.GetUnderlyingType(Type.GetType("System."+type)) != null)
                return null;

            return (type.ToLower()) switch
            {
                "double" => default(Double),
                "single" => default(Single),
                "int32" => default(Int32),
                "byte" => default(Byte),
                "int16" => default(Int16),
                "int64" => default(Int64),
                "uint32" => default(UInt32),
                "sbyte" => default(SByte),
                "uint16" => default(UInt16),
                "uint64" => default(UInt64),
                _ => null,
            };
        }

        /// <summary>
        /// Replaces the keyinputs with '*'s on the console.  
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string HiddenText(string title = null)
        {
            List<char> text = new List<char>(); ;
            ConsoleKeyInfo keyPressed;
            if (title != null)
                Console.WriteLine(title);
            ActiveCursor();
            do
            {
                keyPressed = Console.ReadKey(true);
                if (keyPressed.Key != ConsoleKey.Backspace)
                {
                    if (keyPressed.Key != ConsoleKey.Enter)
                    {
                        text.Add(keyPressed.KeyChar);
                        Console.Write('*');
                    }
                }
                else
                    if (Console.CursorLeft != 0)
                {
                    Console.CursorLeft -= 1;
                    Console.Write(' ');
                    Console.CursorLeft -= 1;
                    text.RemoveAt(text.Count - 1);
                }
            } while (keyPressed.Key != ConsoleKey.Enter);
            DeactiveCursor();
            return new string(text.ToArray());
        }

        /// <summary>
        /// Generic method to convert a string to a value type. Conversion for nullables does not work if the string contains a non-null value, rather the underlying type is returned. 
        /// </summary>
        /// <param name="primaryParameters"></param>
        /// <param name="secundaryParameters"></param>
        /// <returns></returns>
        private static t EnterExtraInformation<t>(string information)
        {
            Console.Clear();
            Console.WriteLine("Please Enter {0}", information);
            string value = Console.ReadLine();
            try
            {
                return (t)Convert.ChangeType(value, typeof(t));
            }
            catch
            {
                if (Nullable.GetUnderlyingType(typeof(t)) != null)
                {
                    try
                    {
                        return (t)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(t)));
                    }
                    catch
                    {
                        throw new InvalidCastException();
                    }
                }
                else if (typeof(t).Name.Contains("[]"))
                {
                    Type actualType = Type.GetType(typeof(t).FullName.Remove(typeof(t).FullName.Length - 2, 2));
                    return (t)Convert.ChangeType(value, actualType);
                }
                else
                    throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Default error handling regarding reporting and informating the user.
        /// 1) Writes the error. <paramref name="e"/>, to the report file.
        /// 2) Writes out <paramref name="message"/> to the console.
        /// 3) Waits on user input.
        /// </summary>
        /// <param name="e">The exception that was casted.</param>
        /// <param name="message">The message to display for the user.</param>
        public static void ErrorHandling(Exception e, string message)
        {
            Reporter.Report(e);
            Console.Clear();
            Console.WriteLine(message);
            WaitOnKeyInput();
        }
    }

}
