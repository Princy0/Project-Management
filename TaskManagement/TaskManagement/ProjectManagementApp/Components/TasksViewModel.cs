using ProjectManagement.Entities;

namespace ProjectManagementApp.Components
{
    public class TasksViewModel
    {
        public List<ProjectTask>? Tasks { get; set; }
        public int NumberOfTasksToDisplay { get; set; }
    }
}
