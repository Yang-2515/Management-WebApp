using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;

namespace WebApp.Domain.Histories
{
    [Table("History")]
    public partial class History : EntityBase<int>
    {
        [StringLength(50)]
        public string Action { get; set; }
        [StringLength(50)]
        public string Message { get; set; }
        public string ContentJSON { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreateAt { get; set; }
        public int? CreateBy { get; set; }
        public string RefType { get; set; }
        public int RefId { get; set; }
    }
}
