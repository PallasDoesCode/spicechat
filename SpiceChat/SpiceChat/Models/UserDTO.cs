using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpiceChat.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }

    public class UserDetailDTO
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; }
    }
}