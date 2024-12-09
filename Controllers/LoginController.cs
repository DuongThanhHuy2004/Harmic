using Harmic.Models;
using Harmic.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    public class LoginController : Controller
    {
        private readonly HarmicContext _context;

        public LoginController(HarmicContext context)
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

            string pw = Function.MD5Password(account.Password);
            var check = _context.TbAccounts.Where(m => (m.Email == account.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Function._Message = "Invalid Username or Password!";
                return RedirectToAction("Index", "Login");
            }

            Function._Message = string.Empty;
            Function._UserID = check.AccountId;
            Function._Username = string.IsNullOrEmpty(check.Username) ? string.Empty : check.Username;
            Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            return RedirectToAction("Index", "Home");
        }
    }
}
