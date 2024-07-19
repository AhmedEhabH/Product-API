using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Core.Entities.Orders;
using Product.Core.Interface;
using Product.Core.Services;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repository
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderServices> _logger;

        public OrderServices(IUnitOfWork uow, ApplicationDbContext context, ILogger<OrderServices> logger)
        {
            _uow = uow;
            _context = context;
            _logger = logger;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShipAddress shipAddress)
        {
            _logger.LogInformation("In CreateOrderAsync");

            var basket = await _uow.BasketRepository.GetBasketAsync(basketId);
            _logger.LogInformation($"Basket:\n{basket}");
            _logger.LogInformation($"Basket.BasketItems:\n{basket.BasketItems}");

            var items = new List<OrderItems>();

            foreach (var item in basket.BasketItems)
            {
                var productItem = await _uow.ProductRepository.GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem?.ProductPicture);
                var orderItem = new OrderItems(productItemOrdered, item.Price, item.Quantity);
                items.Add(orderItem);
            }
            _logger.LogInformation($"items.Count:\n{items.Count}");
            _logger.LogInformation($"items:\n{items}");

            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"First _context.SaveChangesAsync()");

            var deliveryMethod = await _context.DeliveryMethods.Where(x => x.ID == deliveryMethodId).FirstOrDefaultAsync();

            var subtotal = items.Sum(x => x.Price * x.Quantity);

            var order = new Order(buyerEmail, shipAddress, deliveryMethod, items, subtotal);

            _logger.LogInformation($"Order:{order}");

            if (order is null)
            {
                return null;
            }

            _logger.LogInformation($"Order.OrderId:{order.Id}");
            _logger.LogInformation($"Order.BuyerEmail:{order.BuyerEmail}");
            _logger.LogInformation($"Order.OrderDate:{order.OrderDate}");
            _logger.LogInformation($"Order.ShipToAddress:{order.ShipToAddress}");
            _logger.LogInformation($"Order.DeliveryMethod:{order.DeliveryMethod}");
            _logger.LogInformation($"Order.OrderItems:{order.OrderItems}");
            _logger.LogInformation($"Order.SubTotal:{order.SubTotal}");
            _logger.LogInformation($"Order.OrderStatus:{order.OrderStatus}");
            _logger.LogInformation($"Order.PaymentIntentId:{order.PaymentIntentId}");
            _logger.LogInformation($"Order.GetTotal:{order.GetTotal()}");

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Second _context.SaveChangesAsync()");

            // Remove Basket
            await _uow.BasketRepository.DeleteBasketAsync(basketId);
            _logger.LogInformation($"Basket Deleted");

            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
