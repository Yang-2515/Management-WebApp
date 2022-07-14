using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Domain.ListTasks;

namespace WebApp.Domain.Boards
{
    public partial class Board
    {

        public Board(string name, string description, int userId)
        {
            UserId = userId;
            this.Update(name, description);
        }
        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public List<ListTask> GetListTasks()
        {
            List<ListTask> listTasks = new List<ListTask>();
            foreach (var listTask in ListTasks)
            {
                listTasks.Add(listTask);
            }
            return listTasks;
        }
        public void UpdateListTask(int id, string name, string color)
        {
            var listTask = ListTasks.FirstOrDefault(c => c.Id == id);
            listTask.Name = name;
            listTask.Color = color;
        }
    }
}
