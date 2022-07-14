using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Base;

namespace WebApp.Domain.Users
{
    public class AddUserEvent: BaseDomainEvent
    {
        public User User { get; set; }
        public AddUserEvent(User user)
        {
            User = user;
        }
    }
}
