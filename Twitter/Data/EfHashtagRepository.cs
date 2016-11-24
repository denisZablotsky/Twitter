using System;
using System.Linq;
using Twitter.Models;

namespace Twitter.Data
{
    public class EfHashtagRepository : IHashtagRepository
    {
        private EfDbContext context;
        public EfHashtagRepository()
        {
            context = new EfDbContext();
        }
        public IQueryable<Hashtag> Hashtags
        {
            get
            {
                return context.Hashtags;
            }
        }

        public Hashtag CreateHashtag(Hashtag hashtag)
        {
            Hashtag Hashtag = context.Hashtags.Add(hashtag);
            context.SaveChanges();
            return Hashtag;
        }

        public Hashtag GetHashtagById(int id)
        {
            return context.Hashtags.Find(id);
        }

        public Hashtag GetHashtagByTag(string tag)
        {
            return context.Hashtags.SingleOrDefault(x => x.Tag == tag);
        }
    }
}