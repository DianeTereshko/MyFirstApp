using System;
using Blog.Services.DbService;
using Blog.Services.Models;
using Blog.Services.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Default")]
    public class DefaultController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IIdentityService _identityService;
        private readonly IHostingEnvironment _appEnvironment;

        public DefaultController(IBlogService blogService, IIdentityService identityService, IHostingEnvironment appEnvironment)
        {
            _blogService = blogService;
            _identityService = identityService;
            _appEnvironment = appEnvironment;
        }
        [Route("Index/{id?}")]
        public IActionResult Index()
        {
            return View();
        }

        // * Summary: Менеджмент категориями
        [Route("Category/{id?}")]
        public async Task<IActionResult> Category()
        {
            IList<Category> categories = await _blogService.GetCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        [Route("CreateCategory/{id?}")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateCategory/{id?}")]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _blogService.AddCategoryAsync(category);
            }

            return RedirectToAction("Category", "Default", new { area = "Admin" });
        }

        [HttpGet]
        [Route("CreateSubCategory/{id?}")]
        public IActionResult CreateSubCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateSubCategory/{id?}")]
        public async Task<IActionResult> CreateSubCategory(Category category, string subCategoryName)
        {
            Category foundedCategory = await _blogService.FindCategoryByNameAsync(category.Name);
            await _blogService.AddSubCategoryToCategoryAsync(foundedCategory, subCategoryName);

            return RedirectToAction("Category", "Default", new { area = "Admin" });
        }

        [HttpGet]
        [Route("DeleteCategory/{id?}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _blogService.DeleteCategoryById(id);
            return RedirectToAction("Category");
        }

        [HttpGet]
        [Route("DeleteSubCategory/{id?}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            await _blogService.DeleteSubCategoryById(id);
            return RedirectToAction("Category");
        }

        [HttpGet]
        [Route("EditCategory/{id?}")]
        public async Task<IActionResult> EditCategory(int id)
        {
            Category category = await _blogService.GetCategoryByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [Route("EditCategory/{id?}")]
        public async Task<IActionResult> EditCategory(Category category)
        {
            await _blogService.UpdateCategoryAsync(category);
            return RedirectToAction("Category", "Default", new { area = "Admin" });
        }

        [HttpGet]
        [Route("EditSubCategory/{id?}")]
        public async Task<IActionResult> EditSubCategory(int id)
        {
            SubCategory subCategory = await _blogService.GetSubCategoryByIdAsync(id);
            return View(subCategory);
        }

        [HttpPost]
        [Route("EditSubCategory/{id?}")]
        public async Task<IActionResult> EditSubCategory(SubCategory subCategory)
        {
            await _blogService.UpdateSubCategoryAsync(subCategory);
            return RedirectToAction("Category", "Default", new { area = "Admin" });
        }

        // * Summary: Менеджмент пользователями
        [Route("Users/{id?}")]
        public async Task<IActionResult> Users()
        {
            IList<User> users = await _identityService.GetUsersAsync();
            return View(users);
        }

        [HttpGet]
        [Route("Block/{id?}")]
        public async Task<IActionResult> Block(string id)
        {
            await _identityService.BlockUserByIdAsync(id);
            return RedirectToAction("Users", "Default", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UnBlock/{id?}")]
        public async Task<IActionResult> UnBlock(string id)
        {
            await _identityService.UnBlockUserByIdAsync(id);
            return RedirectToAction("Users", "Default", new { area = "Admin" });
        }

        // * Summary: Менеджмент статей
        [Route("Article/{paging?}")]
        public async Task<IActionResult> Article(int paging)
        {
            IList<ArticleDetail> articleDetails = await _blogService.GetArticleDetailsAsync();
            return View(articleDetails.OrderByDescending(ad => ad.Id).GetPaged(paging, 5));
        }

        [HttpGet]
        [Route("AddArticle/{id?}")]
        public IActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        [Route("AddArticle/{id?}")]
        public async Task<IActionResult> AddArticle(Article article,
            IFormFile blogImage,
            IFormFile doc1,
            string doc1Title,
            IFormFile doc2,
            string doc2Title,
            IFormFile doc3,
            string doc3Title,
            string outerLink1Title,
            string outerLink1,
            string outerLink2Title,
            string outerLink2,
            string outerLink3Title,
            string outerLink3,
            string outerLink4Title,
            string outerLink4,
            string outerLink5Title,
            string outerLink5,
            string linkToVideo1,
            string titleVideo1,
            string linkToVideo2,
            string titleVideo2,
            string linkToVideo3,
            string titleVideo3)
        {
            string pathToBlogImage = "";
            if (blogImage != null)
            {

                // путь к папке в которой будем сохранять и добавление префикса Guid к имени файла
                pathToBlogImage = $"/upload/blog_images/{Guid.NewGuid().ToString() + blogImage.FileName}";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToBlogImage, FileMode.Create))
                {
                    await blogImage.CopyToAsync(fileStream);
                }
            }

            if (blogImage == null)
            {
                return StatusCode(500);
            }

            string pathToDoc1 = "";
            if (doc1 != null)
            {
                // путь к папке в которой будем сохранять и добавление префикса Guid к имени файла
                pathToDoc1 = $"/upload/download_files/{Guid.NewGuid().ToString() + doc1.FileName}";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToDoc1, FileMode.Create))
                {
                    await doc1.CopyToAsync(fileStream);
                }
            }
            string pathToDoc2 = "";
            if (doc2 != null)
            {
                // путь к папке в которой будем сохранять и добавление префикса Guid к имени файла
                pathToDoc2 = $"/upload/download_files/{Guid.NewGuid().ToString() + doc2.FileName}";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToDoc2, FileMode.Create))
                {
                    await doc2.CopyToAsync(fileStream);
                }
            }
            string pathToDoc3 = "";
            if (doc3 != null)
            {
                // путь к папке в которой будем сохранять и добавление префикса Guid к имени файла
                pathToDoc3 = $"/upload/download_files/{Guid.NewGuid().ToString() + doc3.FileName}";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToDoc3, FileMode.Create))
                {
                    await doc3.CopyToAsync(fileStream);
                }
            }
            SubCategory subCategory = await _blogService.FindSubCategoryByNameAsync(article.SubCategory.Name);

            if (subCategory == null)
            {
                return StatusCode(500);
            }
            Article newArticle = new Article()
            {
                Name = article.Name,
                Description = article.Description,
                ImageLink = pathToBlogImage,
                Raiting = article.Raiting,
                SubCategory = subCategory
            };

            int articleId = await _blogService.AddArticleAsync(newArticle);


            Article findArticle = await _blogService.GetArticleByIdAsync(articleId);

            if (findArticle == null)
            {
                return StatusCode(500);
            }
            ArticleDetail newArticleDetail = new ArticleDetail()
            {
                Text = article.ArticleDetail.Text,
                LinkToFlipBook = article.ArticleDetail.LinkToFlipBook,
                Image = pathToBlogImage,
                Name = article.Name,
                Description = article.Description,
                Article = findArticle
            };
            await _blogService.AddArticleDetailAsync(newArticleDetail);

            if (doc1 != null)
            {
                Download download1 = new Download()
                {
                    Title = doc1Title,
                    Url = pathToDoc1,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddDownloadAsync(download1);
            }

            if (doc2 != null)
            {
                Download download2 = new Download()
                {
                    Title = doc2Title,
                    Url = pathToDoc2,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddDownloadAsync(download2);
            }

            if (doc3 != null)
            {
                Download download3 = new Download()
                {
                    Title = doc3Title,
                    Url = pathToDoc3,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddDownloadAsync(download3);
            }

            if (titleVideo1 != null)
            {
                Video video1 = new Video()
                {
                    Title = titleVideo1,
                    Url = linkToVideo1,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddVideoAsync(video1);
            }

            if (titleVideo2 != null)
            {
                Video video2 = new Video()
                {
                    Title = titleVideo2,
                    Url = linkToVideo2,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddVideoAsync(video2);
            }
            if (titleVideo3 != null)
            {
                Video video3 = new Video()
                {
                    Title = titleVideo3,
                    Url = linkToVideo3,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddVideoAsync(video3);
            }

            if (outerLink1 != null)
            {
                OuterDownladLink outerDownladLink1 = new OuterDownladLink()
                {
                    Title = outerLink1Title,
                    FileShareLink = outerLink1,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddOuterDownloadLinkAsync(outerDownladLink1);
            }

            if (outerLink2 != null)
            {
                OuterDownladLink outerDownladLink2 = new OuterDownladLink()
                {
                    Title = outerLink2Title,
                    FileShareLink = outerLink2,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddOuterDownloadLinkAsync(outerDownladLink2);
            }
            if (outerLink3 != null)
            {
                OuterDownladLink outerDownladLink3 = new OuterDownladLink()
                {
                    Title = outerLink3Title,
                    FileShareLink = outerLink3,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddOuterDownloadLinkAsync(outerDownladLink3);
            }
            if (outerLink4 != null)
            {
                OuterDownladLink outerDownladLink4 = new OuterDownladLink()
                {
                    Title = outerLink4Title,
                    FileShareLink = outerLink4,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddOuterDownloadLinkAsync(outerDownladLink4);
            }
            if (outerLink5 != null)
            {
                OuterDownladLink outerDownladLink5 = new OuterDownladLink()
                {
                    Title = outerLink5Title,
                    FileShareLink = outerLink5,
                    ArticleDetail = newArticleDetail
                };
                await _blogService.AddOuterDownloadLinkAsync(outerDownladLink5);
            }
            return RedirectToAction("Article", "Default", new { area = "Admin" });
        }

        [HttpGet]
        [Route("EditArticle/{id?}")]
        public async Task<IActionResult> EditArticle(int id)
        {
            // * TODO: Получить Article по ArticleDetails чтобы отредактировать Article
            ArticleDetail articleDetail = await _blogService.GetArticleDetailByIdAsync(id);
            Article article = await _blogService.GetArticleByIdAsync(articleDetail.ArticleId);
            return View(article);
        }

        [HttpPost]
        [Route("EditArticle/{id?}")]
        public async Task<IActionResult> EditArticle(Article article, IFormFile blogImage)
        {
            // * TODO: Отредактировать статью. Сначала получить Article по ArticleDetailId (так как хз но почему то приходит Id от ArticleDetail, наверно связано со связью один к одному) затем их обновить
            ArticleDetail articleDetail = await _blogService.GetArticleDetailByIdAsync(article.Id);
            Article articleToUpdate = await _blogService.GetArticleByIdAsync(articleDetail.ArticleId);
            string path = "";
            if (blogImage != null)
            {
                // путь к папке в которой будем сохранять и  добавление префикса Guid к имени файла
                path = $"/upload/blog_images/{Guid.NewGuid().ToString() + blogImage.FileName}";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await blogImage.CopyToAsync(fileStream);
                }
            }

            if (blogImage == null)
            {
                return StatusCode(500);
            }

            if (articleToUpdate != null)
            {
                articleToUpdate.Name = article.Name;
                articleToUpdate.Description = article.Description;
                articleToUpdate.ImageLink = path;
                articleToUpdate.Raiting = article.Raiting;

                ArticleDetail articleDetailToUpdate = await _blogService.GetArticleDetailByIdAsync(article.Id);

                articleDetailToUpdate.Text = article.ArticleDetail.Text;
                articleDetailToUpdate.LinkToFlipBook = article.ArticleDetail.LinkToFlipBook;
                articleDetailToUpdate.Image = path;
                articleDetailToUpdate.Name = article.Name;
                articleDetailToUpdate.Description = article.Description;
                await _blogService.UpdateArticleAndArticleDetailAsync(articleToUpdate, articleDetailToUpdate);
            }

            return RedirectToAction("Article", "Default", new { area = "Admin" });
        }

        [HttpGet]
        [Route("DeleteArticle/{id?}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            // * TODO: получить Article по ArticleDetail ID и удалить Article, которая каскадно удалит связанные записи в ArticleDetail, Comments, Download Video
            ArticleDetail articleDetail = await _blogService.GetArticleDetailByIdAsync(id);
            await _blogService.DeleteArticleById(articleDetail.ArticleId);
            return RedirectToAction("Article", "Default", new { area = "Admin" });
        }

        // * Summary: Менеджмент комментариями
        [HttpGet]
        [Route("Comment/{paging?}")]
        public async Task<IActionResult> Comment(int paging)
        {
            IList<Comment> comments = await _blogService.GetCommentsAsync();
            IList<CommentViewModel> commentViewModels = await _blogService.GetCommentViewModelAsync();
            ViewBag.DataSource = commentViewModels;
            return View(comments.OrderByDescending(c => c.Id).GetPaged(paging, 10));
        }

        [HttpGet]
        [Route("DeleteComment/{id?}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _blogService.DeleteCommentById(id);
            return RedirectToAction("Article", "Default", new { area = "Admin" });
        }

        [HttpGet]
        [Route("EditComment/{id?}")]
        public async Task<IActionResult> EditComment(int id)
        {
            Comment comment = await _blogService.GetCommentByIdAsync(id);
            return View(comment);
        }

        [HttpPost]
        [Route("EditComment/{id?}")]
        public async Task<IActionResult> EditComment(Comment comment)
        {
            await _blogService.UpdateCommentAsync(comment);
            return RedirectToAction("Article", "Default", new { area = "Admin" });
        }
    }
}