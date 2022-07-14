using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Users
{
    public class AddUserResponse
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string LinkImage { get; set; }
    }
}
