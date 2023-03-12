using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            const int NumProductsToDisplayPerPage = 3;
            const int PageOffset = 1;

            int currPage = id ?? 1; // Set currPage to id if it has a value, otherwise use 1

            List<Product> products = await (from product in _context.Products
                                            select product)
                                            .Skip(NumProductsToDisplayPerPage * (currPage - PageOffset))
                                            .Take(NumProductsToDisplayPerPage)
                                            .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product); // Prepares insert
                await _context.SaveChangesAsync(); // Executes pending insert


                ViewData["Message"] = $"{product.ProductName} was added successfully.";

                return View();
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product? productToEdit = await _context.Products.FindAsync(id);

            if (productToEdit == null)
            {
                return NotFound();
            }

            return View(productToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product productModel)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(productModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{productModel.ProductName} was updated successfully!";
                return RedirectToAction("Index");
            }

            return View(productModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product? productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete == null)
            {
                return NotFound();
            }
            return View(productToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = productToDelete.ProductName + " was deleted successfully";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This product was already deleted";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Product? productDetails = await _context.Products.FindAsync(id);

            if (productDetails == null)
            {
                return NotFound();
            }
            return View(productDetails);
        }
    }
}
