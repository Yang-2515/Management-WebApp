using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.DTOs.ToDos;
using WebApp.Domain.Labels;
using WebApp.Domain.ToDos;

namespace WebApp.API.DTOs.Tasks
{
    public class TaskResponseGetListTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public string Priority { get; set; }
        public int CountAttackment { get; set; }
        public int CountToDo { get; set; }
        public List<ToDoResponse> ToDos { get; set; }
    }
}
