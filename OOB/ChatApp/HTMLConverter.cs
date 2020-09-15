using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    class HTMLConverter
    {


        public static string StringMessageToHTMLMessage(Message message) //Time, UserName, Message
        {
            return String.Format("<div class=\"messageRow\"><p><p class=\"messageTime\">{0} <span class=\"username\">{1}</span></p><p><span class=\"message\">{2}</span></p></div>",message.TimeSincePost,message.User.Name,message.Text);
        }

        public static string UsernameToHTMLUsername(User user)
        {
            return $"<li>{user.Name}</li>";
        }


    }
}
