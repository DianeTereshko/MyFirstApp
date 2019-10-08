using System;

namespace Blog.Services.Models
{
    public partial class BlogVizitor
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public DateTime? Date { get; set; }
    }
}
