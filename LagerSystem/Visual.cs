﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem
{
    public static class Visual
    {

        /// <summary>
        /// Runs the menu and retuns the selected entry point of <paramref name="options"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static byte MenuRun(string[] options)
        {
            byte hoveredOver = 0;
            bool selected = false;
            Support.DeactiveCursor();
            do
            {
                MenuDisplay(options, hoveredOver);
                hoveredOver = MenuSelection(ref selected, options.Length, hoveredOver);
            } while (!selected);
            Support.ActiveCursor();
            return hoveredOver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected"></param>
        /// <returns>Returns the currently hovered over array position. Comnbined with the ref parameter <c>selected</c> to check if enter key has been pressed. </returns>
        private static byte MenuSelection(ref bool selected, int optionAmount, byte currentHoveredOver = 0)
        {
            while (!Console.KeyAvailable) ;
            ConsoleKey key = Console.ReadKey(true).Key;
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
        private static void MenuDisplay(string[] options, byte currentHoveredOver = 0) 
        {
            Console.Clear();
            for (int n = 0; n < options.Length; n++)
                if (n == currentHoveredOver)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(options[n]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    Console.WriteLine(options[n]);
        }

        public static void WareDisplay(List<string[]> information)
        {
            Console.Clear();
            Support.DeactiveCursor();

            string[] titles = new string[] { "Name", "ID", "Amount", "Type" };
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
            int yLocation = 0;
            Console.WriteLine(Pad(increasement - titles[titles.Length-1].Length-2,' ') + "|" + Environment.NewLine + underline);
            for(int n = 0; n < information.Count; n++)
            {
                string[] wareInfo = information[n];
                string wareInformation = ""; //wareInfo[0] + Pad(xLocation[0] - wareInfo[0].Length, addToo: "|") + wareInfo[1] + Pad(xLocation[1] - wareInfo[1].Length, addToo: "|") + 
                                             //wareInfo[3] + Pad(xLocation[2] - wareInfo[2].Length, addToo: "|") + wareInfo[2] + Pad(xLocation[3] - wareInfo[3].Length, addToo: "|");
                for (int m = 0; m < wareInfo.Length; m++)
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

        public static void SetScreenSize(int x, int y)
        {
            Console.WindowWidth = x;
            Console.WindowHeight = y;
        }

    }
}
