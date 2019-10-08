using System;
using System.Collections.Generic;

namespace Blog.Services.Models
{
    public partial class Article
    {
        public Article()
        {
            Author = new HashSet<Author>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public string ImageLink { get; set; }
        public DateTime? Date { get; set; }
        public int? Raiting { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual ArticleDetail ArticleDetail { get; set; }
        public virtual ICollection<Author> Author { get; set; }
    }
}
