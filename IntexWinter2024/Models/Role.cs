using System.ComponentModel.DataAnnotations;

namespace IntexWinter2024.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
