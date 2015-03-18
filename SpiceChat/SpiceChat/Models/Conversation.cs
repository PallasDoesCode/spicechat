using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpiceChat.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedAt { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime ClosedAt { get; set; }
        

        // Foreign Key
        [Required]
        public int CreatedBy { get; set; }
    }
}