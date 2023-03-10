using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
