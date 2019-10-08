using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.ViewModel
{
    public class ArticleDetailViewModel
    {
        public int ArticleId { get; set; }
        public string Description { get; set; }
        public int CommentId { get; set; }
        public DateTime? CommentDate { get; set; }
        public string UserEmail { get; set; }
        public string Message { get; set; }
    }
}
