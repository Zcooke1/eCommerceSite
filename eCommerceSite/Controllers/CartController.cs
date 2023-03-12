using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;

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
            
            
            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "Products");
        }
    }
}
