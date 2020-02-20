using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoApp.Wpf.Models;

namespace ToDoApp.Wpf.ViewModels
{
    class TodoTaskViewModel
    {
        private TodoTask TodoTask { get; }

        public TodoTaskViewModel(TodoTask todoTask)
        {
            this.TodoTask = todoTask;
        }

        public string Description
        {
            get { return TodoTask.Description; }
            set
            {
                TodoTask.Description = value;
                // todo: NotifyPropertyChanged(nameof(Description));
            }
        }

        public DateTime DueBy
        {
            get { return TodoTask.DueBy; }
            set
            {
                TodoTask.DueBy = DateTime.Now.AddDays(7); ;
            }
        }

        public DateTime CompletedOn
        {
            get { return TodoTask.CompletedOn; }
            set
            {
                TodoTask.CompletedOn = value;
            }
        }

        public bool IsCompleted
        {
            get { return TodoTask.IsCompleted; }
            set { TodoTask.IsCompleted = value;
            //todo assign completed date when value==true
            if(value)
                {
                    CompletedOn = DateTime.Now;
                }
            }
        }
    }
}
