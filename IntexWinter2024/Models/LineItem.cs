using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntexWinter2024.Models
{
    public class LineItem
    {
        [Key]
        public int LineItemId { get; set; }

        [ForeignKey("Order")]
        public int TransactionId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Qty { get; set; }

        // Nullable if you want to allow items without a rating
        public int? Rating { get; set; }

        // Navigation properties
        public virtual Order Order{ get; set; }
        public virtual Product Product { get; set; }
    }
}
