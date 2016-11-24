using Twitter.Models;
using System.Linq;

namespace Twitter.Data
{
    public interface IHashtagRepository
    {
        IQueryable<Hashtag> Hashtags { get; }
        Hashtag GetHashtagByTag(string tag);
        Hashtag CreateHashtag(Hashtag hashtag);
        Hashtag GetHashtagById(int id);
    }
}
