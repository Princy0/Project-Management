using ProjectManagement.Entities;

namespace ProjectManagementApp.Models
{
    public class ProjectDetailsViewModel
    {
        public Project? ActiveProject { get; set; }
        public Employee? NewEmployee { get; set; }
        public ProjectTask? NewProjectTask { get; set; }
        public int InProgressTaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public int CancelledTaskCount { get; set; }
    }
}
