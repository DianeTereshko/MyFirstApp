using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Membership.Controllers
{
    [Area("Membership")]
    public class DefaultController : Controller
    {
        //[Route("{page?}/{category?}/{subcategory?}")]
        [Route("Membership/Default/Index/{category?}/{subcategory?}")]
        public IActionResult Index(int category, int subcategory)
        {
            return View();
        }
    }
}