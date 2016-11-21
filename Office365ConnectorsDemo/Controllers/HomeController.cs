using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Office365ConnectorsDemo.Models;

namespace Office365ConnectorsDemo.Controllers
{
    // sample for using a Office 365 connector from a custom web app
    // Toni Pohl, atwork.at, @atwork
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Callback(string state, string webhook_url, string group_name, string error = null)
        {
            // now we get the webhook URL back. Save that!
            ViewBag.PostUrl = webhook_url;
            return View();
        }

        [HttpPost]
        public ActionResult Send(PostModel model)
        {
            // Send a message to the webhook
            var message = new MessageModel.Message
            {
                text = "This is data sent from my Line of Business App.",
                title = "Hello from my App!",
                potentialAction = new List<MessageModel.PotentialAction>
                {
                    new MessageModel.PotentialAction {
                        @context = "http://schema.org",
                        @type = "ViewAction",
                        name = "View in my app",
                        target = new List<string> { "<SOME-URL>"}
                    }
                }
            };

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                // Unfortunately the JSON body of Connectors is using the "@" symbol for field names... this is a workaround for C#
                string json = JsonConvert.SerializeObject(message).Replace("context", "@context").Replace("type", "@type");
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                // We need our webhook here:
                var url = "https://outlook.office365.com/webhook/<YOUR-URL>";
                var result = client.UploadString(url, "POST", json);
                // if everything was ok, we get "1" back
                model.Result = result;
            }
            return View("Index", model);
        }

    }
}