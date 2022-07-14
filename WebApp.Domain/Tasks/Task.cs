using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Labels;
using WebApp.Domain.ListTasks;
using WebApp.Domain.Tasks;
using WebApp.Domain.ToDos;
using WebApp.Domain.Users;

namespace WebApp.Domain.Tasks
{
    [Table("Task")]
    public partial class Task : EntityBase<int>
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }
        public int ListTaskId { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public string Priority { get; set; }
        public virtual ListTask ListTask { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }
        public virtual ICollection<TaskMember> TaskMembers { get; set; }
        public virtual ICollection<Attackment> Attackments { get; set; }
        public virtual ICollection<TaskLabel> TaskLabels { get; set; }
    }
}
