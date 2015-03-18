using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpiceChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedAt { get; set; }
        public string AttachmentLocation { get; set; }
        public string AttachmentContentType { get; set; }
        public string AttachmentName { get; set; }

        // Foreign Key
        [Required]
        public int ConversationID { get; set; }
        [Required]
        public int CreatedBy { get; set; }
    }
}