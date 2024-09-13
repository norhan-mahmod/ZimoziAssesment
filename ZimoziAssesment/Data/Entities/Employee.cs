using System.ComponentModel.DataAnnotations;

namespace ZimoziAssesment.Data.Entities
{
    public class Employee 
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Department { get; set; }
        [Range(0 , double.MaxValue , ErrorMessage = " Salary Must be a Positive Number")]
        public double Salary { get; set; }
    }
}
