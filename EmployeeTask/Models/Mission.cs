using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.Models
{
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Description { get; set; }

        public DateTime DueDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; } = "In Progress";

        [Range(1, int.MaxValue, ErrorMessage = "Please select a employee.")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
