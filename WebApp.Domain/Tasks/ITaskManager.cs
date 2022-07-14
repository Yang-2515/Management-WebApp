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
    public interface ITaskManager : IDomainService
    {
        TaskThead AddTaskAsync(TaskEntity task);
        Task<TaskEntity> GetTaskAsync(int id);
        TaskThead UpdateListTaskId(int taskId, int listTaskId);
        TaskThead UpdateTaskAsync(TaskEntity task);
        TaskThead UpdateAssigneeIdAsync(int taskId, int assigneeId);
        TaskThead DeleteTaskAsync(int id);
        TaskThead UpdatePriority(int taskId, string priority);
        TaskThead AddTaskMemberAsync(TaskMember taskMember);
        TaskThead DeleteTaskMemberAsync(int userId, int taskId);
        TaskThead AddtaskLabelAsync(TaskLabel taskLabel);
        TaskThead DeleteLabelByLabelIdAndTaskIdAsync(int labelId, int taskId);
        TaskThead AddAttackmentAsync(Attackment attackment);
        TaskThead DeleteAttackmentAsync(int attackmentId);
        TaskThead AddToDoAsync(ToDo toDo);
        TaskThead DeleteToDoAsync(int id);
        TaskThead UpdateNameToDoAsync(int toDoId, string name);
        TaskThead UpdateIsCompleteToDoAsync(int toDoId, bool isComplete);
    }
}
