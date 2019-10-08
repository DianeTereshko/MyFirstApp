using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blog.Services.DbService;
using Blog.Services.Models;
using Blog.Services.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Blog.Areas.Main.Controllers
{
    [Area("Main")]
    [Route("Main/Default")]
    public class DefaultController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly SignInManager<User> _signInManager;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly TriggerModel _triggerModel;

        public DefaultController(IBlogService blogService, SignInManager<User> signInManager, IHostingEnvironment appEnvironment, TriggerModel triggerModel)
        {
            _blogService = blogService;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
            _triggerModel = triggerModel;
        }

        [Route("Index/{paging?}")]
        public async Task<IActionResult> Index(int paging)
        {
            var ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            //// * TODO: Сделать сохранение IP адреса посетителя в базе данных
            if (!ip.Contains("178.72.90.49") && !ip.Contains("0.0.0.1") && !ip.Contains("127.0.0.1") &&
                !ip.Contains("52.42.49.200") && !ip.Contains("63.143.42.252") && !ip.Contains("35.173.69.86"))
            {
                BlogVizitor vizitor = new BlogVizitor()
            {
                Ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()
            };

            await _blogService.AddBlogVizitor(vizitor);
            }

            //// * TODO: Сделать уведомление на почту что пришел посетитель

            //if (!ip.Contains("178.72.90.49") && !ip.Contains("0.0.0.1") && !ip.Contains("127.0.0.1") &&
            //    !ip.Contains("52.42.49.200") && !ip.Contains("63.143.42.252") && !ip.Contains("35.173.69.86"))
            //{
            //    SmtpClient emailClient = new SmtpClient();
            //    emailClient.Port = 587;
            //    emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    emailClient.UseDefaultCredentials = false;
            //    emailClient.Host = "smtp.gmail.com";
            //    emailClient.EnableSsl = true;
            //    emailClient.Timeout = 90000;
            //    emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    emailClient.UseDefaultCredentials = false;
            //    emailClient.Credentials = new System.Net.NetworkCredential("Tereshko@gmail.com", "filesecureserver10");

            //    MailMessage mail = new MailMessage();
            //    mail.From = new MailAddress("Tereshko@gmail.com");
            //    mail.To.Add(new MailAddress("DianeFLTereshko@gmail.com"));
            //    mail.Subject = $"Новый посетитель c адресом {HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()}";
            //    mail.Body = $"Посетитель c адресом {HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()}";
            //    mail.BodyEncoding = Encoding.UTF8;

            //    emailClient.Send(mail);
            //}

            // * TODO:  Сделать вывод всех статей, сделать так чтобы при клике на категории отображались статьи только определенной категории или подкатегории
            IList<Article> articles = await _blogService.GetArticlesAsync();
            PagedResult<Article> pagedArticles = articles.OrderByDescending(d => d.Date).GetPaged(paging, 5);
            return View(pagedArticles);
        }

        [Route("Category/{category?}/{paging?}")]
        public async Task<IActionResult> Category(int category, int paging)
        {
            // * TODO:  Сделать вывод всех статей,  при клике на категории будут отображаться статьи только определенной категории


            Category requestCategory = await _blogService.GetCategoryByIdAsync(category);
            IList<Article> articles = await _blogService.GetArticlesInCategoryAsync(requestCategory);
            return View(articles.OrderByDescending(d => d.Date).GetPaged(paging, 5));

        }

        [Route("SubCategory/{subcategory?}/{paging?}")]
        public async Task<IActionResult> SubCategory(int subcategory, int paging)
        {
            // * TODO:  Сделать вывод всех статей,  при клике на категории будут отображаться статьи только определенной под категории

            SubCategory requestSubCategory = await _blogService.GetSubCategoryByIdAsync(subcategory);
            IList<Article> articles = await _blogService.GetArticlesInSubCategoryAsync(requestSubCategory);
            return View(articles.OrderByDescending(d => d.Date).GetPaged(paging, 5));

        }

        [HttpGet]
        [Route("Article/{id?}")]
        public async Task<IActionResult> Article(int id)
        {
            HttpContext.Session.Clear();
            // * TODO: Сделать вывод статьи
            ArticleDetail articleDetail = await _blogService.GetArticleDetailByIdAsync(id);
            if (articleDetail == null)
            {
                return NotFound();
            }
            return View(articleDetail);
        }

        [HttpPost]
        [Route("Article/{message?}/{articleId?}/{linkToFile?}/{id?}")]
        public async Task<IActionResult> Article(string message, int articleId, IFormFile linkToFile, int id)
        {
            string path = "";
            if (linkToFile != null)
            {
                // путь к папке comments
                path = $"/upload/comment_files/{Guid.NewGuid().ToString() + linkToFile.FileName}";
                // сохраняем файл в папку comments в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await linkToFile.CopyToAsync(fileStream);
                }
            }
            // * TODO: Сделать добавление комментария в базу данных
            ArticleDetail article = await _blogService.GetArticleDetailByIdAsync(articleId);
            User user = await _signInManager.UserManager.GetUserAsync(User);
            Comment comment = new Comment();
            if (linkToFile == null)
            {

                comment.UserId = user.Id;
                comment.UserName = user.UserName;
                comment.UserEmail = user.Email;
                comment.Message = message;
                comment.Date = DateTime.Now;
                comment.ArticleDeatil = article;

            }
            if (linkToFile != null)
            {

                comment.UserId = user.Id;
                comment.UserName = user.UserName;
                comment.UserEmail = user.Email;
                comment.Message = message;
                comment.Date = DateTime.Now;
                comment.ArticleDeatil = article;
                comment.LinkToFile = path;
            }
            await _blogService.AddCommentAsync(comment);

            return RedirectToAction("Article", "Default", new { id = articleId, area = "Main" });
        }

        [HttpPost]
        [Route("Search/{search?}")]
        public async Task<IActionResult> Search(string search)
        {
            _triggerModel.Phraze = search;
            // * TODO: Сделать механизм поиска
            //IList<Article> foundedArticles = _blogService.SearchInTitle(search);
            IList<Article> foundedArticles = await _blogService.SearchInText(search);
            return View("search", foundedArticles);
        }

        [HttpGet]
        [Route("Search/{articles?}")]
        public IActionResult Search(IList<Article> articles)
        {
            // * TODO: Сделать механизм поиска
            if (articles != null)
            {
                return View(articles);
            }

            return View();
        }

        [HttpGet]
        [Route("AddSubscribe/{id?}")]
        public IActionResult AddSubscribe()
        {
            // * TODO: Сделать добавление пользователя в список подписчиков

            return View();
        }

        [HttpPost]
        [Route("AddSubscribe/{id?}")]
        public async Task<IActionResult> AddSubscribe(Subscriber subscriber)
        {
            User userName = await _signInManager.UserManager.GetUserAsync(User);
            if (_signInManager.IsSignedIn(User))
            {
                Subscriber newSubscriber = new Subscriber()
                {
                    UserName = subscriber.UserName,
                    Email = userName.Email,
                    Date = DateTime.UtcNow

                };
                await _blogService.AddSubscribeAsync(newSubscriber);

            }
            else
            {
                await _blogService.AddSubscribeAsync(subscriber);

            }
            return RedirectToAction("Index", "Default", new { area = "Main" });
        }



        [HttpGet]
        [Route("AddFeedBack/{id?}")]
        public IActionResult AddFeedBack()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedBack(FeedBack feedBack)
        {
            await _blogService.AddFeedBackAsync(feedBack);
            return RedirectToAction("Index", "Default", new { area = "Main" });
        }

        [HttpGet]
        [Route("About/{id?}")]
        public IActionResult About()
        {
            // * TODO: Сделать добавление пользователя в список подписчиков

            return View();
        }

        [HttpPost]
        [Route("AddLike/{id?}")]
        public async Task<ContentResult> AddLike(string articleId)
        {
            // * TODO: Сделать механизм голосования за статью
            await _blogService.AddLikeAsync(int.Parse(articleId));
            ArticleDetail articleDetail = await _blogService.GetArticleDetailByIdAsync(int.Parse(articleId));
            return Content(articleDetail.Like.ToString());
        }

        [HttpPost]
        [Route("AddUnlike/{id?}")]
        public async Task<ContentResult> AddUnlike(string articleId)
        {
            // * TODO: Сделать механизм голосования за статью
            await _blogService.AddUnlikeAsync(int.Parse(articleId));
            ArticleDetail articleDetail = await _blogService.GetArticleDetailByIdAsync(int.Parse(articleId));
            return Content(articleDetail.Unlike.ToString());
        }

        [HttpPost]
        [Route("AddCommentLike/{id?}")]
        public async Task<ContentResult> AddCommentLike(string commentId)
        {
            // * TODO: Сделать механизм голосования за статью
            await _blogService.AddCommentLikeAsync(int.Parse(commentId));
            Comment comment = await _blogService.GetCommentByIdAsync(int.Parse(commentId));
            return Content(comment.Like.ToString());
        }

        [HttpPost]
        [Route("AddCommentUnlike/{id?}")]
        public async Task<ContentResult> AddCommentUnlike(string commentId)
        {
            // * TODO: Сделать механизм голосования за статью
            await _blogService.AddCommentUnlikeAsync(int.Parse(commentId));
            Comment comment = await _blogService.GetCommentByIdAsync(int.Parse(commentId));
            return Content(comment.Unlike.ToString());
        }

        [HttpGet]
        [Route("Freeze/{id?}")]
        public IActionResult Freeze()
        {
            return View();
        }
    }
}