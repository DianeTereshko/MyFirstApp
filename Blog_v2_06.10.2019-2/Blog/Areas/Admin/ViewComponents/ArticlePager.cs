using System.Threading.Tasks;
using Blog.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.ViewComponents
{
    public class ArticlePager : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return await Task.FromResult((IViewComponentResult)View("_ArticlePager", result));

        }


    }
}
