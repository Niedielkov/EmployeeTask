using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTask.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email.")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "Salary should be more than 0.")]
        public int MonthlySalary { get; set; }

        [NotMapped]
        public string? FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public string? FormatedSalary
        {
            get
            {
                return $"${MonthlySalary}";
            }
        }
    }
}
