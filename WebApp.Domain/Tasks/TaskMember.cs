﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApp.Domain.Base;
using WebApp.Domain.Tasks;
using WebApp.Domain.Users;

namespace WebApp.Domain.Tasks
{
    [Table("TaskMember")]
    public partial class TaskMember : EntityBase<int>
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        public virtual User User { get; set; }
    }
}
