using System.ComponentModel.DataAnnotations;

namespace SpiceChat.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        [Required]
        public string Role { get; set; }
    }
}