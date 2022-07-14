using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.ListTasks
{
    public class ListTaskReponseGetBoard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int CoutTask { get; set; }
    }
}
