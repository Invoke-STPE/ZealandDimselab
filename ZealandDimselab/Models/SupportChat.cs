using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class SupportChat
    {
        public string Support { get; set; }
        public string Student { get; set; }
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}
