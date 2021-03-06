﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChatApp
{
    class Message
    {
        public int? MessageID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime Time { get; set; }
        public string TimeSincePost { get; set; }
        public User Transmitter { get; set; }

        public User Receiver { get; set; }

        public Message(string text, int userID)
        {
            UserID = userID;
            Text = text;
        }

        public Message(string username, string text, string time)
        {
            UserName = username;
            Text = text;
            Time = TimeConverter(time);
        }
        public Message(string text, int messageID, int userID)
        {
            Text = text;
            Time = DateTime.Now;
            MessageID = messageID;
            UserID = userID;
        }

        public Message(string username, string text, int messageID, int userID)
        {
            UserName = username;
            Text = text;
            Time = DateTime.Now;
            MessageID = messageID;
            UserID = userID;
        }
        
        public Message(string username, string text, string time, int messageID)
        {
            UserName = username;
            Text = text;
            Time = TimeConverter(time);
            MessageID = messageID;
        }

        public Message(string username, string text, string time, int messageID, int userID)
        {
            UserName = username;
            Text = text;
            Time = TimeConverter(time);
            MessageID = messageID;
            UserID = userID;
        }

        public Message(User username, string text, string time, int messageID)
        {
            Transmitter = username;
            Text = text;
            Time = TimeConverter(time);
            MessageID = messageID;
        }

        public Message(User username, User receiver, string text, string time, int messageID)
        {
            Transmitter = username;
            Text = text;
            Time = TimeConverter(time);
            MessageID = messageID;
            Receiver = receiver;
        }

        private DateTime TimeConverter(string text)
        {
            string[] dateTimeParts = text.Split(' ');
            string[] dateParts = dateTimeParts[0].Split('-');
            string[] timeParts = dateTimeParts[1].Split(':');
            return new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]), int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2])).ToLocalTime();
        }

        public void Insert()
        {
            if (RegexControl.ContainsSingleQuouteMark(Text))
                Text = Support.SanitiseSingleQuotes(Text);
            SQLControl.SQLAddMessage(Text, Time.ToUniversalTime().ToString("G", new CultureInfo("da-DK")));
        }

        public void Delete()
        {
            SQLControl.SQLRemoveMessage($"Message = '{Text}'");
        }



    }
}
