using Blog.Services.DbService;
using Blog.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.ViewComponents
{
    public class AdminMenuHeader : ViewComponent
    {
        private readonly IBlogService _blogService;

        public AdminMenuHeader(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IList<Category> categories = await _blogService.GetCategoriesAsync();
            return View("_AdminMenuHeader", categories);
        }
    }
}