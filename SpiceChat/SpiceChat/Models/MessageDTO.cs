using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpiceChat.Models
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ConversationID { get; set; }
        public int CreatedBy { get; set; }
    }

    public class MessageDetailDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AttachmentLocation { get; set; }
        public string AttachmentContentType { get; set; }
        public string AttachmentName { get; set; }
        public int ConversationID { get; set; }
        public int CreatedBy { get; set; }
    }
}