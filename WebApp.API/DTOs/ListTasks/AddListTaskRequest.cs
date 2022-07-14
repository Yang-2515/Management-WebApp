using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.ListTasks
{
    public class AddListTaskRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public int? BoardId { get; set; }
    }
}
