namespace E_commerce.Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }

        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}