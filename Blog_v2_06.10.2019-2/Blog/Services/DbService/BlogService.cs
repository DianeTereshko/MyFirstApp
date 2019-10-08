using Blog.Services.Context;
using Blog.Services.Models;
using Blog.Services.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.DbService
{
    public interface IBlogService
    {
        // * Division: Create * //
        Task<int> AddArticleAsync(Article article);
        Task<int> AddCategoryAsync(Category category);
        Task<int> AddSubCategoryAsync(SubCategory subcategory);
        Task<int> AddArticleDetailAsync(ArticleDetail articleDetail);
        Task<int> AddVideoAsync(Video video);
        Task<int> AddDownloadAsync(Download download);
        Task<int> AddOuterDownloadLinkAsync(OuterDownladLink outerDownladLink);
        Task<bool> AddCommentAsync(Comment comment);
        Task<bool> AddSubCategoryToCategoryAsync(Category category, string subcategory);
        Task<bool> AddLikeAsync(int articleDetailId);
        Task<bool> AddUnlikeAsync(int articleDetailId);
        Task<bool> AddCommentLikeAsync(int commentId);
        Task<bool> AddCommentUnlikeAsync(int commentId);
        Task<bool> AddSubscribeAsync(Subscriber subscriber);
        Task<bool> AddFeedBackAsync(FeedBack feedback);
        Task<bool> AddBlogVizitor(BlogVizitor blogVizitor);

        // * Division: Read //
        Task<IList<Article>> GetArticlesAsync();
        Task<Article> GetArticleAsync(Article article);
        Task<Article> GetArticleByIdAsync(int id);
        Task<Category> GetCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int? id);
        Task<IList<Category>> GetCategoriesAsync();
        IList<Category> GetCategories();
        Task<SubCategory> GetSubCategoryAsync(SubCategory subCategory);
        Task<SubCategory> GetSubCategoryByIdAsync(int? id);
        IList<SubCategory> GetSubCategories();
        Task<ArticleDetail> GetArticleDetailAsync(Article article);
        Task<ArticleDetail> GetArticleDetailByIdAsync(int id);
        Task<IList<ArticleDetail>> GetArticleDetailsAsync();
        Task<IList<ArticleDetailViewModel>> GetArticleDetailViewModel();
        Task<Video> GetVideoAsync(Video video);
        Task<Video> GetVideoByIdAsync(int id);
        Task<Comment> GetCommentAsync(Comment comment);
        Task<Comment> GetCommentByIdAsync(int id);
        Task<IList<Comment>> GetCommentsAsync();
        Task<IList<CommentViewModel>> GetCommentViewModelAsync();
        Task<Download> GetDownloadAsync(Download download);
        Task<Download> GetDownloadByIdAsync(int id);
        Task<IList<Article>> GetArticlesInCategoryAsync(Category category);
        Task<IList<Article>> GetArticlesInSubCategoryAsync(SubCategory subCategory);
        Task<IList<SubCategory>> GetSubCategoriesInCategoryAsync(Category category);
        Task<IList<SubCategory>> GetSubCategoriesInCategoryByIdAsync(int id);
        Task<Subscriber> GetSubSubscribeByIdAsync(int id);

        // * Division: Update * //
        Task<bool> UpdateArticleAndArticleDetailAsync(Article article, ArticleDetail articleDetail);
        Task<bool> UpdateCommentAsync(Comment comment);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> UpdateSubCategoryAsync(SubCategory subCategory);
        Task<bool> UpdateVideoAsync(Video video);
        Task<bool> UpdateDownloadAsync(Download download);

        // * Division: Delete * //
        Task<bool> DeleteArticleById(int id);
        Task<bool> DeleteVideoById(int id);
        Task<bool> DeleteDownloadById(int id);
        Task<bool> DeleteCommentById(int id);
        Task<bool> DeleteCategoryById(int id);
        Task<bool> DeleteSubCategoryById(int id);

        // * Division: Find * //
        Task<SubCategory> FindSubCategoryByNameAsync(string name);
        Task<Category> FindCategoryByNameAsync(string name);
        Task<IList<Article>> SearchInTitle(string textToSearch);
        Task<IList<Article>> SearchInText(string textToSearch);

        // * Division: Data Seeding * //
        Task<bool> DataSeedingAsync();
    }

    public class BlogService : IBlogService
    {
        //private readonly BlogDbContext _context;

        private readonly BlogDbContextInMemory _context;
        private readonly IHostingEnvironment _env;

        public BlogService(/*BlogDbContext context,*/ BlogDbContextInMemory context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<int> AddArticleAsync(Article article)
        {
            article.Date = DateTime.Now;
            await _context.Article.AddAsync(article);
            await _context.SaveChangesAsync();
            int id = article.Id;
            return id;
        }

        public async Task<int> AddArticleDetailAsync(ArticleDetail articleDetail)
        {
            articleDetail.Date = DateTime.Now;
            await _context.ArticleDetail.AddAsync(articleDetail);
            await _context.SaveChangesAsync();
            int id = articleDetail.Id;
            return id;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            // * TODO: Сделать добавление в базу данных категорию
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            int id = category.Id;
            return id;
        }

        public async Task<int> AddSubCategoryAsync(SubCategory subCategory)
        {
            // * TODO: Сделать добавление в базу данных подкатегорию
            await _context.SubCategory.AddAsync(subCategory);
            await _context.SaveChangesAsync();
            int id = subCategory.Id;
            return id;
        }

        public async Task<int> AddVideoAsync(Video video)
        {
            await _context.Video.AddAsync(video);
            await _context.SaveChangesAsync();
            int id = video.Id;
            return id;
        }

        public async Task<int> AddDownloadAsync(Download download)
        {
            await _context.Download.AddAsync(download);
            await _context.SaveChangesAsync();
            int id = download.Id;
            return id;
        }

        public async Task<int> AddOuterDownloadLinkAsync(OuterDownladLink outerDownladLink)
        {
            await _context.OuterDownladLink.AddAsync(outerDownladLink);
            await _context.SaveChangesAsync();
            int id = outerDownladLink.Id;
            return id;
        }

        public async Task<bool> AddSubCategoryToCategoryAsync(Category category, string subСategoryName)
        {
            // * TODO: Сделать добавление в базу данных подкатегории с ссылкой на категорию
            SubCategory subCategory = new SubCategory()
            {
                Name = subСategoryName,
                CategoryId = category.Id,
                Category = category
            };
            await _context.SubCategory.AddAsync(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddCommentAsync(Comment comment)
        {
            // * TODO: Сделать добавление в базу данных комментария
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddLikeAsync(int articleDetailId)
        {
            // * TODO: Сделать добавление в базу данных счетчика лайков статьи
            int like;
            ArticleDetail foundedArticleDetail = await _context.ArticleDetail.Where(ad => ad.Id == articleDetailId).FirstOrDefaultAsync();
            if (foundedArticleDetail != null)
            {
                if (foundedArticleDetail.Like == null)
                {
                    like = 1;
                    foundedArticleDetail.Like = like;
                }

                if (foundedArticleDetail.Like != null)
                {
                    like = (int)foundedArticleDetail.Like + 1;
                    foundedArticleDetail.Like = like;
                }
            }
            _context.Update(foundedArticleDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUnlikeAsync(int articleDetailId)
        {
            // * TODO: Сделать добавление в базу данных счетчика дизлайков статьи
            int like;
            ArticleDetail foundedArticleDetail = await _context.ArticleDetail.Where(ad => ad.Id == articleDetailId).FirstOrDefaultAsync();
            if (foundedArticleDetail != null)
            {
                if (foundedArticleDetail.Unlike == null)
                {
                    like = 1;
                    foundedArticleDetail.Unlike = like;
                }

                if (foundedArticleDetail.Unlike != null)
                {
                    like = (int)foundedArticleDetail.Unlike + 1;
                    foundedArticleDetail.Unlike = like;
                }
            }
            _context.Update(foundedArticleDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddCommentLikeAsync(int commentId)
        {
            // * TODO: Сделать добавление в базу данных счетчика лайков комментария

            int like;
            Comment comment = await _context.Comment.Where(c => c.Id == commentId).FirstOrDefaultAsync();
            if (comment != null)
            {
                if (comment.Like == null)
                {
                    like = 1;
                    comment.Like = like;
                }

                if (comment.Like != null)
                {
                    like = (int)comment.Like + 1;
                    like = (int)comment.Like + 1;
                    comment.Like = like;
                }
            }
            _context.Update(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddCommentUnlikeAsync(int commentId)
        {
            // * TODO: Сделать добавление в базу данных счетчика дизлайков комментария

            int like;
            Comment comment = await _context.Comment.Where(c => c.Id == commentId).FirstOrDefaultAsync();
            if (comment != null)
            {
                if (comment.Unlike == null)
                {
                    like = 1;
                    comment.Unlike = like;
                }

                if (comment.Unlike != null)
                {
                    like = (int)comment.Unlike + 1;
                    comment.Unlike = like;
                }
            }
            _context.Update(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddSubscribeAsync(Subscriber subscriber)
        {
            await _context.Subscriber.AddAsync(subscriber);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddFeedBackAsync(FeedBack feedback)
        {
            await _context.FeedBack.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return true;
        }

        // * Summary: Read
        public async Task<IList<Article>> GetArticlesAsync()
        {
            // * TODO: Сделать вывод всех статей из базы данных
            return await _context.Article.Include(sub => sub.SubCategory)
                .Include(ad => ad.ArticleDetail).Include(ad => ad.ArticleDetail.Comment).Take(100).OrderBy(d => d.Date).ToListAsync();
        }

        public async Task<IList<Category>> GetCategoriesAsync()
        {
            // * TODO: Сделать вывод всех категорий из базы данных
            return await _context.Category.Include(sub => sub.SubCategory).OrderBy(c => c.Id).ThenBy(sub => sub.Id).ToListAsync();
        }

        public IList<Category> GetCategories()
        {
            return _context.Category.Include(sub => sub.SubCategory).ToList();
        }

        public async Task<Article> GetArticleAsync(Article article)
        {
            // * TODO: Сделать вывод из базы данных статьи полностью, за исключением комментариев
            return await _context.Article.Include(ad => ad.ArticleDetail).Include(sc => sc.SubCategory)
                .Include(v => v.ArticleDetail.Video).Include(d => d.ArticleDetail.Download).FirstOrDefaultAsync(x => x.Id == article.Id);
        }

        public async Task<Article> GetArticleByIdAsync(int id)
        {
            // * TODO: Сделать вывод из базы данных статьи по id полностью, за исключением комментариев
            return await _context.Article.Include(ad => ad.ArticleDetail).Include(sc => sc.SubCategory)
                .Include(v => v.ArticleDetail.Video).Include(d => d.ArticleDetail.Download).Include(o => o.ArticleDetail.OuterDownladLink).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> GetCategoryAsync(Category category)
        {
            // * TODO: Сделать вывод из базы данных категории
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == category.Id);
        }

        public async Task<Category> GetCategoryByIdAsync(int? id)
        {
            // * TODO: Сделать вывод из базы данных категории по id
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SubCategory> GetSubCategoryAsync(SubCategory subCategory)
        {
            // * TODO: Сделать вывод из базы данных подкатегории

            return await _context.SubCategory.FirstOrDefaultAsync(x => x.Id == subCategory.Id);
        }

        public async Task<SubCategory> GetSubCategoryByIdAsync(int? id)
        {
            // * TODO: Сделать вывод из базы данных подкатегории по id

            return await _context.SubCategory.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IList<SubCategory> GetSubCategories()
        {
            return _context.SubCategory.ToList();
        }

        public async Task<IList<SubCategory>> GetSubCategoriesInCategoryAsync(Category category)
        {
            // * TODO: Сделать вывод всех подкатегорий в категории
            IList<SubCategory> subcategories = await _context.SubCategory.Where(x => x.CategoryId == category.Id).ToListAsync();
            return subcategories;
        }

        public async Task<IList<SubCategory>> GetSubCategoriesInCategoryByIdAsync(int id)
        {
            // * TODO: Сделать вывод всех подкатегорий в категории по id категории

            Category category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            IList<SubCategory> subcategories = await _context.SubCategory.Where(cat => cat.CategoryId == category.Id).ToListAsync();
            return subcategories;
        }

        public async Task<IList<Article>> GetArticlesInCategoryAsync(Category category)
        {
            // * TODO: Сделать вывод всех статей в определенной категории
            IList<Article> articlelist = await _context.Article.Include(subc => subc.SubCategory)
                .Include(ad => ad.ArticleDetail).Where(cat => cat.SubCategory.CategoryId == category.Id).ToListAsync();
            return articlelist;
        }

        public async Task<IList<Article>> GetArticlesInSubCategoryAsync(SubCategory subCategory)
        {
            // * TODO: Сделать вывод всех статей в определенной подкатегории

            SubCategory subcategory = await _context.SubCategory.FirstOrDefaultAsync(x => x.Id == subCategory.Id);
            IList<Article> article = await _context.Article.Where(subc => subc.SubCategoryId == subcategory.Id)
                .Include(ad => ad.ArticleDetail).ToListAsync();
            return article;
        }

        public async Task<ArticleDetail> GetArticleDetailAsync(Article article)
        {
            // * TODO: Сделать вывод статьи с комментариями, ссылками на материалы для скачивания, и ссылками на видеоролики
            return await _context.ArticleDetail.Include(d => d.Download).Include(v => v.Video).Include(o => o.OuterDownladLink)
                .Include(c => c.Comment).FirstOrDefaultAsync(x => x.Id == article.Id);
        }

        public async Task<ArticleDetail> GetArticleDetailByIdAsync(int id)
        {
            // * TODO: Получить статью полностью, включая комментарии пользователей, ссылками для скачивания и ссылками на видеоролики по id статьи
            return await _context.ArticleDetail.Include(d => d.Download).Include(v => v.Video).Include(o => o.OuterDownladLink)
                .Include(c => c.Comment).Include(a => a.Article).Include(sc => sc.Article.SubCategory)
                .Include(c => c.Article.SubCategory.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<ArticleDetail>> GetArticleDetailsAsync()
        {
            // * TODO: Сделать получение подробного списка статей с комментариями, ссылками на видеоролики и ссылками на документы, которые можно скачать
            IList<ArticleDetail> articleDatails = await _context.ArticleDetail.Include(ad => ad.Comment)
                .Include(ad => ad.Download).Include(ad => ad.Video).Take(100).ToListAsync();
            return articleDatails;
        }

        public async Task<IList<ArticleDetailViewModel>> GetArticleDetailViewModel()
        {
            IList<ArticleDetailViewModel> articleDetailViewModel = await _context.Comment.Include(c => c.ArticleDeatil)
                .Select((c) => new ArticleDetailViewModel
                {
                    ArticleId = c.ArticleDeatil.Id,
                    Description = c.ArticleDeatil.Description,
                    CommentId = c.Id,
                    CommentDate = c.Date,
                    UserEmail = c.UserEmail,
                    Message = c.Message
                }).Take(100).ToListAsync();
            return articleDetailViewModel;
        }

        public async Task<Video> GetVideoAsync(Video video)
        {
            return await _context.Video.FirstOrDefaultAsync(x => x.Id == video.Id);
        }

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            return await _context.Video.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Download> GetDownloadAsync(Download download)
        {
            return await _context.Download.FirstOrDefaultAsync(x => x.Id == download.Id);
        }

        public async Task<Download> GetDownloadByIdAsync(int id)
        {
            return await _context.Download.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment> GetCommentAsync(Comment comment)
        {
            return await _context.Comment.FirstOrDefaultAsync(x => x.Id == comment.Id);
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Comment>> GetCommentsAsync()
        {
            // * TODO: Сделать вывод всех статей
            return await _context.Comment.Include(c => c.ArticleDeatil).Take(100).ToListAsync();
        }

        public async Task<Subscriber> GetSubSubscribeByIdAsync(int id)
        {
            return await _context.Subscriber.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<CommentViewModel>> GetCommentViewModelAsync()
        {
            return await _context.Comment.Include(c => c.ArticleDeatil).Select(c => new CommentViewModel
            {
                CommentId = c.Id,
                CommentDate = c.Date,
                UserEmail = c.UserEmail,
                ArticleDeatilId = c.ArticleDeatilId,
                Description = c.ArticleDeatil.Description
            }).Take(100).OrderByDescending(c => c.CommentDate).ToListAsync();
        }

        // * Summary: Update
        public async Task<bool> UpdateArticleAndArticleDetailAsync(Article article, ArticleDetail articleDetail)
        {
            // * TODO: Сделать обновление анонса статьи (Article) и статьи (ArticleDetail)
            Article foundedAarticle = await _context.Article.FirstOrDefaultAsync(x => x.Id == article.Id);
            if (foundedAarticle != null)
            {
                foundedAarticle.Name = article.Name;
                foundedAarticle.Description = article.Description;
                foundedAarticle.ImageLink = article.ImageLink;
            }

            _context.Article.Update(article);
            await _context.SaveChangesAsync();

            ArticleDetail foundedArticleDetail = await _context.ArticleDetail.FirstOrDefaultAsync(x => x.Id == articleDetail.Id);
            if (foundedArticleDetail != null)
            {
                foundedArticleDetail.Name = articleDetail.Name;
                foundedArticleDetail.Text = articleDetail.Text;
                foundedArticleDetail.Image = articleDetail.Image;
                foundedArticleDetail.Description = articleDetail.Description;
            }

            _context.ArticleDetail.Update(foundedArticleDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCommentAsync(Comment comment)
        {
            Comment commentToUpdate = await _context.Comment.FirstOrDefaultAsync(c => c.Id == comment.Id);
            if (commentToUpdate != null)
            {
                commentToUpdate.Message = comment.Message;
            }

            if (commentToUpdate != null) _context.Comment.Update(commentToUpdate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            Category categoryToUpdate = await _context.Category.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
            }

            if (categoryToUpdate != null) _context.Category.Update(categoryToUpdate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSubCategoryAsync(SubCategory subCategory)
        {
            SubCategory subCategoryToUpdate = await _context.SubCategory.FirstOrDefaultAsync(c => c.Id == subCategory.Id);
            if (subCategoryToUpdate != null)
            {
                subCategoryToUpdate.Name = subCategory.Name;
            }

            if (subCategoryToUpdate != null) _context.SubCategory.Update(subCategoryToUpdate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateVideoAsync(Video video)
        {
            Video videoToUpdate = await _context.Video.FirstOrDefaultAsync(c => c.Id == video.Id);
            if (videoToUpdate != null)
            {
                videoToUpdate.Title = video.Title;
                videoToUpdate.Description = video.Description;
                videoToUpdate.Duration = video.Duration;
                videoToUpdate.Url = videoToUpdate.Url;
                videoToUpdate.Image = videoToUpdate.Image;
            }

            if (videoToUpdate != null) _context.Video.Update(videoToUpdate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDownloadAsync(Download download)
        {
            Download downloadToUpdate = await _context.Download.FirstOrDefaultAsync(c => c.Id == download.Id);
            if (downloadToUpdate != null)
            {
                downloadToUpdate.Title = download.Title;
                downloadToUpdate.Url = download.Url;
            }

            if (downloadToUpdate != null) _context.Download.Update(downloadToUpdate);
            await _context.SaveChangesAsync();
            return true;
        }

        // * Summary: Delete
        public async Task<bool> DeleteCategoryById(int id)
        {
            // * TODO: Удалить категорию из базы данных
            Category category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSubCategoryById(int id)
        {
            // * TODO: Удалить подкатегорию из базы данных
            SubCategory subCategory = await _context.SubCategory.FirstOrDefaultAsync(x => x.Id == id);
            _context.SubCategory.Remove(subCategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteArticleById(int id)
        {
            Article article = await _context.Article.FirstOrDefaultAsync(x => x.Id == id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVideoById(int id)
        {
            Video video = await _context.Video.FirstOrDefaultAsync(x => x.Id == id);
            _context.Video.Remove(video);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDownloadById(int id)
        {
            Download download = await _context.Download.FirstOrDefaultAsync(x => x.Id == id);
            _context.Download.Remove(download);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCommentById(int id)
        {
            Comment comment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        // * Summary: Find
        public async Task<SubCategory> FindSubCategoryByNameAsync(string name)
        {
            // * TODO: Сделать поиск категории
            SubCategory subCategory = await _context.SubCategory.Where(n => n.Name == name).FirstOrDefaultAsync();
            return subCategory;
        }

        public async Task<Category> FindCategoryByNameAsync(string name)
        {
            Category category = await _context.Category.Where(n => n.Name == name).FirstOrDefaultAsync();
            return category;
        }

        public async Task<IList<Article>> SearchInTitle(string textToSearch)
        {
            IList<Article> articles = await _context.Article.Where(a => a.Name.Contains(textToSearch)).Take(30).ToListAsync();
            return articles;
        }

        public async Task<IList<Article>> SearchInText(string textToSearch)
        {
            IList<Article> articles = await _context.Article.Where(a => a.Description.Contains(textToSearch)).Take(30).ToListAsync();
            return articles;
        }

        public async Task<bool> DataSeedingAsync()
        {
            Category category = new Category()
            {
                Name = "Статьи"
            };

            Category category2 = new Category()
            {
                Name = "Варез"
            };

            Category category3 = new Category()
            {
                Name = "Учебное"
            };

            SubCategory subCategoryCategory = new SubCategory()
            {
                Name = "Frontend",
                Category = category
            };

            SubCategory subCategory2Category = new SubCategory()
            {
                Name = "Backend",
                Category = category
            };

            SubCategory subCategoryCategory2 = new SubCategory()
            {
                Name = "Программы",
                Category = category2
            };

            SubCategory subCategory2Category2 = new SubCategory()
            {
                Name = "Скрипты",
                Category = category2
            };
            SubCategory subCategory3Category2 = new SubCategory()
            {
                Name = "Шаблоны",
                Category = category2
            };
            SubCategory subCategoryCategory3 = new SubCategory()
            {
                Name = "Видео материалы",
                Category = category3
            };

            SubCategory subCategory2Category3 = new SubCategory()
            {
                Name = "Книги",
                Category = category3
            };
            SubCategory subCategory3Category3 = new SubCategory()
            {
                Name = "Курсы",
                Category = category3
            };

            await _context.Category.AddRangeAsync(category, category2, category3);
            await _context.SubCategory.AddRangeAsync(subCategoryCategory, subCategory2Category, subCategoryCategory2);
            await _context.SubCategory.AddRangeAsync(subCategoryCategory2, subCategory2Category2, subCategoryCategory2);
            await _context.SubCategory.AddRangeAsync(subCategoryCategory3, subCategory2Category3, subCategory3Category3);
            await _context.SaveChangesAsync();

            Article newArticle = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/d6217743-5b36-47b2-834c-e282fbfe70cas5dBTFldt5SIDqkq3veO3ZC6sLIYye01.png",
                Raiting = 5,
                SubCategory = subCategoryCategory
            };

            ArticleDetail newArticleDetail = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle.ImageLink,
                Name = newArticle.Name,
                Description = newArticle.Description,
                Article = newArticle
            };

            Article newArticle2 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/039f6957-3729-4eed-8b08-031b1b685868MdTWrL2wFzMrxQ5nT9CM2MZYnJ1nMBIz.png",
                Raiting = 5,
                SubCategory = subCategoryCategory2
            };

            ArticleDetail newArticleDetail2 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle2.ImageLink,
                Name = newArticle2.Name,
                Description = newArticle2.Description,
                Article = newArticle2
            };
            Article newArticle3 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/be43d7e3-c034-40de-9554-959cfe42d892th_MChQhSrVtKSxSXM1c3PoDNa4CRHYeAq1.png",
                Raiting = 5,
                SubCategory = subCategoryCategory2
            };

            ArticleDetail newArticleDetail3 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle3.ImageLink,
                Name = newArticle3.Name,
                Description = newArticle3.Description,
                Article = newArticle3
            };
            Article newArticle4 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/8cdbfcf2-bdb3-4047-b179-e05029feffb3th_XSOGSftcYi7AEPJjMkMYbdskPF0K9Rf6.png",
                Raiting = 5,
                SubCategory = subCategory2Category2
            };

            ArticleDetail newArticleDetail4 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle4.ImageLink,
                Name = newArticle4.Name,
                Description = newArticle4.Description,
                Article = newArticle4
            };
            Article newArticle5 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/865702eb-c7aa-4fff-8490-cf5799f1ea13th_z2t6eLzAX3VTcBBj7ojTkTWlUaiiludV.png",
                Raiting = 5,
                SubCategory = subCategory2Category2
            };

            ArticleDetail newArticleDetail5 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle5.ImageLink,
                Name = newArticle5.Name,
                Description = newArticle5.Description,
                Article = newArticle5
            };
            Article newArticle6 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/efa3f8a5-5817-4618-b830-e539829edcd1th_tNxkMH96TXtm3nOCovqCcpUgoNTIF3V8.png",
                Raiting = 5,
                SubCategory = subCategory3Category2
            };

            ArticleDetail newArticleDetail6 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle6.ImageLink,
                Name = newArticle6.Name,
                Description = newArticle6.Description,
                Article = newArticle6
            };
            Article newArticle7 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/9beafed7-af11-49e9-bd3a-5dedd79b3264th_S9RzK6epPnV2ZwnZIdo5wOGT4rhmkbIc.png",
                Raiting = 5,
                SubCategory = subCategoryCategory3
            };

            ArticleDetail newArticleDetail7 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle7.ImageLink,
                Name = newArticle7.Name,
                Description = newArticle7.Description,
                Article = newArticle7
            };
            Article newArticle8 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/dabd4e8e-68f0-48b4-a30e-7ea65bcd16f5th_cHStzjp24iAWfmYspNR2B27eTOc8oC9B.png",
                Raiting = 5,
                SubCategory = subCategory2Category3
            };

            ArticleDetail newArticleDetail8 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle8.ImageLink,
                Name = newArticle8.Name,
                Description = newArticle8.Description,
                Article = newArticle8
            };
            Article newArticle9 = new Article()
            {
                Name = "What is Lorem Ipsum?",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",
                ImageLink = "/upload/blog_images/6e5f166c-b53e-449c-805f-ca09c899f884th_qLgFkHYd5UFmnqnGzGXJbpGe9wYJohmO.png",
                Raiting = 5,
                SubCategory = subCategory3Category3
            };

            ArticleDetail newArticleDetail9 = new ArticleDetail()
            {
                Text = "<h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><hr/><p></p><p></p><h3 style=\"text-align: center\">Lorem Ipsum</h3><hr/><p>There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don&#39;t look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn&#39;t anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.</p><p></p><hr/>",
                Image = newArticle9.ImageLink,
                Name = newArticle9.Name,
                Description = newArticle9.Description,
                Article = newArticle9
            };

            await _context.AddRangeAsync(newArticle, newArticleDetail, newArticle2, newArticleDetail2, newArticle3, newArticleDetail3, newArticle4, newArticleDetail4, newArticle5, newArticleDetail5, newArticle6, newArticleDetail6, newArticle7, newArticleDetail7, newArticle8, newArticleDetail8, newArticle9, newArticleDetail9);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddBlogVizitor(BlogVizitor blogVizitor)
        {
            blogVizitor.Date = DateTime.Now;
            await _context.AddAsync(blogVizitor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}