using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ToDoItem
    {
        public int Id { get; set; } 

        public string? TaskName { get; set; }
        public string? TaskDescription { get; set; }

        public bool Done { get; set; }
    }
}