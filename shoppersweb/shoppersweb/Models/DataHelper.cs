/*
 * Data Helper class to invoke API methods
 */

using Newtonsoft.Json;
using shoppersdata;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace shoppersweb.Models
{
    public class DataHelper
    {
        // Your local host port will be different
        // Please change 44325 to your local host port that will be assigned
        private readonly string apiBaseUrl = "https://paas-shoppingcart-api.azurewebsites.net/api/";

        public async Task<List<Category>> Categories()
        {
            List<Category> categories = new List<Category>();

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(apiBaseUrl + "Categories");

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);
            }

            return categories;
        }

        public async Task<List<Product>> Products()
        {
            List<Product> products = new List<Product>();

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(apiBaseUrl + "Products");

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
            }

            return products;
        }

        public async Task<List<CartItem>> CartItems()
        {
            List<CartItem> cartItems = new List<CartItem>();

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(apiBaseUrl + "CartItems");

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(apiResponse);
            }

            return cartItems;
        }

        public async Task<CartItem> AddItemToCart(CartItem cartItem)
        {
            CartItem newCartItem = new CartItem();
            using var httpClient = new HttpClient();
            var content = new MultipartFormDataContent
            {
                new StringContent(cartItem.CartId),
                new StringContent(cartItem.ProductId.ToString()),
                new StringContent(cartItem.Quantity.ToString()),
                new StringContent(cartItem.CreatedOn.ToString()),
                new StringContent(cartItem.ModifiedOn.ToString())
            };

            using (var response = await httpClient.PutAsync(apiBaseUrl + "CartItems", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                newCartItem = JsonConvert.DeserializeObject<CartItem>(apiResponse);
            };

            return newCartItem;
        }

        public async Task<CartItem> UpdateCart(CartItem cartItem)
        {
            CartItem newCartItem = new CartItem();
            using var httpClient = new HttpClient();
            var content = new MultipartFormDataContent
            {
                new StringContent(cartItem.Quantity.ToString()),
                new StringContent(cartItem.CreatedOn.ToString()),
                new StringContent(cartItem.ModifiedOn.ToString())
            };

            using (var response = await httpClient.PostAsync(apiBaseUrl + "CartItems", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                newCartItem = JsonConvert.DeserializeObject<CartItem>(apiResponse);
            };

            return newCartItem;
        }


        public async Task<List<int>> SubCategories(int categoryId)
        {

            var result = Categories();
            List<int> subCategories = new List<int>();
            var awaitResult = await result;

            var categories = awaitResult.Where(c => c.ParentCategoryId == categoryId);

            if (categories != null)
            {
                foreach (var child in categories)
                {
                    subCategories.Add(child.Id);
                }
            }

            return subCategories;
        }
    }
}
