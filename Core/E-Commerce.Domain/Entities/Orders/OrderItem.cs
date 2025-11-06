namespace E_Commerce.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItems product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductInOrderItems Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}