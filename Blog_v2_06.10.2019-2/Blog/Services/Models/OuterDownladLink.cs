namespace Blog.Services.Models
{
    public partial class OuterDownladLink
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileShareLink { get; set; }
        public int ArticleDetailId { get; set; }

        public virtual ArticleDetail ArticleDetail { get; set; }
    }
}
