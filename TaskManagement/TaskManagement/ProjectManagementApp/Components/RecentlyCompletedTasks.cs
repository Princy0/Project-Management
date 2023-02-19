using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using ProjectManagementApp.DataAccess;

namespace ProjectManagementApp.Components
{
    public class RecentlyCompletedTasks : ViewComponent
    {
        public RecentlyCompletedTasks(ProjectManagementDbContext projectManagementDbContext)
        {
            _projectManagementDbContext = projectManagementDbContext;
        }

        public IViewComponentResult Invoke(int numberOfTasksToDisplay)
        {
            DateTime now = DateTime.Now;

            var projectTasks = _projectManagementDbContext.ProjectTasks
                    .Include(pt => pt.Project)
                    .Where(pt => pt.DueDate < now && pt.TaskStatus == TaskStatusOptions.Completed)
                    .OrderByDescending(pt => pt.DueDate)
                    .ToList();

            TasksViewModel recentlyCompletedTasksViewModel = new TasksViewModel() {
                Tasks = projectTasks,
                NumberOfTasksToDisplay = numberOfTasksToDisplay
            };

            return View(recentlyCompletedTasksViewModel);
        }

        private ProjectManagementDbContext _projectManagementDbContext;
    }
}
