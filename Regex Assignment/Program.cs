using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Regex_Assignment
{
    /*Regex password
     * 6-24 signs.
     * contains min. 1 uppercase letter.
     * contains min. 1 lowercase letter.
     * contains numbers from 0 to 9.
     * max two characters after each others are similar, e.g. aa is fine, but aaa is not.
     * 
     */


    class Program
    {
        static void Main(string[] args)
        {
            string test = "Hey 1 there 3 how 9 is 5 it 0 going 2?";
            Password.Test(test);
        }
    }


    public class Password
    {
        static string testNumberString = "[0-9]*";
        static string pattern = $@"{testNumberString}";
        static Regex rg = new Regex(pattern);

        public static void Test(string text)
        {
            MatchCollection matches = rg.Matches(text);

            foreach (var match in matches)
                Console.WriteLine(match);
        }

    }


}
