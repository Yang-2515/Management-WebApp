using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.DTOs.Tasks
{
    public class AddTaskLabelRequest
    {
        public int TaskId { get; set; }
        public int LabelId { get; set; }
        public int Index { get; set; }
    }
}
