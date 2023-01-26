using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Doch.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("First name")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Last name")]
        public string SurName { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DisplayName("Position")]
        public int PositionId { get; set; }
        public Position? Position { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("IP Address")]
        public string IpAddress { get; set; }
        [DisplayName("IP Country Code")]
        public string IpCountryCode { get; set; }
    }
}
