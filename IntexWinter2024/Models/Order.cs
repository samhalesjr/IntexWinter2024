using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IntexWinter2024.Models
{
    public class Order
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        //[DataType(DataType.Date)]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string DayOfWeek { get; set; }

        //public int Time { get; set; } = 0;

        public string EntryMode { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }

        public string TypeOfTransaction { get; set; }

        public string CountryOfTransaction { get; set; }

        [StringLength(1024)]
        public string ShippingAddress { get; set; }

        public string Bank { get; set; }

        public string TypeOfCard { get; set; }

        public bool Fraud { get; set; }

        // Navigation property to the Customer
        public virtual Customer Customer { get; set; }
    }
}
