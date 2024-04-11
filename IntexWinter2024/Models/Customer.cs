using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Identity;

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

        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //public virtual User User { get; set; }


        // Foreign key for ASP.NET Core Identity User
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
