using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Labels;
using WebApp.Domain.Tasks;

namespace WebApp.Domain.Tasks
{
    [Table("TaskLabel")]
    public partial class TaskLabel : EntityBase<int>
    {
        public int TaskId { get; set; }
        public int LabelId { get; set; }
        public int Index { get; set; }
        public virtual Label Label { get; set; }
        public virtual Task Task { get; set; }
    }
}
