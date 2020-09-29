using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    class PrivateMessage : Message
    {

        public int RecipientID { get; set; }
        public PrivateMessage(User username, string text, string time, int messageID, int recipientID) : base(username,text,time,messageID)
        {
            RecipientID = recipientID;
        }
        

    }
}
