using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Users
{
    public class AddUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
    }
}
