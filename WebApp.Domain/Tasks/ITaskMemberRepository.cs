using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain.Interfaces;

namespace WebApp.Domain.Tasks
{
    public interface ITaskMemberRepository : IAsyncRepository<TaskMember>
    {
    }
}
