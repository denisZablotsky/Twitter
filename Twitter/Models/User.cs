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
        public string Email { get; set; }
        public DateTime SignupData { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }

        public User()
        {
            Tweets = new Collection<Tweet>();
        }
    }
}