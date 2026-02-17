using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkCoreH3
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CurrentTaskId { get; set; }
        public Task? CurrentTask { get; set; } = null!;
        public List<Worker> Workers { get; } = new();
    }
}
