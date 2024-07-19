namespace Product.Core.Entities.Orders
{
    public class OrderItems : BaseEntity<int>
    {
        public OrderItems()
        {
            
        }
        public OrderItems(ProductItemOrdered productItemOrder, decimal price, decimal quantity)
        {
            ProductItemOrder = productItemOrder;
            Price = price;
            Quantity = quantity;
        }

        public int OrderItemsID { get; set; }
        public ProductItemOrdered ProductItemOrder { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}