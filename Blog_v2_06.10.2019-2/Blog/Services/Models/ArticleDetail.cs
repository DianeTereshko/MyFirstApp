using System;
using System.Collections.Generic;

namespace Blog.Services.Models
{
    public partial class ArticleDetail
    {
        public ArticleDetail()
        {
            Comment = new HashSet<Comment>();
            Download = new HashSet<Download>();
            OuterDownladLink = new HashSet<OuterDownladLink>();
            Video = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int ArticleId { get; set; }
        public string Image { get; set; }
        public int? Like { get; set; }
        public int? Unlike { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public string LinkToFlipBook { get; set; }

        public virtual Article Article { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Download> Download { get; set; }
        public virtual ICollection<OuterDownladLink> OuterDownladLink { get; set; }
        public virtual ICollection<Video> Video { get; set; }
    }
}
