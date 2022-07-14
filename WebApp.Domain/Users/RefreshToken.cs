using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Base;

namespace WebApp.Domain.Users
{
    public partial class RefreshToken: EntityBase<int>
    {
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
