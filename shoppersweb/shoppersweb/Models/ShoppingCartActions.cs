using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using shoppersdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shoppersweb.Models
{
    public class ShoppingCartActions : PageModel
    {

        private readonly DataHelper helper = new DataHelper();
        public const string CartSessionKey = "CartId";

        public decimal CartTotal { get; set; }
        public string ShoppingCartId { get; set; }
        public IHttpContextAccessor httpContextAccessor { get; set; }

        public async Task<CartItem> AddToAddAsync(int id)
        {
            ShoppingCartId = GetCartId();

            var result = helper.CartItems();
            var awaitResult = await result;

            var cartItem = awaitResult.SingleOrDefault(c => c.CartId == ShoppingCartId && c.ProductId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Quantity = 1,
                    CreatedOn = DateTimeOffset.Now,
                    ModifiedOn = DateTimeOffset.Now
                };

                var addtocart = helper.AddItemToCart(cartItem);
                var awaitCartItem = await addtocart;
            }
            else
            {
                cartItem.Quantity++;
                var updatecart = helper.UpdateCart(cartItem);
                var awaitUpdatedItem = await updatecart;
            }

            return cartItem;
        }

        public string GetCartId()
        {
            if (string.IsNullOrEmpty(httpContextAccessor.HttpContext.Session.GetString(CartSessionKey)))
            {
                if (!string.IsNullOrWhiteSpace(httpContextAccessor.HttpContext.User.Identity.Name))
                {
                    httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, HttpContext.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    httpContextAccessor.HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return httpContextAccessor.HttpContext.Session.GetString(CartSessionKey);
        }

    }
}
