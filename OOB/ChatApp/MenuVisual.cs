using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    static class MenuVisual
    {

        static public byte MenuRun(string[] options, string title = null)
        {
            Console.CursorVisible = false;
            byte hoveredOver = 0;
            byte oldHoveredOver = 0;
            bool selected;
            MenuDisplay(options, hoveredOver, title);
            do
            {
                hoveredOver = MenuSelection(out selected, options.Length, hoveredOver);
                MenuDisplayUpdater(options, ref oldHoveredOver, hoveredOver);
            } while (!selected);
            Console.CursorVisible = true;
            while (Console.KeyAvailable) { Console.ReadKey(true); }
            return hoveredOver;
        }

        static private byte MenuSelection(out bool selected, int optionAmount, byte currentHoveredOver = 0)
        {
            while (!Console.KeyAvailable) ;
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
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
                else
                {
                    Console.CursorLeft = 1;
                    Console.WriteLine(options[n]);
                }
        }

        private static void MenuDisplayUpdater(string[] options, ref byte oldHoveredOver, byte currentHoveredOver = 0)
        {
            Console.CursorTop = 1;
            if (oldHoveredOver != currentHoveredOver)
            {
                Paint(2, currentHoveredOver, ConsoleColor.Red, options[currentHoveredOver]);
                Paint(1, oldHoveredOver, ConsoleColor.White, options[oldHoveredOver]);
                oldHoveredOver = currentHoveredOver;
            }

            static void Paint(byte indent, byte y, ConsoleColor colour, string text)
            {
                byte length = (byte)text.Length;
                Console.CursorTop = y + 1;
                Console.Write(" ".PadLeft(length + 2));
                Console.ForegroundColor = colour;
                Console.CursorLeft = indent;
                Console.WriteLine(text);
            }
        }
    }
}
