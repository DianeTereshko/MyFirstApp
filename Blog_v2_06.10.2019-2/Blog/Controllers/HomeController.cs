using Blog.Models;
using Blog.Services.DbService;
using Blog.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Blog.Controllers
{

    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBlogService _blogService;
        public static bool check = true;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, IBlogService blogService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // * TODO: Сделать добавление пользователя и администратора для базы данных в памяти
            if (check)
            {
                IdentityResult createUserRole = await _roleManager.CreateAsync(new IdentityRole("User"));
                IdentityResult createAdminRole = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                IdentityResult createBannedRole = await _roleManager.CreateAsync(new IdentityRole("Block"));
                User admin = new User()
                {
                    Email = "admin@admin.com",
                    UserName = "admin"
                };
                User user = new User()
                {
                    Email = "user@user.com",
                    UserName = "user",
                };
                await _userManager.CreateAsync(user, "123456");
                await _userManager.CreateAsync(admin, "654321");
                await _userManager.AddToRoleAsync(user, "User");
                await _userManager.AddToRoleAsync(admin, "Admin");

                await _blogService.DataSeedingAsync();
                check = false;
            }

            return RedirectToAction("Index", "Default", new { area = "Main" });
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
