using eTicket.Data;
using eTicket.Data.Cart;
using eTicket.Data.Services;
using eTicket.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTicket.Controllers
{
    public class OrdersController : Controller
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IMoviesService _moviesService;
#pragma warning restore IDE0052 // Remove unread private members

#pragma warning disable IDE0052 // Remove unread private members
        private readonly ShoppingCart _shoppingCart;
#pragma warning restore IDE0052 // Remove unread private members
        private readonly IOrderService _orderService;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = User.FindFirst(ClaimTypes.Role).Value;
            var orders = await _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }


        public IActionResult ShoppingCart()
        {
            var shoppingCarts = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = shoppingCarts;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            var movieItems = await _moviesService.GetMovieByIdAsync(id);

            if(movieItems != null)
            {
                _shoppingCart.AddShoppingCartItem(movieItems);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var movieItems = await _moviesService.GetMovieByIdAsync(id);

            if (movieItems != null)
            {
                _shoppingCart.RemoveItemFromCart(movieItems);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var userId = User.FindFirstValue (ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Name); 
            

            await _orderService.StoreOrderAsync(items, userId, email);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
