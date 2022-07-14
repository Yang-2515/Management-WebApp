using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Users
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string HomeAddress { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string LinkImage { get; set; }
    }
}
