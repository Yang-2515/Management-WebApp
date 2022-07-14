using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TaskThead = System.Threading.Tasks.Task;
using TaskEntity = WebApp.Domain.Tasks.Task;
using System.Threading.Tasks;
using WebApp.Domain.ToDos;

namespace WebApp.Domain.Tasks
{
    public class TaskManager : DomainService, ITaskManager
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskMemberRepository _taskMemberRepository;
        private readonly ITaskLabelRepository _taskLabelRepository;
        private readonly IAttackmentRepository _attackmentRepository;
        private readonly IToDoRepository _toDoRepository;

        public TaskManager(ITaskRepository taskRepository,
            ITaskMemberRepository taskMemberRepository,
            ITaskLabelRepository taskLabelRepository,
            IAttackmentRepository attackmentRepository,
            IToDoRepository toDoRepository)
        {
            _taskRepository = taskRepository;
            _taskMemberRepository = taskMemberRepository;
            _taskLabelRepository = taskLabelRepository;
            _attackmentRepository = attackmentRepository;
            _toDoRepository = toDoRepository;
        }

        public async TaskThead AddAttackmentAsync(Attackment attackment)
        {
            await _attackmentRepository.AddAsync(attackment);
        }

        public async TaskThead AddTaskAsync(TaskEntity task)
        {
            await _taskRepository.AddAsync(task);
        }

        public async TaskThead AddtaskLabelAsync(TaskLabel taskLabel)
        {
            await _taskLabelRepository.AddAsync(taskLabel);
        }

        public async TaskThead AddTaskMemberAsync(TaskMember taskMember)
        {
            await _taskMemberRepository.AddAsync(taskMember);
        }

        public async TaskThead AddToDoAsync(ToDo toDo)
        {
            await _toDoRepository.AddAsync(toDo);
        }

        public async TaskThead DeleteAttackmentAsync(int attackmentId)
        {
            await _attackmentRepository.DeleteAsync(await _attackmentRepository.FindAsync(attackmentId));
        }

        public async TaskThead DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.FindAsync(id);
            await _taskRepository.DeleteAsync(task);
        }

        public async TaskThead DeleteLabelByLabelIdAndTaskIdAsync(int labelId, int taskId)
        {
            await _taskLabelRepository.DeleteAsync(await _taskLabelRepository.GetAsync(c => c.LabelId == labelId && c.TaskId == taskId));
        }

        public async TaskThead DeleteTaskMemberAsync(int userId, int taskId)
        {
            await _taskMemberRepository.DeleteAsync(await _taskMemberRepository.GetAsync(c => c.TaskId == taskId && c.UserId == userId));
        }

        public async TaskThead DeleteToDoAsync(int id)
        {
            var todo = await _toDoRepository.FindAsync(id);
            await _toDoRepository.DeleteAsync(todo);
        }

        public async Task<TaskEntity> GetTaskAsync(int id)
        {
            return await _taskRepository.FindAsync(id);
        }

        public async TaskThead UpdateAssigneeIdAsync(int taskId, int assigneeId)
        {
            var task = await _taskRepository.FindAsync(taskId);
            task.UpdateAsigneeId(assigneeId);
        }

        public async TaskThead UpdateListTaskId(int taskId, int listTaskId)
        {
            var task = await _taskRepository.FindAsync(taskId);
            task.UpdateListTaskId(listTaskId);
        }

        public async TaskThead UpdateNameToDoAsync(int toDoId, string name)
        {
            var todo = await _toDoRepository.FindAsync(toDoId);
            var task = await _taskRepository.FindAsync(todo.TaskId);
            task.UpdateNameToDo(toDoId, name);
        }
        public async TaskThead UpdateIsCompleteToDoAsync(int toDoId, bool isComplete)
        {
            var todo = await _toDoRepository.FindAsync(toDoId);
            var task = await _taskRepository.FindAsync(todo.TaskId);
            task.UpdateIsCompleteToDo(toDoId, isComplete);
        }

        public async TaskThead UpdatePriority(int taskId, string priority)
        {
            var task = await _taskRepository.FindAsync(taskId);
            task.UpdatePriority(priority);
        }

        public async TaskThead UpdateTaskAsync(TaskEntity task)
        {
            var taskUpdate = await _taskRepository.FindAsync(task.Id);
            taskUpdate.Update(task.Name, task.Description, task.StartDate, task.DueDate, task.Priority, task.UserId);
        }

    }
}
