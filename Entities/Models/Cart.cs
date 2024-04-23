namespace Entities.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; }
        public Cart()
        {
            Lines = new List<CartLine>();
        }

        public virtual void AddItem(Product product, int quantity) //we use virtual so we can override the method
        {
            CartLine? line = Lines.Where(l => l.Product.ProductId.Equals(product.ProductId)) //l represents each cartline inside the lines list.
            .FirstOrDefault(); //we control if there's a product with the same id number

            if (line is null) //if there's no such product
            {
                Lines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }


        public virtual void RemoveLine(Product product) =>
        Lines.RemoveAll(l => l.Product.ProductId.Equals(product.ProductId));


        public decimal ComputeTotalValue() => 
        Lines.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => Lines.Clear();

    }
}