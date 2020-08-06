using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace GPOpgaver
{
    public static class Opgaver
    {
        /*
        * Introduktion til Algoritmer
        * Exercise 1. 
        * Describe an algorithm that interchanges the values of the variables x and y, using only assignment statements.
        * What is the minimum number of assignment statements needed to do so?
        */
        public static void Interchange(ref int x, ref int y)
        {
            int temp_ = x;
            x = y;
            y = temp_;
        }
        /*
        * Introduktion til Algoritmer
        * Exercise 2. 
        * 2.Describe an algorithm that uses only assignment statements to replace thevalues of the triple(x, y, z) with (y, z, x).
        * What is the minimum number of assignment statements needed?
        */
        public static void InterchangeTriple(ref int a, ref int b, ref int c)
        {
            int temp_ = a;
            a = c;
            c = b;
            b = temp_;
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 3.
         * A palindrome is a string that reads the same forward and backward.
         * Describe an algorithm for determining whether a string of n characters is a palindrome.
         */
        public static bool IsPalindrome(string s)
        {
            if (s.Length > 1)
                //byte pairs = (byte)Math.Floor((double)s.Length / 2d); //not needed, could just go to i and s.length-1-i is the same value using a while loop
                for (byte i = 0; i < (byte)Math.Floor(s.Length / 2d); i++)
                    if (s[i] != s[s.Length - 1 - i])
                        return false;
            return true;
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 4.a.
         * List all the steps used to search for 9 in the sequence 1, 3, 4, 5, 6, 8, 9, 11
         * Do this by completing the unit test for 4A
         */
        public static int StepsInLinearSearch(int searchFor, int[] intergerArray)
        {
            int steps = 1;
            foreach (int interger in intergerArray)
                if (searchFor == interger)
                    return steps;
                else
                    steps++;
            return steps;
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 4.b.
         * List all the steps used to search for 9 in the sequence 1, 3, 4, 5, 6, 8, 9, 11
         * Do this by completing the unit test for 4B
         */
        public static int StepsInBinarySearch(int[] integerArray, int arrayStart, int arrayEnd, int searchFor)
        {
            int stop = 1;
            if (arrayStart < arrayEnd)
            {
                int[] tempArray_ = new int[arrayEnd - arrayStart];
                for (int i = arrayStart; i < arrayEnd; i++)
                    tempArray_[i - arrayStart] = integerArray[i];
                integerArray = tempArray_;
                do
                {
                    int halfLength = (int)Math.Round(integerArray.Length / 2d) - 1;
                    int fullLength = integerArray.Length;
                    int[] array = integerArray;
                    if (integerArray[halfLength] == searchFor)
                        return stop;
                    else if (integerArray[halfLength] < searchFor)
                    {
                        integerArray = new int[fullLength - halfLength];
                        for (int i = halfLength; i < fullLength; i++)
                            integerArray[i - halfLength] = array[i];
                    }
                    else
                    {
                        integerArray = new int[halfLength];
                        for (int i = 0; i < integerArray.Length; i++)
                            integerArray[i] = array[i];
                    }
                    stop++;
                } while (integerArray.Length != 1);
            }
            return stop;
        }
        /*
         * Introduktion til Algoritmer
         * Exercise 5.
         * Describe an algorithm based on the linear search for de-termining the correct position in which to insert a new element in an already sorted list.
         */
        public static int InsertSortedList(List<int> sortedList, int insert)
        {
            for (int i = 0; i < sortedList.Count; i++)
                if (sortedList[i] > insert)
                    return i--;
            return sortedList.Count;
        }
        /*
         * Exercise 6.
         * Arrays
         * Create a function that takes two numbers as arguments (num, length) and returns an array of multiples of num up to length.
         * Notice that num is also included in the returned array.
         */
        public static int[] ArrayOfMultiples(int num, int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
                array[i] = (1 + i) * num;
            return array;
        }
        /*
         * Exercise 7.
         * Create a function that takes in n, a, b and returns the number of values raised to the nth power that lie in the range [a, b], inclusive.
         * Remember that the range is inclusive. a < b will always be true.
         */
        public static int PowerRanger(int power, int min, int max)
        { //return the number of numbers that lies between when raised to power. E.g. how many numbers are between 10 and 100. Start with 0^power, 1^power etc.
            int value = 0;
            int startValue = 0;
            int steps = 0;
            while (value < max)
            {
                value = (int)Math.Pow(startValue++, power);
                if (value >= min && value <= max)
                    steps++;
            }
            return steps;
        }
        /*
         * Exercise 8.
         * Create a Fact method that receives a non-negative integer and returns the factorial of that number.
         * Consider that 0! = 1.
         * You should return a long data type number.
         * https://www.mathsisfun.com/numbers/factorial.html
         */
        public static long Factorial(int n)
        {
            //if (n == 0)
            //    return 1;
            //return n * Factorial(--n);
            return n == 0 ? 1 : n * Factorial(--n);
        }
        /*
         * Exercise 9.
         * Write a function which increments a string to create a new string.
         * If the string ends with a number, the number should be incremented by 1.
         * If the string doesn't end with a number, 1 should be added to the new string.
         * If the number has leading zeros, the amount of digits should be considered.
         */
        public static string IncrementString(string txt)
        {
            //int posLetter = 0;
            //int posNumber = 0;
            int charPos = 0;
            char lastChar = (char)0;
            bool? firstLetter = null;
            char[] chrs = txt.ToCharArray();
            List<List<char>> charListsNumbers = new List<List<char>>();
            List<List<char>> charListsLetters = new List<List<char>>();
            //charListsNumbers.Add(new List<char>());
            //charListsLetters.Add(new List<char>());
            foreach (char chr in chrs)
            {
                if (firstLetter == null)
                    firstLetter = chr < 48 || chr > 57;
                if((bool)firstLetter && charListsLetters.Count == 0)
                    charListsLetters.Add(new List<char>());
                else if(!(bool)firstLetter && charListsNumbers.Count == 0)
                    charListsNumbers.Add(new List<char>());
                if (chr < 48 || chr > 57)
                {
                    charListsLetters[charListsLetters.Count-1].Add(chr);
                    if (charPos != chrs.Length - 1)
                    {
                        char nextChar = chrs[charPos + 1];
                        if (nextChar > 47 && nextChar < 58)
                        {
                            charListsNumbers.Add(new List<char>()); //it can allow the creation of empty lists, fix that. 
                        }
                    }

                }
                else
                {

                    charListsNumbers[charListsNumbers.Count-1].Add(chr);
                    if (charPos != chrs.Length - 1) { 
                        char nextChar = chrs[charPos + 1];
                        if(nextChar < 48 || nextChar > 57)
                        {

                            charListsLetters.Add(new List<char>());
                            //posLetter++;
                        }
                    }
                }
                lastChar = chr;
                charPos++;
            }
            //for (int i = charListsLetters.Count - 1; i >= 0; i--)
            //    if (charListsLetters[i].Count == 0)
            //        charListsLetters.RemoveAt(i);
            ////if (charListsLetters[charListsLetters.Count - 1].Count == 0)
            ////    charListsLetters.RemoveAt(charListsLetters.Count - 1);
            ////if (charListsNumbers[charListsNumbers.Count - 1].Count == 0)
            ////    charListsNumbers.RemoveAt(charListsNumbers.Count - 1);
            //for (int i = charListsNumbers.Count - 1; i >= 0; i--)
            //    if (charListsNumbers[i].Count == 0)
            //        charListsNumbers.RemoveAt(i);


            string[] wordStrings = new string[charListsLetters.Count];
            string[] valueStrings = new string[charListsNumbers.Count];
            int stringPos = 0;
            if (charListsNumbers.Count != 0)
                foreach (List<char> charList in charListsNumbers)
                {
                    if (charList.Count != 0)
                    {
                        char[] number = charList.ToArray();
                        char[] valueCharArray = (double.Parse(new string(number)) + 1).ToString().ToCharArray();
                        if (valueCharArray.Length < number.Length) //add trailing zeroes back if they are missing
                        {
                            byte zeroDifference = (byte)(number.Length - valueCharArray.Length);
                            char[] zeroArray = new char[zeroDifference];
                            for (byte i = 0; i < zeroDifference; i++)
                                zeroArray[i] = '0';
                            int length = zeroArray.Length + valueCharArray.Length;
                            char[] temp_ = new char[length];
                            for (int i = 0; i < length; i++)
                            {
                                if (i < zeroArray.Length)
                                    temp_[i] = zeroArray[i];
                                else
                                    temp_[i] = valueCharArray[i - zeroArray.Length];
                            }
                            valueCharArray = temp_;
                        }
                        valueStrings[stringPos] = new string(valueCharArray);
                    }
                    else
                        valueStrings[stringPos] = "1";
                    stringPos++;
                }
            else
                valueStrings = new string[] { "1" };

            stringPos = 0;
            foreach (List<char> charList in charListsLetters)
            {
                char[] word = charList.ToArray();
                wordStrings[stringPos] = new string(word);
                stringPos++;
            }
            string returnString = "";
            string[] start = (bool)firstLetter ? wordStrings : valueStrings;
            string[] rest = !(bool)firstLetter ? wordStrings : valueStrings;
            int longestLength = start.Length > rest.Length ? start.Length : rest.Length;
            for (int i = 0; i < longestLength; i++)
            {
                if (i < start.Length)
                    returnString += start[i];
                if (i < rest.Length)
                    returnString += rest[i];
            }
            return returnString;
        }
    }
}