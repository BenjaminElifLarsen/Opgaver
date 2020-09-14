using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    class Web
    {

        public static string GetHTMLMessages(List<Message> messages)
        {
            string message = @"";
            foreach (string str in GenerateMessagesWithHTML(messages))
                message += str;
            string test = GetHTMLUsers(SQLControl.SQLGetUsers());
            return message;
        }

        private static string[] GenerateMessagesWithHTML(List<Message> nonHTMLMessage) //make private when tested and the rest of the web is working.
        {
            //string[][] nonHTML = new string[5][];//SQLControl.SQLGetMessages();
            nonHTMLMessage = Support.MessageTimePrepare(nonHTMLMessage);
            string[] messagesWithHTML = new string[nonHTMLMessage.Count];
            for (int n = 0; n < messagesWithHTML.Length; n++)
                messagesWithHTML[n] = HTMLConverter.StringMessageToHTMLMessage(nonHTMLMessage[n]);
            return messagesWithHTML;
        }

        public static string GetHTMLUsers(List<User> messages)
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

    }
}
