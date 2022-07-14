using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.DTOs.Tasks;

namespace WebApp.API.DTOs.ListTasks
{
    public class GetListTaskResponse
    {
        public ListTaskResponse ListTask { get; set; }
        public List<TaskResponseGetListTask> Tasks { get; set; }
    }
}
