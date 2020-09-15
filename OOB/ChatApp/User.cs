using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    public class User
    {
        private string userName;
        private int userID; 
        private User()
        {

        }

        public User(string username, int userID)
        {
            userName = username;
            this.userID = userID;
        }

        public string Name { get => userName; set => userName = value; }
        public int ID { get => userID; set => userID = value; }


    }
}
