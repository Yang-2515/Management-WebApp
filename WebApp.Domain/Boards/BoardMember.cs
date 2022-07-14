using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Boards;
using WebApp.Domain.Users;

namespace WebApp.Domain.Boards
{
    [Table("BoardMember")]
    public partial class BoardMember : EntityBase<int>
    {
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? BoardId { get; set; }
        public virtual Board Board { get; set; }
    }
}
