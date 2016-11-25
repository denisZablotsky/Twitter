using System;

namespace Twitter.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatingDate { get; set; }

        public int AuthourId { get; set; }
        public string AuthorName { get; set; }

        public virtual int TweetId { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
}