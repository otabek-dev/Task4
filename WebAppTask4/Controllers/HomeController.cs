using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
        //ILogger<HomeController> logger, AppDbContext context, 
        
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;

            // Проверить, существует ли пользователь в базе данных
            if (user == null)
            {
                // Закрыть сессию
                _signInManager.SignOutAsync().Wait();

                // Перенаправить на страницу логина или другую нужную страницу
                return Redirect("/Identity/Account/Login"); ;
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