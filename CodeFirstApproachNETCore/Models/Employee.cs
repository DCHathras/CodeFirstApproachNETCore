using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstApproachNETCore.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column("EmployeeName", TypeName = "varchar(50)")]
        [Required]
        public string Name { get; set; }

        [Column("EmpDesignation", TypeName = "varchar(50)")]
        [Required]
        public string Designation { get; set; }

        [Column("EmpPhoneNumber")]
        [Required]
        public int? PhoneNumber { get; set; }

        [Required]
        public int? Age { get; set; }    
    }
}
