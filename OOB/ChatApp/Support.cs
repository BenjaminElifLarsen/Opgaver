using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    class Support
    {
        public static void FoundForbiddenWord()
        {
            Console.WriteLine("Found a forbidden word, operation has been canceled");
            Console.ReadKey();
            ClearBuffer();
        }

        public static void ClearBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
    }
}
