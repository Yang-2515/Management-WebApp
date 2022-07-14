using WebApp.API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Interfaces;
using WebApp.Domain.IRepositories;
using Task = System.Threading.Tasks.Task;
using WebApp.Domain.Boards;
using AutoMapper;
using WebApp.API.DTOs.Boards;
using WebApp.Domain.ListTasks;
using WebApp.API.DTOs.ListTasks;
using System.Linq;
using WebApp.Domain.Users;
using WebApp.API.DTOs.Users;
using WebApp.API.DTOs.Tasks;
using TaskEntity = WebApp.Domain.Tasks.Task;

namespace WebApp.Service
{
    public class BoardService: BaseService
    {
        private readonly IBoardManager _boardManager;
        private readonly IMapper _mapper;
        public BoardService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IBoardManager boardManager) : base(unitOfWork)
        {
            _boardManager = boardManager;
            _mapper = mapper;
        }

        public async Task AddBoardAsync(AddBoard board, int userId)
        {
            try
            {
                await UnitOfWork.BeginTransaction(); 
                await _boardManager.AddBoardAsync(_mapper.Map<AddBoard, Board>(board), userId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task<GetBoardResponse> GetBoardAsync(int boardId, long userId)
        {
            var board = await _boardManager.GetBoardAsync(boardId, Convert.ToInt32(userId));
            GetBoardResponse getBoardResponse = new GetBoardResponse();
            getBoardResponse.Board = _mapper.Map<Board, BoardResponse>(board);
            if (board != null)
            {
                getBoardResponse.ListTasks = _mapper.Map<List<ListTask>, List<ListTaskReponseGetBoard>>(board.ListTasks.ToList());
                foreach (var item in getBoardResponse.ListTasks)
                {
                    item.CoutTask = board.ListTasks.Count(c => c.Id == item.Id);
                }
                List<User> users = new List<User>();
                foreach (var item in board.BoardMembers)
                {
                    users.Add(item.User);
                }
                getBoardResponse.Members = _mapper.Map<List<User>, List<AddUserResponse>>(users);
            }
            return getBoardResponse;
        }
        public async Task UpdateBoard(UpdateBoardRequest updateBoardRequest)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _boardManager.UpdateBoardAsync(_mapper.Map<UpdateBoardRequest, Board>(updateBoardRequest));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task DeleteBoard(int boardId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _boardManager.DeleteBoardAsync(boardId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task AddBoardMember(AddBoardMemberRequest boardMember)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _boardManager.AddBoardMemberAsync(_mapper.Map<AddBoardMemberRequest, BoardMember>(boardMember));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task DeleteByUserIdAndBoardTd(int userId, int boardId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _boardManager.DeleteBoardMemberAsync(userId, boardId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task AddListTaskAsync(AddListTaskRequest listTaskRequest)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _boardManager.AddListTaskAsync(_mapper.Map<AddListTaskRequest, ListTask>(listTaskRequest));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task UpdateListTaskAsync(UpdateListTaskRequest updateListTaskRequest)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _boardManager.UpdateListTaskAsync(_mapper.Map<UpdateListTaskRequest, ListTask>(updateListTaskRequest));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<GetListTaskResponse> GetListTaskAsync(int id)
        {
            var listTask = await _boardManager.GetListTaskAsync(id);
            GetListTaskResponse getListTaskResponse = new GetListTaskResponse();
            getListTaskResponse.ListTask = _mapper.Map<ListTask, ListTaskResponse>(listTask);
            if (listTask != null)
            {
                getListTaskResponse.Tasks = _mapper.Map<List<TaskEntity>, List<TaskResponseGetListTask>>(listTask.Tasks.ToList());
                foreach (var item in getListTaskResponse.Tasks)
                {
                    var task = listTask.Tasks.FirstOrDefault(c => c.Id == item.Id);
                    item.CountAttackment = task.CountAttackment();
                    item.CountToDo = task.CountToDo();
                    item.ToDos = item.ToDos.Where(c => c.ParentId == 0).ToList();
                }
            }
            return getListTaskResponse;
        }

        public async Task DeleteListTaskAsync(int id)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _boardManager.DeleteListTaskAsync(id);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
