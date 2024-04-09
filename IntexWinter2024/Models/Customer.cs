using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IntexWinter2024.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [StringLength(255)]
        public string CountryOfResidence { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public bool MfaEnabled { get; set; } = false;

        // Assuming Role and User are other entities related to Customer
        // This sets up a foreign key relationship in EF Core
        // Ensure that RoleId and UserId match the type expected by the related entities (e.g., int, Guid, etc.)

        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
