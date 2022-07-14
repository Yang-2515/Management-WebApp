using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.ListTasks;

namespace WebApp.Domain.Boards
{
    public interface IBoardManager : IDomainService
    {
        Task AddBoardAsync(Board board, int userId);
        Task<Board> GetBoardAsync(int boardId, int userId);
        Task UpdateBoardAsync(Board board);
        Task DeleteBoardAsync(int boardId);
        Task AddBoardMemberAsync(BoardMember boardMember);
        Task DeleteBoardMemberAsync(int userId, int boardId);
        Task AddListTaskAsync(ListTask listTask);
        Task UpdateListTaskAsync(ListTask listTask);
        Task<ListTask> GetListTaskAsync(int id);
        Task DeleteListTaskAsync(int id);
    }
}
