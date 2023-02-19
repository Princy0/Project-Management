using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProjectManagement.Entities;
using ProjectManagementApp.Entities;

namespace ProjectManagementApp.DataAccess
{
    public class ProjectManagementDbContext : IdentityDbContext<User>
    {
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesame123#";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // call base class version to setup Indentity relations:
            base.OnModelCreating(modelBuilder);

            // Map enum values --> strings in DB based on enum name:
            modelBuilder.Entity<ProjectTask>()
                .Property(pt => pt.TaskStatus)
                .HasConversion<string>()
                .HasMaxLength(64);

            // Seed some Projects:
            modelBuilder.Entity<Project>().HasData(
                new Project() { ProjectId = 1, Name = "West parking lot expansion" },
                new Project() { ProjectId = 2, Name = "Cafeteria upgrade" }
            );

            // Seed some Employees:
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { EmployeeId = 1, EmployeeNumber = "17-3456-DF", FirstName = "Bart", LastName = "Simpson", ProjectId = 1 },
                new Employee() { EmployeeId = 2, EmployeeNumber = "99-7768-AB", FirstName = "Lisa", LastName = "Simpson", ProjectId = 1 },
                new Employee() { EmployeeId = 3, EmployeeNumber = "87-4559-FG", FirstName = "Maggie", LastName = "Simpson", ProjectId = 2 },
                new Employee() { EmployeeId = 4, EmployeeNumber = "11-4092-LM", FirstName = "Marge", LastName = "Simpson", ProjectId = 2 },
                new Employee() { EmployeeId = 5, EmployeeNumber = "57-5930-ZC", FirstName = "Homer", LastName = "Simpson", ProjectId = 2 }
            );

            // Seed some ProjectTasks:
            modelBuilder.Entity<ProjectTask>().HasData(
                new ProjectTask() { ProjectTaskId = 1, DueDate = new DateTime(2023, 2, 7), Description = "Excavate ground", ProjectId = 1 },
                new ProjectTask() { ProjectTaskId = 2, DueDate = new DateTime(2023, 2, 9), Description = "Paving", ProjectId = 1 },
                new ProjectTask() { ProjectTaskId = 3, DueDate = new DateTime(2023, 5, 8), Description = "Install new flooring", ProjectId = 2 },
                new ProjectTask() { ProjectTaskId = 4, DueDate = new DateTime(2023, 11, 24), Description = "Paint", ProjectId = 2 },
                new ProjectTask() { ProjectTaskId = 5, DueDate = new DateTime(2023, 12, 29), Description = "Install new tabels and chairs", ProjectId = 2 },
                new ProjectTask() { ProjectTaskId = 6, DueDate = new DateTime(2022, 5, 31), Description = "Survey surroundings", ProjectId = 1, TaskStatus = TaskStatusOptions.Completed },
                new ProjectTask() { ProjectTaskId = 7, DueDate = new DateTime(2022, 7, 17), Description = "Level ground", ProjectId = 1, TaskStatus = TaskStatusOptions.Completed },
                new ProjectTask() { ProjectTaskId = 8, DueDate = new DateTime(2022, 8, 5), Description = "Redo drop ceiling", ProjectId = 2, TaskStatus = TaskStatusOptions.Completed },
                new ProjectTask() { ProjectTaskId = 9, DueDate = new DateTime(2022, 8, 17), Description = "Replace lighting with LED bulbs", ProjectId = 2, TaskStatus = TaskStatusOptions.Completed }
            );
        }
    }
}
