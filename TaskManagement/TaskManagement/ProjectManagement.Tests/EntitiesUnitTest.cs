using ProjectManagement.Entities;

namespace ProjectManagement.Tests
{
    public class EntitiesUnitTest
    {
        [Fact]
        public void TestNumberOfOverdueTasks()
        {
            Project p1 = new Project() { 
                Name = "Test project #1",
                Tasks = new List<ProjectTask>()
            };

            p1.Tasks.Add(new ProjectTask() { DueDate = new DateTime(2020, 8, 5), Description = "Get milk" });
            p1.Tasks.Add(new ProjectTask() { DueDate = new DateTime(2021, 11, 24), Description = "Do laundry" });
            p1.Tasks.Add(new ProjectTask() { DueDate = new DateTime(2023, 2, 9), Description = "Learn C#" });
            p1.Tasks.Add(new ProjectTask() { DueDate = new DateTime(2023, 5, 8), Description = "Learn ASP.NET Core MVC" });
            p1.Tasks.Add(new ProjectTask() { DueDate = new DateTime(2023, 5, 31), Description = "Learn Git" });

            DateTime now = DateTime.Now;
            int numOverdueTasks = p1.Tasks.Where(pt => pt.DueDate < now).Count();

            Assert.Equal(2, numOverdueTasks);
        }
    }
}