using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Office365ConnectorsDemo.Models
{
    public class MessageModel
    {
        public class PotentialAction
        {
            public string @context { get; set; }
            public string @type { get; set; }
            public string name { get; set; }
            public List<string> target { get; set; }
        }

        public class Message
        {
            public string text { get; set; }
            public string title { get; set; }
            public List<PotentialAction> potentialAction { get; set; }
        }
    }
}