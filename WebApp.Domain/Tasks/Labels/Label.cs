using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Tasks;

namespace WebApp.Domain.Labels
{
    [Table("Label")]
    public partial class Label : EntityBase<int>
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        public virtual ICollection<TaskLabel> TaskLabels { get; set; }
    }
}
