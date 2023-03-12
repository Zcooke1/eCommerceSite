using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        private const string Cart = "ShoppingCart";

        public CartController(ProductContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Product? productToAdd = _context.Products.Where(p => p.ProductId == id).SingleOrDefault();

            if (productToAdd == null)
            {
                // Product with specified id does not exist
                TempData["Message"] = "Sorry that product no longer exists";
                return RedirectToAction("Index", "Products");
            }

            CartProductViewModel cartProduct = new()
            {
                ProductId = productToAdd.ProductId,
                ProductName = productToAdd.ProductName,
                Price = productToAdd.Price
            };

            List<CartProductViewModel> cartProducts = GetExistingCartData();
            cartProducts.Add(cartProduct);
            WriteShoppingCartCookie(cartProducts);

            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "Products");
        }

        private void WriteShoppingCartCookie(List<CartProductViewModel> cartProducts)
        {
            string cookieData = JsonConvert.SerializeObject(cartProducts);

            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddYears(1)
            });
        }

        /// <summary>
        /// Return the current list of products in the users shopping
        /// cart cookie. If there is no cookie, an empty list will be returned
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<CartProductViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return new List<CartProductViewModel>();
            }
            return JsonConvert.DeserializeObject<List<CartProductViewModel>>(cookie);
        }

        public IActionResult Summary()
        {
            //Read shopping cart data and convert to list of view model
            List<CartProductViewModel> cartProducts = GetExistingCartData();
            return View(cartProducts);
        }

        public IActionResult Remove(int id)
        {
            List<CartProductViewModel> cartProducts = GetExistingCartData();

            CartProductViewModel? targetProduct =
                cartProducts.Where(p => p.ProductId == id).FirstOrDefault();

            cartProducts.Remove(targetProduct);

            WriteShoppingCartCookie(cartProducts);

            return RedirectToAction(nameof(Summary));
        }
    }
}
