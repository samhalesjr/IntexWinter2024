using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IntexWinter2024.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int Year { get; set; }

        [Column("NumParts")]
        public int? NumParts { get; set; } // Made nullable in case not all products have a parts count

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(2048)]
        public string ImgLink { get; set; }

        [StringLength(100)]
        public string PrimaryColor { get; set; }

        [StringLength(100)]
        public string SecondaryColor { get; set; }

        [StringLength(3000)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Category { get; set; }
    }
}
