using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.IRepositories;
using WebApp.Domain.ListTasks;
using WebApp.Infragtructure;
using AppContext = WebApp.Infragtructure.AppContext;

namespace WebApp.Infrastructure.Repositories
{
    public class ListTaskRepository : RepositoryBase<ListTask>, IListTaskRepository
    {
        public ListTaskRepository(AppContext appContext) : base(appContext)
        {

        }
    }
}
