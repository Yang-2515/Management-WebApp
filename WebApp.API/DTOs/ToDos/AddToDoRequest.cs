using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.ToDos
{
    public class AddToDoRequest
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int TaskId { get; set; }
        public int? ParentId { get; set; }
    }
}
