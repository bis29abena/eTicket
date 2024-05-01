using eTicket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _db { get; set; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext db)
        {
            _db = db;
        }

        //Creating and getting a session for the shopping cart
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _db.ShoppingCartItems.Where(n => n.ShopingCartId == ShoppingCartId).Include(n => n.Movie).ToList();
        }

        public double GetShoppingCartTotal()
        {
            var total = _db.ShoppingCartItems.Where(n => ShoppingCartId == ShoppingCartId).Select(n => n.Movie.MoviePrice * n.Amount).Sum();

            return total;
        }

        public void AddShoppingCartItem(Movie movie)
        {
            var ShoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShopingCartId == ShoppingCartId);

            if(ShoppingCartItem == null)
            {
                ShoppingCartItem = new ShoppingCartItem()
                {
                    ShopingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _db.ShoppingCartItems.Add(ShoppingCartItem);
            }
            else
            {
                ShoppingCartItem.Amount++;
            }
            _db.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var ShoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShopingCartId == ShoppingCartId);

            if (ShoppingCartItem != null)
            {
                if(ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    _db.ShoppingCartItems.Remove(ShoppingCartItem);
                }
            }
            _db.SaveChanges();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _db.ShoppingCartItems.Where(n => n.ShopingCartId == ShoppingCartId).ToListAsync();

            _db.ShoppingCartItems.RemoveRange(items);
        }
    }
}
