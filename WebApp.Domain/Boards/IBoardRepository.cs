using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Interfaces;
using WebApp.Domain.Boards;

namespace WebApp.Domain.IRepositories
{
    public interface IBoardRepository : IAsyncRepository<Board>
    {
    }
}
