using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Services.DbService;
using Blog.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Main.ViewComponents
{
    public class MenuHeader : ViewComponent
    {
        private readonly IBlogService _blogService;

        public MenuHeader(IBlogService blogService)
        {
            _blogService = blogService; 
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IList<Category> categories = await _blogService.GetCategoriesAsync();
            return View("_MenuHeader", categories);
        }
    }
}
