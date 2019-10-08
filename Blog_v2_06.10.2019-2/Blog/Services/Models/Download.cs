namespace Blog.Services.Models
{
    public partial class Download
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ArticleDetailId { get; set; }

        public virtual ArticleDetail ArticleDetail { get; set; }
    }
}
