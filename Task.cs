using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkCoreH3
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Todo> Todo { get; } = new();
    }
}