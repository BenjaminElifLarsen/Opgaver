using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ChatApp
{
    class Webpage
    {
        public static string GetHTML(List<Message> messages)
        {
            string html = "";
            List<string> htmlCodeIndex = ReadHtmlPage("indexBase");
            string htmlMessages = GetHTMLMessages(messages);
            string htmlUsers = GetHTMLUsers(SQLControl.SQLGetUsers());
            Replacer(htmlCodeIndex, htmlUsers, htmlMessages);
            WriteHTMLPage(htmlCodeIndex, "index");
            return html;
        }

        private static string GetHTMLMessages(List<Message> messages)
        {
            string message = "";
            foreach (string str in GenerateMessagesWithHTML(messages))
                message += str;
            //string test = GetHTMLUsers(SQLControl.SQLGetUsers());
            //string test3 = HttpUtility.HtmlEncode("\"test\"");
            //string test4 = System.Net.WebUtility.HtmlEncode("\"test\"");
            //List<string> htmlCodeIndex = ReadHtmlPage("indexBase");
            //Console.Clear();
            //display(htmlCodeIndex);
            //WriteHTMLPage(htmlCodeIndex, "index");
            return message;
            
        }


        private static void Replacer(List<string> htmlStrings, string users, string messages)
        {
            Replace(htmlStrings, "{{messages}}", messages);
            Replace(htmlStrings, "{{users}}", users);
        }

        private static string[] GenerateMessagesWithHTML(List<Message> nonHTMLMessage) //make private when tested and the rest of the web is working.
        {
            nonHTMLMessage = Support.MessageTimePrepare(nonHTMLMessage);
            string[] messagesWithHTML = new string[nonHTMLMessage.Count];
            for (int n = 0; n < messagesWithHTML.Length; n++)
                messagesWithHTML[n] = HTMLConverter.StringMessageToHTMLMessage(nonHTMLMessage[n]);
            return messagesWithHTML;
        }

        private static void Replace(List<string> strings, string oldValue, string newValue)
        {
            for(int i = 0; i < strings.Count; i++)
                if (strings[i] == oldValue)
                {
                    strings[i] = newValue;
                    break;
                }
        }

        private static string GetHTMLUsers(List<User> messages)
        {
            string users = "";
            foreach (string str in GenerateUsernameWithHTML(messages))
                users += str;
            return users;
        }

        private static string[] GenerateUsernameWithHTML(List<User> nonHTML)
        {
            //List<string> nonHTML = SQLControl.SQLGetUsers();
            string[] usernamesWithHTML = new string[nonHTML.Count];
            for (int n = 0; n < usernamesWithHTML.Length; n++)
                usernamesWithHTML[n] = HTMLConverter.UsernameToHTMLUsername(nonHTML[n]);
            return usernamesWithHTML;
        }


        private static void Display(List<string> file)
        {
            foreach (string str in file)
                Console.WriteLine(str);
        }

        private static List<string> ReadHtmlPage(string page) 
        {
            string fileLine;
            List<string> fileLines = new List<string>();
            using(StreamReader file = new StreamReader(@$".\HTML\{page}.html"))
            {
               while((fileLine = file.ReadLine()) != null)
                {
                    fileLines.Add(fileLine);
                }
            }
            return fileLines;
        }

        private static void WriteHTMLPage(List<string> htmlLines, string page)
        {
            using(StreamWriter file = new StreamWriter(@$".\HTML\{page}.html",false))
            {

                foreach (string str in htmlLines)
                    file.WriteLine(str);
            }
        }

    }
}
