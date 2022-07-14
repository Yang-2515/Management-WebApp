using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using WebApp.API.DTOs.Tasks;
using WebApp.Domain.Interfaces;
using WebApp.Domain.Tasks;
using TaskEntity = WebApp.Domain.Tasks.Task;
using System.Threading.Tasks;
using WebApp.Domain.ToDos;
using WebApp.API.DTOs.ToDos;
using WebApp.Domain.Labels;
using WebApp.API.DTOs.Labels;
using WebApp.API.DTOs.Attackments;
using WebApp.Domain.Users;
using WebApp.API.DTOs.Users;

namespace WebApp.API.Services.Tasks
{
    public class TaskService: BaseService
    {
        private readonly ITaskManager _taskManager;
        private readonly IMapper _mapper;
        public TaskService(IUnitOfWork unitOfWork,
            ITaskManager taskManager,
            IMapper mapper) : base(unitOfWork)
        {
            _taskManager = taskManager;
            _mapper = mapper;
        }
        public async Task AddTaskAsync(AddTaskRequest taskRequest)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.AddTaskAsync(_mapper.Map<AddTaskRequest, TaskEntity>(taskRequest));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<GetTaskResponse> GetTaskAsync(int id)
        {
            var task = await _taskManager.GetTaskAsync(id);
            GetTaskResponse getTaskResponse = new GetTaskResponse();
            getTaskResponse = _mapper.Map<TaskEntity, GetTaskResponse>(task);
            if (task != null)
            {
                List<ToDoResponseGetTask> toDoResponseGetTasks = new List<ToDoResponseGetTask>();
                foreach (var item in task.ToDos.Where(c => c.TaskId == id && c.ParentId == 0).ToList())
                {
                    ToDoResponseGetTask toDo = new ToDoResponseGetTask();
                    toDo.ToDo = _mapper.Map<ToDo, ToDoResponse>(item);
                    toDo.ToDos = _mapper.Map<List<ToDo>, List<ToDoResponse>>(task.ToDos.Where(c => c.TaskId == id && c.ParentId == item.Id).ToList());
                    toDoResponseGetTasks.Add(toDo);
                }
                getTaskResponse.ListToDos = toDoResponseGetTasks;
                getTaskResponse.Attackments = _mapper.Map<List<Attackment>, List<AttackmentResponse>>(task.GetAttackments());
                List<Label> labels = new List<Label>();
                foreach (var item in task.TaskLabels.ToList())
                {
                    labels.Add(item.Label);
                }
                getTaskResponse.Labels = _mapper.Map<List<Label>, List<LabelResponse>>(labels);
                List<User> users = new List<User>();
                foreach (var item in task.TaskMembers.ToList())
                {
                    users.Add(item.User);
                }
                getTaskResponse.Members = _mapper.Map<List<User>, List<AddUserResponse>>(users);
            }
            return getTaskResponse;
        }

        public async Task UpdateListTaskIdAsync(int taskId, int listTaskId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.UpdateListTaskId(taskId, listTaskId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async  Task UpdateTaskAsync(UpdateTaskRequest updateTaskRequest)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.UpdateTaskAsync(_mapper.Map<UpdateTaskRequest, TaskEntity>(updateTaskRequest));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task UpdateAssigneeAsync(int taskId, int assigneeId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.UpdateAssigneeIdAsync(taskId, assigneeId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.DeleteTaskAsync(id);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task UpdatePriorityAsync(int taskId, string priority)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.UpdatePriority(taskId, priority);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task AddTaskMemberAsync(AddTaskMemberRequest taskMember)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.AddTaskMemberAsync(_mapper.Map<AddTaskMemberRequest, TaskMember>(taskMember));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task DeleteTaskMemberByUserIdAndTaskTdAsync(int userId, int taskId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.DeleteTaskMemberAsync(userId, taskId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task AddTaskLabelAsync(AddTaskLabelRequest taskLabel)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.AddtaskLabelAsync(_mapper.Map<AddTaskLabelRequest, TaskLabel>(taskLabel));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task DeleteLabelByLabelIdAndTaskIdAsync(int labelId, int taskId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.DeleteLabelByLabelIdAndTaskIdAsync(labelId, taskId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task AddAttackmentAsync(AddAttackmentRequest attackment)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.AddAttackmentAsync(_mapper.Map<AddAttackmentRequest, Attackment>(attackment));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task DeleteAttackmentAsync(int attackmentId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.DeleteAttackmentAsync(attackmentId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task AddToDoAsync(AddToDoRequest toDoRequest)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.AddToDoAsync(_mapper.Map<AddToDoRequest, ToDo>(toDoRequest));

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task DeleteToDoAsync(int id)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.DeleteToDoAsync(id);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task UpdateNameToDoAsync(int toDoId, string name)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.UpdateNameToDoAsync(toDoId, name);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task UpdateIsCompleteToDoAsync(int toDoId, bool isComplete)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _taskManager.UpdateIsCompleteToDoAsync(toDoId, isComplete);

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
