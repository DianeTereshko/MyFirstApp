using System;

namespace Blog.Services.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? Date { get; set; }
        public int ArticleDeatilId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public int? Like { get; set; }
        public int? Unlike { get; set; }
        public string LinkToFile { get; set; }

        public virtual ArticleDetail ArticleDeatil { get; set; }
    }
}
