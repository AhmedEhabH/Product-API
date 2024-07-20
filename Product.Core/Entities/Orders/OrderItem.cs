namespace Product.Core.Entities.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered productItemOrder, decimal price, decimal quantity)
        {
            ProductItemOrder = productItemOrder;
            Price = price;
            Quantity = quantity;
        }

        public int OrderItemID { get; set; }
        public ProductItemOrdered ProductItemOrder { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}