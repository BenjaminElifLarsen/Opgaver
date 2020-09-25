using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ChatApp
{
    class Webpage
    {
        public static string GetHTML(List<Message> messages, List<User> users, User user)
        {
            string html = "";

            List<string> htmlCodeIndex;

            string htmlMessages = GetHTMLMessages(messages);
            string htmlUsers = GetHTMLUsers(users);
            try
            {
                htmlCodeIndex = ReadHtmlPage("indexBase");
                Replacer(htmlCodeIndex, htmlUsers, htmlMessages, user.ID);
                WriteHTMLPage(htmlCodeIndex, "index");
                foreach (string str in htmlCodeIndex)
                    html += str;
            }
            catch (FileNotFoundException e)
            {
                Reporter.Report(e);
                html = "Failed at loading html page (serverside)";
            }
            try
            {
                string js = File.ReadAllText("HTML/updateMessages.js");
                html = html.Replace("{{JavaScript}}", js);
            }
            catch (FileNotFoundException e)
            {
                Reporter.Report(e);
                html = "Failed at loading javascript (serverside)";
            }
            return html;
        }

        public static string GetHTMLMessages(List<Message> messages)
        {
            string message = "";
            foreach (string str in GenerateMessagesWithHTML(messages))
                message += str;
            return message;
            
        }


        private static void Replacer(List<string> htmlStrings, string users, string messages, int userID = 1)
        {
            Replace(htmlStrings, "{{messages}}", messages);
            Replace(htmlStrings, "{{users}}", users);
            Replace(htmlStrings, "{{USERID}}", userID.ToString());
        }

        private static string[] GenerateMessagesWithHTML(List<Message> nonHTMLMessage) 
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

        public static string GetHTMLUsers(List<User> messages)
        {
            string users = "<ul>";
            foreach (string str in GenerateUsernameWithHTML(messages))
                users += str;
            users += "</ul>";
            return users;
        }

        private static string[] GenerateUsernameWithHTML(List<User> nonHTML)
        {
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

        public static string GenerateLoginHTML()
        {
            string html = "";
            string login = "";
            List<string> htmlCodeLogin;
            //List<string> htmlCodeIndex = ReadHtmlPage("indexBase");
            try { 
                htmlCodeLogin = ReadHtmlPage("login");
                foreach (string str in htmlCodeLogin)
                    login += str;
            }
            catch(FileNotFoundException e)
            {
                Reporter.Report(e);
                login = "Failed at loading html page (serverside)";
            }
            //WriteHTMLPage(htmlCodeIndex, "index");

            //Replacer(htmlCodeIndex, null, login);
            //foreach (string str in htmlCodeIndex)
            //    html += str;
            return login;
        }

    }
}
