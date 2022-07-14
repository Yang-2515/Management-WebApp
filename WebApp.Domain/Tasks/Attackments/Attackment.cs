using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Tasks;

namespace WebApp.Domain.Tasks
{
    [Table("Attackment")]
    public partial class Attackment : EntityBase<int>
    {
        [Required]
        [StringLength(100)]
        public string StorageURL { get; set; }
        public string FileName { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
