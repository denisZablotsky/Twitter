using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Twitter.Models
{
    public class Hashtag
    {
        public int Id { get; set; }
        public string Tag { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }

        public Hashtag()
        {
            Tweets = new Collection<Tweet>();
        }
    }
}