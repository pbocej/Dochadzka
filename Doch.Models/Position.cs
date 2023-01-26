using System.ComponentModel.DataAnnotations;

namespace Doch.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        [Required]
        [StringLength(50)]
        public string PositionName { get; set; }
    }
}
