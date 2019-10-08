using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Services.ViewModel
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public DateTime? CommentDate { get; set; }
        public string UserEmail { get; set; }
        public int ArticleDeatilId { get; set; }
        public string Description { get; set; }
    }
}
