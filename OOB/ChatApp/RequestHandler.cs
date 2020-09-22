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
            httpListener.Start();
        }

        private string RequestLogin(Request r)
        {


            return Webpage.GenerateLoginHTML();
        }

        //user request localHost:8080/
        private string RequestRoot(Request r)
        {
            string method = r.Context.Request.HttpMethod;
            if(method.ToLower() == "post")
            {
                RequestData data = r.Data;
                if (data.Post.ContainsKey("username"))
                {
                    PostUser(data);
                }
                else if (data.Post.ContainsKey("chatmessage"))
                {
                    PostMessage(data);
                }
            }

            return Webpage.GetHTML(SQLControl.SQLGetMessages(), SQLControl.SQLGetUsers());
        }

        private void PostUser(RequestData data)
        {
            try
            {
                string username = data.Post["username"];
                username = Support.SanitiseSingleQuotes(username);
                SQLControl.CreateUser(data.Post["username"], "Test123.");
            }
            catch
            {
                string username = data.Post["username"];
                username = Support.SanitiseSingleQuotes(username);
            }
        }

        private void PostMessage(RequestData data)
        {
            string clientMessage = data.Post["chatmessage"];
            SQLControl.SQLAddMessage(clientMessage, 1);

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
            return System.IO.File.ReadAllText(@".\HTML\style.css");
        }

        //transmission of files
        private string RequestAsset(Request r)
        {
            throw new NotImplementedException();
        }

    }
}
