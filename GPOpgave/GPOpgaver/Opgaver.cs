using System;
using System.Collections.Generic;
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
            {
                char[] chrs = s.ToCharArray();
                byte pairs = (byte)Math.Floor((double)chrs.Length / 2d);
                for (byte i = 0; i < pairs; i++)
                {
                    if (s[i] != s[s.Length - 1 - i])
                        return false;
                }
            }
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
            int stop = 1;
            foreach (int interger in intergerArray)
                if (searchFor == interger)
                    return stop;
                else
                    stop++;
            return stop;
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
                int[] tempArray_ = new int[arrayEnd-arrayStart];
                for (int i = arrayStart; i < arrayEnd; i++)
                {
                    tempArray_[i - arrayStart] = integerArray[i];
                }
                integerArray = tempArray_;
                do
                {
                    int length = (int)Math.Round(integerArray.Length / 2d)-1;
                    int fullLength = integerArray.Length;
                    int[] array = integerArray;
                    if (integerArray[length] == searchFor)
                        return stop;
                    else if (integerArray[length] < searchFor)
                    {
                        integerArray = new int[fullLength - length];
                        for (int i = length; i < fullLength; i++)
                            integerArray[i - length] = array[i];
                    }
                    else
                    {
                        integerArray = new int[length];
                        for (int i = 0; i < integerArray.Length; i++)
                            integerArray[i] = array[i];
                    }
                    stop++;
                } while (true);
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
        { //return the number of numbers that lies between when raised
            long value = 0;
            long startValue = (long)min;
            int steps = 1;
            while(value < (long)max)
            {
                steps++;
                value = (long)Math.Pow(startValue, power);
                startValue++;
            }
            return --steps;
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
            throw new NotImplementedException();
            //Write your solution here
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
            throw new NotImplementedException();
            //Write your solution here
        }
    }
}