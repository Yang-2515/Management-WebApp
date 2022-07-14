using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Boards;
using WebApp.Domain.IRepositories;
using WebApp.Domain.ListTasks;
using WebApp.Domain.Users;
using WebApp.Infragtructure;
using AppContext = WebApp.Infragtructure.AppContext;

namespace WebApp.Infrastructure.Repositories
{
    public class BoardRepository : RepositoryBase<Board>, IBoardRepository
    {
        private readonly DbSet<Board> _dbSet;
        private readonly AppContext _appContext;
        public BoardRepository(AppContext appContext) : base(appContext)
        {
            _appContext = appContext;
            _dbSet = appContext.Set<Board>();
        }
        public async Task UpdateBoardAsync(int id, string name, string des)
        {
            var board = await _dbSet.FindAsync(id);
            board.Update(name, des);
            //await _appContext.SaveEntitiesAsync();
        }
    }
}
