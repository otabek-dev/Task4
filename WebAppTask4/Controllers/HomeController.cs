using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using WebAppTask4.Areas.Identity.Data;
using WebAppTask4.Attributes;
using WebAppTask4.Data;
using WebAppTask4.Models;

namespace WebAppTask4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (user == null)
            {
                _signInManager.SignOutAsync().Wait();
                return Redirect("/Identity/Account/Login");
            }
            else if (!user.IsActive)
            {
                _signInManager.SignOutAsync().Wait();
                return Redirect("/Identity/Account/Login");
            }

            user.LastLoginTime = DateTime.UtcNow;
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}