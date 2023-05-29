using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using WebAppTask4.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using WebAppTask4.Data;

namespace WebAppTask4.Attributes
{
    public class DatabaseUserCheckAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly AppDbContext _context;

        public DatabaseUserCheckAttribute(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.Identity.Name;

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                // Разлогинить пользователя
                await context.HttpContext.SignOutAsync();

                // Перенаправить пользователя на страницу входа
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }

    //    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    //    {
    //        var userId = context.HttpContext.User.Identity.Name;

    //        // Проверить наличие пользователя в базе данных по userId
    //        var userExists = await CheckUserExists(userId);

    //        if (!userExists)
    //        {
    //            // Разлогинить пользователя
    //            await context.HttpContext.SignOutAsync();

    //            // Перенаправить пользователя на страницу входа
    //            context.Result = new RedirectToActionResult("Login", "Account", null);
    //        }
    //    }

    //    private async Task<bool> CheckUserExists(string userId)
    //    {
    //        return true;
    //    }
    }
}
