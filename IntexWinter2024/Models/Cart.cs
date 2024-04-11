using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace IntexWinter2024.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Product p, int quantity)
        {
            CartLine? line = Lines
                .Where(x => x.Product.ProductId == p.ProductId)
                .FirstOrDefault();

            // Has the item already been added to our cart?
            // If not, create a new line item
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = p,
                    Quantity = quantity
                });
            }
            // If yes, add to the quantity
            else
            {
                line.Quantity += quantity;
            }
        }

        //Remove an item from the cart
        public virtual void RemoveLine(Product p) => Lines.RemoveAll(x => x.Product.ProductId == p.ProductId);

        //Clear the cart
        public virtual void ClearCart() => Lines.Clear();

        //Get the sum of price x quantity to get the cart total
        public decimal CalculateTotal() => Lines.Sum(x => x.Product.Price * x.Quantity);

        public class CartLine
        {
            public int CartLineId { get; set; }
            public Product? Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
