using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.IRepositories;
using WebApp.Domain.ListTasks;

namespace WebApp.Domain.Boards
{
    public class BoardManager: DomainService, IBoardManager
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IListTaskRepository _listTaskRepository;
        private readonly IBoardMemberRepository _boardMemberRepository;
        public BoardManager(IBoardRepository boardRepository,
            IBoardMemberRepository boardMemberRepository,
            IListTaskRepository listTaskRepository)
        {
            _boardRepository = boardRepository;
            _boardMemberRepository = boardMemberRepository;
            _listTaskRepository = listTaskRepository;
        }

        public async Task AddBoardAsync(Board board, int userId)
        {
            var boardAdd = await _boardRepository.AddAsync(board);
            BoardMember boardMember = new BoardMember();
            boardMember.BoardId = board.Id;
            boardMember.UserId = userId;
            await _boardMemberRepository.AddAsync(boardMember);
            await _listTaskRepository.AddAsync(new ListTask("ToDo", "Basic", boardAdd.Id));
        }

        public async Task AddBoardMemberAsync(BoardMember boardMember)
        {
            await _boardMemberRepository.AddAsync(boardMember);
        }

        public async Task AddListTaskAsync(ListTask listTask)
        {
            await _listTaskRepository.AddAsync(listTask);
        }

        public async Task DeleteBoardAsync(int boardId)
        {
            var board = await _boardRepository.FindAsync(boardId);
            await _boardRepository.DeleteAsync(board);
        }

        public async Task DeleteBoardMemberAsync(int userId, int boardId)
        {
            await _boardMemberRepository.DeleteAsync(await _boardMemberRepository.GetAsync(c => c.BoardId == boardId && c.UserId == userId));
        }

        public async Task DeleteListTaskAsync(int id)
        {
            var listTask = await _listTaskRepository.FindAsync(id);
            await _listTaskRepository.DeleteAsync(listTask);
        }

        public async Task<Board> GetBoardAsync(int boardId, int userId)
        {
            return await _boardRepository.GetAsync(c => c.Id == boardId && c.UserId == userId);
        }

        public async Task<ListTask> GetListTaskAsync(int id)
        {
            return await _listTaskRepository.FindAsync(id);
        }

        public async Task UpdateBoardAsync(Board board)
        {
            var boardUpdate = await _boardRepository.FindAsync(board.Id);
            boardUpdate.Update(board.Name, board.Description);
        }

        public async Task UpdateListTaskAsync(ListTask listTask)
        {
            var listTaskUpdate = await _listTaskRepository.FindAsync(listTask.Id);
            var board = await _boardRepository.FindAsync(listTaskUpdate.BoardId);
            board.UpdateListTask(listTask.Id, listTask.Name, listTask.Color);
        }
    }
}
