using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string? Name { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        // Nav props:
        public ICollection<ProjectTask>? Tasks { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
