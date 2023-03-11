using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    
    public class MembersController : Controller
    {
        private readonly ProductContext _context;
        public MembersController(ProductContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };
                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            return View(regModel);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                Member? m = (from member in _context.Members
                           where member.Email == loginModel.Email &&
                           member.Password == loginModel.Password
                           select member).SingleOrDefault();

                if (m != null)
                {
                    HttpContext.Session.SetString("Email", loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found!");

            }
            return View(loginModel);
        }
    }
}
