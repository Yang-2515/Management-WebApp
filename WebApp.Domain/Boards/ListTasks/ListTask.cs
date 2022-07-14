using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Boards;
using WebApp.Domain.Tasks;

namespace WebApp.Domain.ListTasks
{
    [Table("ListTask")]
    public partial class ListTask : EntityBase<int>
    {
        public ListTask(string name, string color, int boardId)
        {
            Name = name;
            Color = color;
            BoardId = boardId;
        }
        public ListTask()
        {

        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
