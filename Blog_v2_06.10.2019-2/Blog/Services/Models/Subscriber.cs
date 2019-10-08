using System;

namespace Blog.Services.Models
{
    public partial class Subscriber
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? Date { get; set; }
    }
}
