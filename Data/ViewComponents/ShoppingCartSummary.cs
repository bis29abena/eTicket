using eTicket.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shopping;

        public ShoppingCartSummary(ShoppingCart shopping)
        {
            _shopping = shopping;
        }

        public IViewComponentResult Invoke()
        {
            var shoppingItems = _shopping.GetShoppingCartItems();

            return View(shoppingItems.Count());
        }
    }
}
