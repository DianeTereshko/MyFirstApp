using System.Threading.Tasks;
using Blog.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Main.ViewComponents
{
    public class PagerSubCategory : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return await Task.FromResult((IViewComponentResult)View("_PagerSubCategory", result));

        }


    }
}
