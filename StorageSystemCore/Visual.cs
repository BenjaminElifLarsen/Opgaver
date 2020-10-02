using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    public static class Visual
    {

        private static ConsoleKey key;

        static Visual()
        {
            Publisher.PubKey.RaiseKeyPressEvent += KeyEvnetHandler;
        }

        /// <summary>
        /// Runs the menu and retuns the selected entry point of <paramref name="options"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static byte MenuRun(string[] options, string title = null)
        {
            byte hoveredOver = 0;
            byte oldHoveredOver = 0;
            bool selected;
            Support.DeactiveCursor();
            MenuDisplay(options, hoveredOver, title);
            do
            {
                hoveredOver = MenuSelection(out selected, options.Length, hoveredOver);
                MenuDisplayUpdater(options, ref oldHoveredOver, hoveredOver);
            } while (!selected);
            Support.ActiveCursor();
            return hoveredOver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected"></param>
        /// <returns>Returns the currently hovered over array position. Comnbined with the ref parameter <c>selected</c> to check if enter key has been pressed. </returns>
        private static byte MenuSelection(out bool selected, int optionAmount, byte currentHoveredOver = 0)
        {
            key = new ConsoleKey();
            while (key == new ConsoleKey()) ;
            Support.BufferFlush();
            if(key == ConsoleKey.Enter)
            {
                selected = true;
                return currentHoveredOver;
            }
            selected = false;
            if (key == ConsoleKey.DownArrow && currentHoveredOver < optionAmount - 1)
                return ++currentHoveredOver;
            else if (key == ConsoleKey.UpArrow && currentHoveredOver > 0)
                return --currentHoveredOver;
            else
                return currentHoveredOver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="currentHoveredOver"></param>
        private static void MenuDisplay(string[] options, byte currentHoveredOver = 0, string title = null) 
        {
            Console.Clear();
            if (title != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(title);
            }
            Console.CursorTop = 1;
            for (int n = 0; n < options.Length; n++)
                if (n == currentHoveredOver)
                {
                    Console.CursorLeft = 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(options[n]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { 
                    Console.CursorLeft = 1;
                    Console.WriteLine(options[n]);
                }
        }

        /// <summary>
        /// Ensures only the part of the menu that should be changed is updated. 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="oldHoveredOver"></param>
        /// <param name="currentHoveredOver"></param>
        private static void MenuDisplayUpdater(string[] options, ref byte oldHoveredOver, byte currentHoveredOver = 0)
        {
            Console.CursorTop = 1; 
            if(oldHoveredOver != currentHoveredOver)
            {
                Paint(2, currentHoveredOver, ConsoleColor.Red, options[currentHoveredOver]);
                Paint(1, oldHoveredOver, ConsoleColor.White, options[oldHoveredOver]);
                oldHoveredOver = currentHoveredOver;
            }

            void Paint(byte indent, byte y, ConsoleColor colour, string text)
            {
                byte length = (byte)text.Length;
                Console.CursorTop = y + 1;
                Console.Write(" ".PadLeft(length + 2));
                Console.ForegroundColor = colour;
                Console.CursorLeft = indent;
                Console.WriteLine(text);
            }
        }

        /// <summary>
        /// Displays 
        /// </summary>
        /// <param name="information"></param>
        public static void WareDisplay(List<string[]> information) //have an overload that takes the List<Dictionary<string,object>>
        {
            Console.Clear();
            Support.DeactiveCursor();

            string[] titles = new string[] { "Name", "ID", "Amount", "Type" }; //can use reflection to find the specific methods/properties in different classes. Can use strings parameters to display different values using param string
            int[] xLocation = new int[titles.Length];
            byte increasement = 20;
            //int totalLength = xLocation[xLocation.Length-1];
            for (int n = 1; n < xLocation.Length; n++)
                xLocation[n] = increasement * n;
            for(int n = 0; n < titles.Length; n++)
            {
                Console.CursorLeft = xLocation[n];
                Console.Write("| " + titles[n]);
            }
            string underline = "|"; //+ Pad(totalLength, '-');
            foreach (int xloc in xLocation)
                underline += Pad(increasement, '-', "|");
            //int yLocation = 0;
            Console.WriteLine(Pad(increasement - titles[titles.Length-1].Length-2,' ') + "|" + Environment.NewLine + underline);
            for(int n = 0; n < information.Count; n++)
            {
                string[] wareInfo = information[n];
                //string wareInformation = //wareInfo[0] + Pad(xLocation[0] - wareInfo[0].Length, addToo: "|") + wareInfo[1] + Pad(xLocation[1] - wareInfo[1].Length, addToo: "|") + 
                                           //wareInfo[3] + Pad(xLocation[2] - wareInfo[2].Length, addToo: "|") + wareInfo[2] + Pad(xLocation[3] - wareInfo[3].Length, addToo: "|");
                for (int m = 0; m < wareInfo.Length; m++) //make it find the longest word in each catergory and use the length plus something for the placement of | in all lines for that category (have a function for this)
                {
                    Console.CursorLeft = xLocation[m];
                    Console.Write("| " + wareInfo[m]);
                }
                Console.Write(Pad(increasement-wareInfo[wareInfo.Length-1].Length-2)+"|");
                Console.WriteLine(Environment.NewLine + underline);
                //Console.CursorTop += 1;
            }
            //Console.WriteLine(underline);
            Support.ActiveCursor();

            string Pad(int value, char padding = ' ', string addToo = "")
            {
                value = value < 0 ? 0 : value;
                return addToo.PadLeft(value,padding);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="columnAndValues"></param>
        public static void WareDisplay(List<string> columnNames, List<Dictionary<string,object>> columnAndValues)
        {
            Support.DeactiveCursor();
            int[] columnStartLocation = new int[columnNames.Count];
            int[] currentLongestRowValue = new int[columnNames.Count];
            int totalLength = 0;
            string[,] textToDisplay = new string[columnNames.Count, columnAndValues.Count];
            for(int n = 0; n < textToDisplay.GetLength(0); n++)
            {
                for(int m = 0; m < textToDisplay.GetLength(1); m++)
                {
                    
                    if (columnAndValues[m].TryGetValue(columnNames[n], out object value))
                        if(value != null)
                            textToDisplay[n, m] = value.ToString();
                        else
                            textToDisplay[n, m] = "null";
                    else
                        textToDisplay[n, m] = "null";
                    if (textToDisplay[n, m].Length > currentLongestRowValue[n])
                    {
                        if (columnNames[n].Length < textToDisplay[n, m].Length)
                            currentLongestRowValue[n] = textToDisplay[n, m].Length;
                        else
                            currentLongestRowValue[n] = columnNames[n].Length;
                        //totalLength += textToDisplay[n, m].Length + 2;
                        //columnEndLocation[n] = totalLength;
                    }
                }
            }
            for(int n = 0; n < columnStartLocation.Length; n++)
            {
                columnStartLocation[n] = totalLength;
                totalLength += currentLongestRowValue[n] + 2;
                               
            }

            Console.Clear();
            string underscore = "|".PadRight(totalLength, '-');
            Console.CursorTop = 1;
            Console.Write(underscore);
            for (int n = 0; n < textToDisplay.GetLength(0); n++)
            {
                Console.CursorTop = 0;
                Console.CursorLeft = columnStartLocation[n]; //System.ArgumentOutOfRangeException, buffer size in y is to small. Check beforehand if there is a n value that is same or bigger than the buffer size. If true, lower the GetLength to smaller than it
                Console.Write("|"+columnNames[n]);
                for (int m = 0; m < textToDisplay.GetLength(1); m++)
                {
                    Console.CursorLeft = columnStartLocation[n];
                    Console.CursorTop = m + 2;
                    Console.Write("|"+textToDisplay[n, m]);
                    //if (n > 0)
                    //    Console.CursorLeft = columnStartLocation[n - 1];
                    //else
                    //    Console.CursorLeft = 0;

                }
            }
            Support.WaitOnKeyInput();
            Support.ActiveCursor();
        }

        public static void SetScreenSize(int x, int y)
        {
            Console.WindowWidth = x;
            Console.WindowHeight = y;
        }

        private static void KeyEvnetHandler(object sender, ControlEvents.KeyEventArgs e)
        {
            key = e.Key;
        }


        //public static string SelectionRun(List<string> attributes, string title = null)
        //{
        //    byte location = SelectionDisplay(attributes, title);


        //    return SelectionEntering(attributes,location);
        //}

        //private static string SelectionEntering(List<string> attributes, byte writeLocation)
        //{
        //    Console.CursorTop = writeLocation;
        //    Console.WriteLine(){

        //    }
        //}

        //private static byte SelectionDisplay(List<string> attributes, string title)
        //{
        //    if (title != null)
        //        Console.WriteLine(title);
        //    else
        //        Console.CursorTop = 1;
            
        //    foreach (string seAttr in attributes)
        //    {
        //        Console.CursorLeft = 2;
        //        Console.WriteLine(seAttr);
        //    }
        //    return (byte)Console.CursorTop;
        //}

    }
}
