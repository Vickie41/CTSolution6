using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CTSolution.Models;

namespace CTSolution.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginView (LoginView model)
        {
          
            
                if (model.Username == "CTadmin" && model.Password == "CT@dmin123")
                {
                    return RedirectToAction("Index", "Dashboard"); // Redirect to a secure page after login
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
