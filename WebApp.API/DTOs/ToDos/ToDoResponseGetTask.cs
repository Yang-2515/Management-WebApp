using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.ToDos
{
    public class ToDoResponseGetTask
    {
        public ToDoResponse ToDo { get; set; }
        public List<ToDoResponse> ToDos { get; set; }
    }
}
