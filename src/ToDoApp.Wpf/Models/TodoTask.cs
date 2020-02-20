using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Wpf.Models
{
    class TodoTask
    {
        public bool IsCompleted { get; set; }

        public string Description { get; set; }

        public DateTime DueBy { get; set; }
        
        public DateTime CompletedOn { get; set; }
    }
}
