using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkCoreH3
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CurrentTodoId { get; set; }
        public Todo? CurrentTodo { get; set; } = null!;
        public List<Team> Teams { get; } = new();
    }
}
