namespace SportsStore.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = Lines.Where(l => l.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                line = new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                };
                Lines.Add(line);
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        public decimal CalcTotalValue() => Lines.Sum(l => l.Product.Price * l.Quantity);
        public virtual void Clear() => Lines.Clear();
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; } = new Product();
        public int Quantity {  get; set; }

    }
}
