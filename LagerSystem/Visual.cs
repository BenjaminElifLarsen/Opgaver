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
            do
            {
                MenuDisplay(options, hoveredOver);
                hoveredOver = MenuSelection(ref selected, options.Length, hoveredOver);
            } while (!selected);
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

        public static void WareDisplay(List<string[]> Information)
        {

        }

        public static void SetScreenSize(int x, int y)
        {
            Console.WindowWidth = x;
            Console.WindowHeight = y;
        }

    }
}
