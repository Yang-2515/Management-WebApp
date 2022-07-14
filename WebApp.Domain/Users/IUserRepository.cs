using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Interfaces;

namespace WebApp.Domain.Users
{
    public interface IUserRepository: IAsyncRepository<User>
    {
    }
}
