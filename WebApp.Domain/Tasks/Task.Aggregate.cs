using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Domain.ToDos;

namespace WebApp.Domain.Tasks
{
    public partial class Task
    {
        public int CountAttackment()
        {
            return Attackments.Count();
        }
        public int CountToDo()
        {
            return ToDos.Count();
        }
        public List<ToDo> GetToDos()
        {
            List<ToDo> toDos = new List<ToDo>();
            foreach (var listTask in ToDos)
            {
                toDos.Add(listTask);
            }
            return toDos;
        }
        public List<Attackment> GetAttackments()
        {
            List<Attackment> attackments = new List<Attackment>();
            foreach (var attackment in Attackments)
            {
                attackments.Add(attackment);
            }
            return attackments;
        }
        public void Update(string name, string description, DateTime? startDate, DateTime? dueDate, string priority, int? userId)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            DueDate = dueDate;
            Priority = priority;
            UserId = userId;
        }
        public void UpdateListTaskId(int listTaskId)
        {
            ListTaskId = listTaskId;
        }
        public void UpdateAsigneeId(int assigneeId)
        {
            UserId = assigneeId;
        }

        public void UpdatePriority(string priority)
        {
            Priority = priority;
        }
        public void UpdateNameToDo(int todoId, string name)
        {
            var toDo = ToDos.FirstOrDefault(c => c.Id == todoId);
            toDo.Name = name;
        }
        public void UpdateIsCompleteToDo(int todoId, bool isComplete)
        {
            var toDo = ToDos.FirstOrDefault(c => c.Id == todoId);
            toDo.IsComplete = isComplete;
        }
    }
}
