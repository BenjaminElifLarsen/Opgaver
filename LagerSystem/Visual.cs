using System;
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

            string[] titles = new string[] { "Name", "ID", "Type", "Amount" };
            int[] xLocation = new int[] {20, 16, 10, 10 };
            int totalLength = 0;
            foreach (int length in xLocation)
                totalLength += length;
            foreach (string title in titles)
                totalLength += title.Length + 2;
            for(int n = 0; n < titles.Length; n++)
            {
                Console.Write(titles[n] + " ".PadLeft(xLocation[n]) + "| ");
            }
            string underline = Pad(totalLength, '-');
            int yLocation = 0;
            Console.CursorLeft = 0;
            for(int n = 0; n < information.Count; n++)
            {
                string[] wareInfo = information[n];
                string wareInformation = wareInfo[0] + Pad(xLocation[0] - 1, addToo: "|") + wareInfo[1] + Pad(xLocation[1] - 1, addToo: "|") + 
                    wareInfo[3] + Pad(xLocation[2] - 1, addToo: "|") + wareInfo[2] + Pad(xLocation[3] - 1, addToo: "|");
                Console.CursorTop = ++yLocation;
                Console.WriteLine(underline + Environment.NewLine + wareInformation);   
            }
            Console.CursorTop = ++yLocation;
            Console.WriteLine(underline);
            Support.ActiveCursor();

            string Pad(int value, char padding = ' ', string addToo = "")
            {
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
