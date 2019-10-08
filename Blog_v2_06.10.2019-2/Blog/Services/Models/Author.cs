namespace Blog.Services.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
