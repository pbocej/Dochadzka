using System.ComponentModel.DataAnnotations;

namespace Doch.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string SurName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int PositionId { get; set; }
        public Position Position { get; set; }
        [Required]
        [MaxLength(50)]
        public string IpAddress { get; set; }
        public string IpCountryCode { get; set; }
    }
}
