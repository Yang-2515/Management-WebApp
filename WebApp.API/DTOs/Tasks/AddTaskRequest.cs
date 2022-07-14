using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Tasks
{
    public class AddTaskRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int ListTaskId { get; set; }
        public int? UserId { get; set; }
        public string Priority { get; set; }
    }
}
