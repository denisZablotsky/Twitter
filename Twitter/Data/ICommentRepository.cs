using Twitter.Models;
using System.Linq;

namespace Twitter.Data
{
    public interface ICommentRepository
    {
        Comment CreateComment(Comment comm);
        IQueryable<Comment> GetCommentsByTweetId(int TweetId);
    }
}
