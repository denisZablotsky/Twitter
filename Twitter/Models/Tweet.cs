using System;

namespace Twitter.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatingDate { get; set; }

        /*
         * Navigation Property. 
         * |User| -HasMany-> |Tweet|
         */
        public virtual int UserId { get; set; }
        public virtual User Author { get; set; }
    }
}