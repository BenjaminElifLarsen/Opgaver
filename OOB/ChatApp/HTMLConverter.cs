using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    class HTMLConverter
    {


        public static string StringMessageToHTMLMessage(Message message) 
        {
            return String.Format("<div class=\"messageRow\"><p><p class=\"messageTime\">{0} <span class=\"username\">{1}</span></p><p><span class=\"message\">{2}</span></p></div>",message.TimeSincePost,message.Transmitter.Name,message.Text);
        }

        public static string UsernameToHTMLUsername(User user)
        {
            return $"<li><a href=\"#{user.ID}\">{user.Name}</a></li>";
        }


    }
}
