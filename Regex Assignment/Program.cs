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
            string test = "Hey 1 there 3 how 9 is 5 it 0 going 2? eH 12Db34";
            Password.ValueTest(test);
            Password.LetterLowerTest(test);
            Password.PasswordChecker(test);
        }
    }


    public class Password
    {
        static string valuesString = "[0-9]{1,}";
        static string valuePattern = $@"{valuesString}";
        static Regex rgValue = new Regex(valuePattern);

        static string lowerLetterString = "[a-z]{1,}";
        static string upperLetterString = "[A-Z]{1,}";
        static string letterPattern = $@"{upperLetterString}|{lowerLetterString}";
        static Regex rgLowerLetter = new Regex(letterPattern);

        static string alphanumericalString = @"\w{1,24}";
        static string combinationPattern = $@"{alphanumericalString}({valuesString}|{lowerLetterString}|{upperLetterString})";
        static Regex rgPassword = new Regex(combinationPattern);

        public static void ValueTest(string text)
        {
            MatchCollection matches = rgValue.Matches(text);
            WriteMatchOut(matches);
        }

        public static void LetterLowerTest(string text)
        {
            MatchCollection matches = rgLowerLetter.Matches(text);
            WriteMatchOut(matches);
        }

        public static void PasswordChecker(string text)
        {
            MatchCollection matches = rgPassword.Matches(text);
            WriteMatchOut(matches);
        }

        private static void WriteMatchOut(MatchCollection matches)
        {
            foreach (Match match in matches)
                Console.WriteLine(match);
        }
    }


}
