using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TECHCOOL;

namespace ChatApp
{
    class RequestHandler
    {

        private string[] host;

        public string[] SetHost { set { host = value; Debug.WriteLine(host[0]); } }

        public void Start()
        {
            WebLet httpListener = new WebLet(host[0]);
            //routering, paring af urler og metoder
            httpListener.Route("^[/]login$",RequestLogin);
            httpListener.Route("^[/]users$", RequestUser);
            httpListener.Route("^[/]messages$", RequestMessages);
            httpListener.Route("^[/]$HTML^[/]$style^[/]$css", RequestCSS);
            httpListener.Route("^[/]$", RequestRoot);
            try {
                Reporter.Log($"Starting server up with {host[0]}");
                httpListener.Start();
            }
            catch (System.Net.HttpListenerException e)
            {
                Reporter.Log($"Failed at starting server with {host[0]}");
                Console.Clear();
                Reporter.Report(e);
                Console.WriteLine("Could not start the server. Error logged at " + Reporter.ErrorLocation);
                Console.Read();
                Menu.SetWebThreadRunning = false;
            }
            catch(Exception e)
            {
                Reporter.Log($"See error log for the error");
                Console.Clear();
                Reporter.Report(e);
                Console.WriteLine("Error! Error logged at " + Reporter.ErrorLocation);
                Console.Read();
                Menu.SetWebThreadRunning = false;

            }
        }

        private string RequestLogin(Request r)
        {


            return Webpage.GenerateLoginHTML();
        }

        //user request localHost:8080/
        private string RequestRoot(Request r) //needed to permit the firewall to transmit tcp data out on port 80
        {
            string method = r.Context.Request.HttpMethod;
            if(method.ToLower() == "post")
            {
                User user = null;
                RequestData data = r.Data;
                if (data.Post.ContainsKey("username"))
                {
                    user = PostUser(data);
                }
                else if (data.Post.ContainsKey("chatmessage"))
                {
                    try { 
                    user = SQLControl.SQLGetUser(Int32.Parse(data.Post["userID"])); //fails right now here because of userID is not set. input string was not in right format (value is {{USERID}} if
                    if(user != null) //Webpage.GetHTML is called instead of WebPage.GeneratorLoginHTML() the first time
                        PostMessage(data);
                    }
                    catch (FormatException e)
                    {
                        Reporter.Report(e);
                    }
                }

                if (user != null)
                {
                    try
                    {
                        return Webpage.GetHTML(SQLControl.SQLGetMessages(user), SQLControl.SQLGetUsers(), user);
                    }
                    catch (Exception e)
                    {
                        Reporter.Report(e);
                        return Webpage.GetHTML(SQLControl.SQLGetMessages(), SQLControl.SQLGetUsers(), user);
                    }
                }
            }

            //return Webpage.GetHTML(SQLControl.SQLGetMessages(), SQLControl.SQLGetUsers());
            return Webpage.GenerateLoginHTML();
        }

        private User PostUser(RequestData data)
        {
            try
            {
                string username = data.Post["username"];
                username = Support.SanitiseSingleQuotes(username);
                SQLControl.CreateUser(data.Post["username"], data.Post["password"]);
                return SQLControl.SQLGetUser(username);
            }
            catch
            {
                try
                {
                    string username = data.Post["username"]; //try catch this in case of Username is to long 
                    username = Support.SanitiseSingleQuotes(username);
                    return SQLControl.SQLGetUser(username);
                }
                catch(Exception e)
                {
                    Reporter.Report(e);
                    return null;
                }
            }
        }

        private void PostMessage(RequestData data)
        {
            string clientMessage = data.Post["chatmessage"];
            if(data.Post["userID"] != null) { 
                if(data.Post["recipientID"] == null || data.Post["recipientID"] == "" || data.Post["recipientID"] == "0")
                    SQLControl.SQLAddMessage(clientMessage, Int32.Parse(data.Post["userID"]));
                else
                    SQLControl.SQLAddMessage(clientMessage, Int32.Parse(data.Post["userID"]), Int32.Parse(data.Post["recipientID"]));
            }
        }

        //localhost:8080/messages
        private string RequestMessages(Request r)
        {
            return Webpage.GetHTMLMessages(SQLControl.SQLGetMessages());
        }

        //localhost:8080/users
        private string RequestUser(Request r)
        {
            return Webpage.GetHTMLUsers(SQLControl.SQLGetUsers());
        }


        //transmission of css files (can also transmit other files)
        private string RequestCSS(Request r)
        {
            try
            {
                return System.IO.File.ReadAllText(@".\HTML\style.css");
            }
            catch (System.IO.IOException e)
            {
                Reporter.Report(e);
                return "Failed at loading css (serverside)";
            }
        }

        //transmission of files
        private string RequestAsset(Request r)
        {
            throw new NotImplementedException();
        }

    }
}
