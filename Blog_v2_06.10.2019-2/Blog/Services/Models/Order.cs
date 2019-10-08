using System;

namespace Blog.Services.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Site { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Complete { get; set; }
        public string NickName { get; set; }
        public string Message { get; set; }
    }
}
