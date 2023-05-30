using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using WebAppTask4.Areas.Identity.Data;
using WebAppTask4.Models;
using WebAppTask4.Models;

namespace WebAppTask4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
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