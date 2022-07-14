using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.DTOs.Attackments;
using WebApp.API.DTOs.Labels;
using WebApp.API.DTOs.ToDos;
using WebApp.API.DTOs.Users;
using WebApp.Domain.Labels;
using WebApp.Domain.Tasks;
using WebApp.Domain.Users;

namespace WebApp.API.DTOs.Tasks
{
    public class GetTaskResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int ListTaskId { get; set; }
        public int? UserId { get; set; }
        public string Priority { get; set; }
        public List<LabelResponse> Labels { get; set; }
        public List<AttackmentResponse> Attackments { get; set; }
        public List<ToDoResponseGetTask> ListToDos { get; set; }
        public List<AddUserResponse> Members { get; set; }
    }
}
