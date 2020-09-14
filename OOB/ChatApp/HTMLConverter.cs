using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    class HTMLConverter
    {


        public string StringMessageToHTMLMessage(string[] message) //Time, UserName, Message
        {
            return $"<div class=\"messageRow\"><p><p class=\"{message[0]}\">10:11 <span class=\"username\">{message[1]}: </p></span><span class=\"message\">{message[2]}</span></p></div>";
        }

    }
}
