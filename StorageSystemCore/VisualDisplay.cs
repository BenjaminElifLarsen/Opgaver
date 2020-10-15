using System;
using System.Collections.Generic;
using System.Text;

namespace StorageSystemCore
{
    public class VisualDisplay 
    {

        public delegate void WriteOutDelegate(string message, bool newLine = false);
        public static WriteOutDelegate writeOut = writeOutMessage;
        private static void writeOutMessage(string message, bool newLine = false)
        {
            Console.Write(message);
            if (newLine)
                Console.WriteLine();
        }

        public delegate void WriteOutDelegateTitle(string message, VisualCalculator.Colours colour, bool newLine = false);
        public static WriteOutDelegateTitle writeOutColour = writeOutMessage;
        private static void writeOutMessage(string message, VisualCalculator.Colours colour, bool newLine = false)
        {
            Console.ForegroundColor = (ConsoleColor)(int)colour;
            Console.Write(message);
            if (newLine)
                Console.WriteLine();
        }

        public delegate void WriteOutDelegateComplex(string message, int x, int y, VisualCalculator.Colours colour1, bool newLine = false);
        public static WriteOutDelegateComplex writeOutComplex = writeOutMessage;

        private static void writeOutMessage(string message, int x, int y, VisualCalculator.Colours colour1, bool newLine = false)
        {
            Console.ForegroundColor = (ConsoleColor)(int)colour1;
            Console.SetCursorPosition(x, y);
            Console.Write(message);
            if (newLine)
                Console.WriteLine();
        }

        public delegate void ClearPartDelegate(byte length, int y);
        public static ClearPartDelegate clearPart = clearPartText;
        private static void clearPartText(byte length, int y)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = y;
            Console.Write(" ".PadLeft(length));
        }

        public delegate void FullClearDelegate();
        public static FullClearDelegate clearFull = fullClear;
        private static void fullClear()
        {
            Console.Clear();
        }

    }
}
