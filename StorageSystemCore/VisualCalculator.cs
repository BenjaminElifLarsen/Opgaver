﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystemCore
{
    /// <summary>
    /// Class that deals with the visual part of the storage system. 
    /// </summary>
    public static class VisualCalculator
    {
        public enum Colours
        {
            Red = 12,
            White = 15
        }

        private static ConsoleKey key;
        private static int optionDisplayLowering = 1; //1 because of the possiblity of tiles
        
        /// <summary>
        /// The basic, static, constructor. Will always have been called before the first run-time call to any of the class's functions. 
        /// </summary>
        static VisualCalculator()
        {
            Publisher.PubKey.RaiseKeyPressEvent += KeyEvnetHandler;
        }

        /// <summary>
        /// Runs the menu and retuns the selected entry point of <paramref name="options"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Returns the number of the selected index.</returns>
        /// <exception cref="NullReferenceException">Thrown when <paramref name="options"/> is null.</exception>
        public static byte MenuRun(string[] options, string title = null)
        {
            if (options == null)
                throw new NullReferenceException();
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
        /// Allows the interaction with the menu.
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
        /// Displays the visual part of the menu.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="currentHoveredOver"></param>
        private static void MenuDisplay(string[] options, byte currentHoveredOver = 0, string title = null) 
        {
            VisualDisplay.ClearFull();
            if (title != null)
            {
                VisualDisplay.WriteMessageColoured(title, Colours.White);
            }
            //Console.CursorTop = 1;
            Colours colour;
            byte indent;
            for (int n = 0; n < options.Length; n++) { 
                if (n == currentHoveredOver)
                {
                    colour = Colours.Red;
                    indent = 2;
                }
                else {
                    colour = Colours.White;
                    indent = 1;
                }
                VisualDisplay.WriteMessageComplex(options[n], indent, n + 1, colour, true);
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
            //Console.CursorTop = 1; 
            
            if(oldHoveredOver != currentHoveredOver)
            {
                Paint(2, currentHoveredOver, Colours.Red, options[currentHoveredOver]);
                Paint(1, oldHoveredOver, Colours.White, options[oldHoveredOver]);
                oldHoveredOver = currentHoveredOver;
            }

            void Paint(byte indent, byte y, Colours colour, string text)
            {
                byte length = (byte)text.Length;
                VisualDisplay.ClearPart((byte)(length+4),y + optionDisplayLowering);
                VisualDisplay.WriteMessageComplex(text, indent, y + optionDisplayLowering, colour);
            }
        }

        /// <summary>
        /// Displays the basic information listen in <paramref name="information"/>.
        /// </summary>
        /// <param name="information">Contains the information to display. Each string[] should be a seperate object</param>
        public static void WareDisplay(List<string[]> information) 
        {
            VisualDisplay.ClearFull();
            Support.DeactiveCursor();
            int y = 0;
            string[] titles = new string[] { "Name", "ID", "Amount", "Type" };
            int[] xLocation = new int[titles.Length];
            byte increasement = 20;
            for (int n = 1; n < xLocation.Length; n++) //calculates the start location of each column
                xLocation[n] = increasement * n;
            for(int n = 0; n < titles.Length; n++) //displays the titles and '|'
            {
                VisualDisplay.WriteMessageComplex("| " + titles[n], xLocation[n], 0, Colours.White);
            }
            y += 2;
            string underline = "|"; 
            foreach (int xloc in xLocation) //calculates the line seperator
                underline += Pad(increasement, '-', "|");
            VisualDisplay.writeOut(Pad(increasement - titles[titles.Length - 1].Length - 2, ' ') + "|" + Environment.NewLine + underline,true);//Console.WriteLine(Pad(increasement - titles[titles.Length-1].Length-2,' ') + "|" + Environment.NewLine + underline);
            for (int n = 0; n < information.Count; n++) //writes out the information of each string array
            {
                string[] wareInfo = information[n];
                for (int m = 0; m < wareInfo.Length; m++) 
                {
                    VisualDisplay.WriteMessageComplex("| " + wareInfo[m], xLocation[m], y, Colours.White);
                }
                y++;
                VisualDisplay.writeOut(Pad(increasement - wareInfo[wareInfo.Length - 1].Length - 2) + "|");;
                VisualDisplay.WriteMessageComplex(underline,0,y++,Colours.White);
            }
            Support.ActiveCursor();

            string Pad(int value, char padding = ' ', string addToo = "") 
            {
                value = value < 0 ? 0 : value;
                return addToo.PadLeft(value,padding);
            }
        }

        /// <summary>
        /// Displays the information, stored in <paramref name="columnAndValues"/>, of all wares. The column titles comes from <paramref name="columnNames"/>.
        /// </summary>
        /// <param name="columnNames">The name of the column titles.</param>
        /// <param name="columnAndValues">The name and values of each <paramref name="columnNames"/> from all wares. </param>
        public static void WareDisplay(List<string> columnNames, List<Dictionary<string,object>> columnAndValues)
        {
            Support.DeactiveCursor();
            int[] columnStartLocation = new int[columnNames.Count];
            int[] currentLongestRowValue = new int[columnNames.Count];
            int totalLength = 0;
            string[,] textToDisplay = new string[columnNames.Count, columnAndValues.Count];
            for(int n = 0; n < textToDisplay.GetLength(0); n++) //fills out the 2D array with the inforamtion to display
            {
                for(int m = 0; m < textToDisplay.GetLength(1); m++)
                {

                    if (columnAndValues[m].TryGetValue(columnNames[n], out object value))
                        if (value != null)
                        {
                            if(value.GetType().BaseType.Name == "Array") //if-statement code will convert all values in a array to a string
                            {
                                foreach(object element in value as IEnumerable)
                                {
                                    textToDisplay[n, m] += element + " ";
                                }
                            }
                            else
                                textToDisplay[n, m] = value.ToString();
                        }
                        else
                            textToDisplay[n, m] = "null";
                    else
                        textToDisplay[n, m] = "null";
                    if (textToDisplay[n, m].Length > currentLongestRowValue[n]) //ensures the longest length of information of all rows in each column is known
                    {
                        if (columnNames[n].Length < textToDisplay[n, m].Length)
                            currentLongestRowValue[n] = textToDisplay[n, m].Length;
                        else
                            currentLongestRowValue[n] = columnNames[n].Length;
                    }
                }
            }
            for(int n = 0; n < columnStartLocation.Length; n++) //adds some more length to each column length 
            {
                columnStartLocation[n] = totalLength;
                totalLength += currentLongestRowValue[n] + 2;              
            }

            RunNonBasicDisplayInformation(textToDisplay, columnNames.ToArray(), columnStartLocation, totalLength);
        }

        /// <summary>
        /// Displays the information, stored in <paramref name="values"/>, of all wares. The column titles comes from <paramref name="columnNames"/>. Designed for SQL
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="values"></param>
        public static void WareDisplay(string[] columnNames, List<List<string>> values) //when this is working, find the parts that can moved into functions and shared with the one above
        {
            Support.DeactiveCursor();
            int[] columnStartLocation = new int[columnNames.Length];
            int[] currentLongestRowValue = new int[columnNames.Length];
            int totalLength = 0;
            string[,] textToDisplay = new string[columnNames.Length, values.Count];
            for (int n = 0; n < textToDisplay.GetLength(0); n++) //fills out the 2D array with the inforamtion to display
            {
                for (int m = 0; m < textToDisplay.GetLength(1); m++)
                {
                    textToDisplay[n, m] = values[m][n] == "" ? "null" : values[m][n];
                    if (textToDisplay[n, m].Length > currentLongestRowValue[n]) //ensures the longest length of information of all rows in each column is known
                    {
                        if (columnNames[n].Length < textToDisplay[n, m].Length)
                            currentLongestRowValue[n] = textToDisplay[n, m].Length;
                        else
                            currentLongestRowValue[n] = columnNames[n].Length;
                    }
                }
            }
            for (int n = 0; n < columnStartLocation.Length; n++) //adds some more length to each column length 
            {
                columnStartLocation[n] = totalLength;
                totalLength += currentLongestRowValue[n] + 2;

            }
            RunNonBasicDisplayInformation(textToDisplay, columnNames, columnStartLocation, totalLength);
        }

        private static void RunNonBasicDisplayInformation(string[,] textToDisplay, string[] columnNames, int[] columnStartLocation, int totalLength) //rename
        {
            byte maxLength = (byte)textToDisplay.GetLength(0); 
            while (columnStartLocation[maxLength - 1] > Console.BufferWidth) //delegate this or something like that
                maxLength--;

            VisualDisplay.ClearFull();
            string underscore = "|".PadRight(totalLength, '-');
            int y = 1;
            VisualDisplay.WriteMessageComplex(underscore, 0, y, Colours.White);
            for (int n = 0; n < maxLength; n++) //displays all information in the 2D array textToDisplay
            {
                y = 0;
                VisualDisplay.WriteMessageComplex("|" + columnNames[n], columnStartLocation[n], y, Colours.White);
                for (int m = 0; m < textToDisplay.GetLength(1); m++)
                {
                    VisualDisplay.WriteMessageComplex("|" + textToDisplay[n, m], columnStartLocation[n], m + 2, Colours.White);
                }
            }

            Support.WaitOnKeyInput();
            Support.ActiveCursor();
        }

        /// <summary>
        /// Function subscribed to the key event. When the event is called it will add what key has been pressed to <c>key</c>. Needed to interact with the class and its functions. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void KeyEvnetHandler(object sender, ControlEvents.KeyEventArgs e)
        {
            key = e.Key;
        }

    }
}
