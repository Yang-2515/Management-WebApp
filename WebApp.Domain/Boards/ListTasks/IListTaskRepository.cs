using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Users;
using WebApp.Domain.Interfaces;
using WebApp.Domain.ListTasks;

namespace WebApp.Domain.IRepositories
{
    public interface IListTaskRepository : IAsyncRepository<ListTask>
    {
    }
}
