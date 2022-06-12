using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class MessageModel
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.Now;
    }
}
