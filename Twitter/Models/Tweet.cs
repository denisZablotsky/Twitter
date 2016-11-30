using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Twitter.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatingDate { get; set; }
        public int Likes { get; set; }
        public string Links { get; set; }
        /*
         * Navigation Property. 
         * |User| -HasMany-> |Tweet|
         */
        public virtual int UserId { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<Hashtag> Hashtags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Tweet()
        {
            Hashtags = new Collection<Hashtag>();
            Comments = new Collection<Comment>();
        }
    }
}