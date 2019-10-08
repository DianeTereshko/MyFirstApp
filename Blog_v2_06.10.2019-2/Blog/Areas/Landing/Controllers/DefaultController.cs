using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Landing.Controllers
{
    public class DefaultController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public DefaultController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager; 
        }
        public async Task<IActionResult> Index()
        {
            User user = await _signInManager.UserManager.GetUserAsync(User);
            if (await _signInManager.UserManager.IsInRoleAsync(user, "Admin"))
            {
                RedirectToAction("Index", "Default", new { area = "Admin" });
            }

            else
            {
                RedirectToAction("Index", "Default", new { area = "Main" });
            }
            return View();
        }
    }
}