using System;
using System.Collections.Generic;
using System.Text;

namespace StorageSystemCore
{
    public class VisualPublisher
    {

        private delegate void WriteOutDelegate(string message, bool newLine = false);
        private WriteOutDelegate writeOut = writeOutMessage;
        private static void writeOutMessage(string message, bool newLine = false)
        {
            Console.Write(message);
            if (newLine)
                Console.WriteLine();
        }
        public void WriteOut(string message, bool newLine = false)
        {
            writeOut(message, newLine);
        }

        private delegate void WriteOutDelegateTitle(string message, Visual.Colours colour, bool newLine = false);
        private WriteOutDelegate writeOutTitle = writeOutMessage;
        private static void writeOutMessage(string message, Visual.Colours colour, bool newLine = false)
        {
            Console.ForegroundColor = (ConsoleColor)(int)colour;
            Console.Write(message);
            if (newLine)
                Console.WriteLine();
        }
        public void WriteOut(string message, Visual.Colours colour, bool newLine = false)
        {
            writeOutMessage(message, colour, newLine);
        }

        private delegate void WriteOutDelegateComplex(string message, int x, int y, Visual.Colours colour1, bool newLine = false);
        private WriteOutDelegateComplex writeOutComplex = writeOutMessage;

        private static void writeOutMessage(string message, int x, int y, Visual.Colours colour1, bool newLine = false)
        {
            Console.ForegroundColor = (ConsoleColor)(int)colour1;
            Console.SetCursorPosition(x, y);
            Console.Write(message);
            if (newLine)
                Console.WriteLine();
        }
        public void WriteOut(string message, int x, int y, Visual.Colours colour1, bool newLine = false)
        {
            writeOutMessage(message, x, y, colour1,newLine);
        }

        private delegate void ClearPartDelegate(byte length, int y);
        private ClearPartDelegate clearPart = clearPartText;
        private static void clearPartText(byte length, int y)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = y;
            Console.Write(" ".PadLeft(length));
        }
        public void ClearNextText(byte length, int line)
        {
            clearPartText(length, line);
        }

        private delegate void FullClearDelegate();
        private FullClearDelegate clearFull = fullClear;
        private static void fullClear()
        {
            Console.Clear();
        }
        public void ClearAllText()
        {
            fullClear();
        }

    }
}
