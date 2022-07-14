using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Domain.Users
{
    public partial class RefreshToken
    {
        public void Update(bool used)
        {
            Used = used;
        }
    }
}
