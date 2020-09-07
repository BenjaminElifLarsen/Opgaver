using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp
{
    public class User
    {
        private string userName;
        private int userID; 
        public User()
        {

        }

        public User(string username, int userID)
        {
            userName = username;
            this.userID = userID;
        }

    }
}
