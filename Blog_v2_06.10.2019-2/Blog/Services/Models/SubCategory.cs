using System.Collections.Generic;

namespace Blog.Services.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Article = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Article> Article { get; set; }
    }
}
