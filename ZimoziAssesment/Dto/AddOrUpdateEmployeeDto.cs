using System.ComponentModel.DataAnnotations;

namespace ZimoziAssesment.Dto
{
    public class AddOrUpdateEmployeeDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Department { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = " Salary Must be a Positive Number")]
        public double Salary { get; set; }
    }
}
