using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Tasks;

namespace WebApp.Domain.ToDos
{
    [Table("ToDo")]
    public partial class ToDo : EntityBase<int>
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int TaskId { get; set; }
        public int? ParentId { get; set; }
        public virtual Task Task { get; set; }

    }
}
