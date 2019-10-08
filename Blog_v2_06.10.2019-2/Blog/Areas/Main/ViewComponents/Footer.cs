using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Main.ViewComponents
{
    public class Footer : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("_Footer");
        }
    }
}
