using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Message
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public DateTime SendDate { get; set; }
        public MessageType Type { get; set; }
    }
}
