using E_commerce.Core.Entities.OrderAggregate;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Services.Contract;
using E_commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly ApplicationDbContext _dbContext;

        public OrderService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _basketRepository = basketRepository;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress shippingAddress)
        {
            // Get Basket Item
            var basket = await _basketRepository.GetBasketByIdAsync(basketId);
            if (basket is null) return null;

            var orderItems = new List<OrderItem>();

            /// Fill Item
            ///Parallel.ForEach(basket.BasketItems, item =>
            ///{
            ///    var product = _unitOfWork.ProductRepository.GetByIdAsync(item.Id).GetAwaiter().GetResult();
            ///    var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.ProductPicture);
            ///    var orderItem = new OrderItem(productItemOrdered, item.Price, item.Quantity);
            ///    lock (orderItems)
            ///    {
            ///        orderItems.Add(orderItem);
            ///    }
            ///});

            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                if (product == null) continue;

                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.ProductPicture);
                var orderItem = new OrderItem(productItemOrdered, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            await _dbContext.OrderItems.AddRangeAsync(orderItems);
            //await _dbContext.SaveChangesAsync();

            // Get Delivery Method
            var deliveryMethod = await _dbContext.DeliveryMethods.Where(d => d.Id == deliveryMethodId).FirstOrDefaultAsync();
            if (deliveryMethod == null) return null;

            // Calculate subTotal
            var subTotal = orderItems.Sum(s => s.Price * s.Quantity);

            // Initialaize on Constractor
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);

            // Check Order is not null
            if (order is null) return null;

            // Adding Order in DB
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            // Remove Basket
            await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _dbContext.DeliveryMethods.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _dbContext.Orders.Where(o => o.Id == id && o.BuyerEmail == buyerEmail)
                .Include(o => o.OrderItems).Include(o => o.DeliveryMethods)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            return order ?? new Order();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _dbContext.Orders.Where(o => o.BuyerEmail == buyerEmail)
                .Include(o => o.OrderItems).Include(o => o.DeliveryMethods)
                .ToListAsync();

            return orders;
        }
    }
}
