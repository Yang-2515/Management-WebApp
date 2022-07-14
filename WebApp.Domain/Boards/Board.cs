using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Boards;
using WebApp.Domain.ListTasks;
using WebApp.Domain.Users;

namespace WebApp.Domain.Boards
{
    [Table("Board")]
    public partial class Board : EntityBase<int>
    {
        public Board()
        {

        }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BoardMember> BoardMembers { get; set; }
        public virtual ICollection<ListTask> ListTasks { get; set; }

        public void DoSomeThing()
        {
            // lam du thu
                
        }
    }
}
