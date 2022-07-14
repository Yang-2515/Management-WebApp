using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Boards;
using WebApp.Domain.Tasks;

namespace WebApp.Domain.Users
{
    [Table("User")]
    public partial class User : EntityBase<int>
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [Column("Email Address")]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [Column("Home Address")]
        [StringLength(50)]
        public string HomeAddress { get; set; }
        public int Age { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(100)]
        public string LinkImage { get; set; }
        public string Username { get; set; }
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(10)]
        public string FirstSecurityString { get; set; }

        [StringLength(10)]
        public string LastSecurityString { get; set; }
        public virtual ICollection<TaskMember> TaskMembers { get; set; }
        public virtual ICollection<BoardMember> BoardMembers { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
