using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Domain.ToDos
{
    public partial class ToDo
    {
        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateIsComplete(bool isComplete)
        {
            IsComplete = isComplete;
        }
    }
}
