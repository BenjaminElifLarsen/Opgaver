using System;
using System.Collections.Generic;
using System.Text;
using TECHCOOL;

namespace ChatApp
{
    class RequestHandler
    {

        

        public void Start()
        {
            WebLet httpListener = new WebLet("http://localHost:8080/");
            //routering, paring af urler og metoder
            httpListener.Route("^[/]$HTML^[/]$style^[/]$css", RequestCSS);
            httpListener.Route("^[/]$", RequestRoot);
            httpListener.Start();
        }

        //user request localHost:8080/
        private string RequestRoot(Request r)
        {
            string method = r.Context.Request.HttpMethod;
            if(method.ToLower() == "post")
            {
                RequestData data = r.Data;
                string clientMessage = data.Post["chatmessage"];
                SQLControl.SQLAddMessage(clientMessage, 1);
            }

            return Webpage.GetHTML(SQLControl.SQLGetMessages(), SQLControl.SQLGetUsers());
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
