using System;
using System.Collections.Generic;
using System.Text;
using TECHCOOL;

namespace ChatApp
{
    static class UserDirectory
    {
        static private Dictionary<string, User> users = new Dictionary<string, User>();
        static private string loggedInUser;
        
        static public string GetUserName { get => loggedInUser; }
        static public string SetUserName { set => loggedInUser = value; }
        static UserDirectory()
        {
            loggedInUser = "Guest";
        }

        static public bool DoesLoginExist(string login) //should check the database and find the different usernames
        {
            Result result = SQLet.GetResult($"Select Distinct UserName From Message_Information");
            for (int i = 0; i < result.Count; i++)
            {
                if(login == result[i]["UserName"])
                        return false;
            }
            return true;
        }



    }
}
