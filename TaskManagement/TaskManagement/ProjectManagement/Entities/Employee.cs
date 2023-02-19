using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Please Enter the Employee's Number .")]
        [RegularExpression(@"[A-Za-z]{3}-[0-9]{6}", ErrorMessage = "In-Valid Employee's Number!!")]

        public string? EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Please Enter the Employee's First Name.")]

        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter the Student's Last Name.")]

        public string? LastName { get; set; }

        public string? FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

        // FK:
        public int? ProjectId { get; set; }

        // And nav prop:
        public Project? Project { get; set; }
    }
}
