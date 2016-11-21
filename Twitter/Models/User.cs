using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Twitter.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public virtual UserProfile Profile { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }

        public virtual ICollection<User> Followings { get; set; }
        public virtual ICollection<User> Followers { get; set; }

        public User()
        {
            Tweets = new Collection<Tweet>();
            Followers = new Collection<User>();
            Followings = new Collection<User>();
        }
    }
}