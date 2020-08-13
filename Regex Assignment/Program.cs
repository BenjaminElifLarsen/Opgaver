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
     * supported special characters: ! @ # $ % ^ & * ( ) + = _ - { } [ ] : ; " ' ? < > , .
     */


    class Program
    {
        static void Main(string[] args)
        {
            string test = "12Db34dweweEEww";
            //Password.ValueTest(test);
            //Password.LetterLowerTest(test);
            //Password.PasswordChecker(test);
            Password.PasswordChecker("Pè7$areLove");
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

        static string alphanumericalString = @"[a-zA-Z \!\@\#\$\%\^\&\*\(\)\+\=\-\{\}\[\]\:\;\""\'\?\<\>\,\.]{1,24}";
        static string combinationPattern = $@"{alphanumericalString}({valuesString}|{lowerLetterString}|{upperLetterString})";
        static Regex rgPassword = new Regex(combinationPattern);
        static Regex rgPasswordLength = new Regex(alphanumericalString);
        static Regex rgPasswordValue = new Regex("." + valuePattern);
        static Regex rgPasswordLower = new Regex("." + lowerLetterString);
        static Regex rgPasswordUpper = new Regex("." + upperLetterString);
        static Regex rgPasswordLowerRepeat = new Regex(@"([a-zA-Z])\1{2,}");
        //static Regex rgPasswordUpperRepeat = new Regex(".[A-Z]{3}");
        static Regex rgPasswordSpecial = new Regex(@"[\!\@\#\$\%\^\&\*\(\)\+\=\-\{\}\[\]\:\;\""\'\?\<\>\,\.]*");

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

        public static bool PasswordChecker(string text)
        {
            //MatchCollection matches = rgPassword.Matches(text);
            //MatchCollection matches = rgPasswordLength.Matches(text);
            int length = text.Length;
            MatchCollection match = rgPasswordLength.Matches(text);
            if (rgPasswordLength.IsMatch(text))
            
                //MatchToString(ref text);
               // matches = rgPasswordValue.IsMatch(text);
                if(rgPasswordValue.IsMatch(text))
                
                    //MatchToString(ref text);
                    //matches = rgPasswordLower.Matches(text);
                    if (rgPasswordLower.IsMatch(text))
                    
                        //MatchToString(ref text);
                        //matches = rgPasswordUpper.Matches(text);
                        if (rgPasswordUpper.IsMatch(text))
                        
                            if (!rgPasswordLowerRepeat.IsMatch(text))
                            
                                if(rgPasswordSpecial.IsMatch(text))
                                //if (!rgPasswordUpperRepeat.IsMatch(text))
                                {
                                    Console.WriteLine(text);

                                    return true;
                                }

            return false;


            //void MatchToString(ref string txt)
            //{
            //    txt = "";
            //    foreach (Match match in matches)
            //        txt += match.Value + " ";
            //}
        }

        private static void WriteMatchOut(MatchCollection matches)
        {
            foreach (Match match in matches)
                Console.WriteLine(match);
        }
    }


}
