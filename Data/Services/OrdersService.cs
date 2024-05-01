using eTicket.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Data.Services
{
    public class OrdersService : IOrderService
    {

        private readonly ApplicationDbContext _db;

        public OrdersService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _db.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n=>n.User).ToListAsync();

            if(userRole != "Admin")
            {
                orders = await _db.Orders.Where(n => n.UserId == userId).ToListAsync();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string email)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = email
            };

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.MoviePrice
                };

                await _db.OrderItems.AddAsync(orderItem);
            }

            await _db.SaveChangesAsync();
        }
    }
}
