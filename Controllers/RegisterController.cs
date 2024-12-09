using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    
    public class RegisterController : Controller
    {
        private readonly HarmicContext _context;

        public RegisterController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TbAccount account)
        {
            if (account == null)
            {
                return NotFound();
            }

            var check = _context.TbAccounts.Where(m => m.Email == account.Email).FirstOrDefault();
            if (check != null)
            {
                Function._MessageEmail = "Duplicate Email!";
                return RedirectToAction("Index", "Register");
            }
            Function._MessageEmail = string.Empty;
            account.Password = Function.MD5Password(account.Password);
            account.IsActive = true;
            _context.Add(account);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
