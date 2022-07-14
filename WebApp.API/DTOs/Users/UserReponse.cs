using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Users
{
    public class UserReponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string FirstSecurityString { get; set; }
        public string LastSecurityString { get; set; }
    }
}
