namespace Blog.Services.Models
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public int ArticleDetailId { get; set; }

        public virtual ArticleDetail ArticleDetail { get; set; }
    }
}
