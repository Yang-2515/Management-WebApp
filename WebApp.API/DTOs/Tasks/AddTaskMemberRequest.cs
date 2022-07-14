using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Tasks
{
    public class AddTaskMemberRequest
    {
        public AddTaskMemberRequest(int taskId, int userId)
        {
            TaskId = taskId;
            UserId = userId;
        }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    }
}
