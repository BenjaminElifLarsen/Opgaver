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
        static private int loggedInID; 
        
        static public string GetUserName { get => loggedInUser; }
        static public string SetUserName { set => loggedInUser = value; }
        static public int GetUserID { get => FindID(loggedInUser); }
        static UserDirectory()
        {
            loggedInUser = "Guest";
        }

        static public bool DoesLoginExist(string login) //should check the database and find the different usernames
        {
            Result result = SQLet.GetResult($"Select Distinct UserName From User_Information");
            for (int i = 0; i < result.Count; i++)
            {
                if(login == result[i]["UserName"])
                        return false;
            }
            return true;
        }

        static private int FindID(string login)
        {
            if(GetUserName != "Guest")
            { 
                string[][] IDs = SQLet.GetArray($"Select UserID From User_Information where UserName = '{login}'");
                return int.Parse(IDs[0][0]);
            }
            return 0;
        }

    }
}
