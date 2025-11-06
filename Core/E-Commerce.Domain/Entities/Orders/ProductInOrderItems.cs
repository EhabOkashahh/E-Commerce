namespace E_Commerce.Domain.Entities.Orders
{
    public class ProductInOrderItems
    {
        public ProductInOrderItems()
        {
            
        }
        public ProductInOrderItems(int productId, string productName, string productUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductUrl = productUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
    }
}