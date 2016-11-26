
using Twitter.Models;

namespace Twitter.Data
{
    public class CommentRepository : ICommentRepository
    {
        EfDbContext context;
        public CommentRepository()
        {
            context = new EfDbContext();
        }
        public Comment CreateComment(Comment comm)
        {
            comm.TweetId = comm.Id;
            Comment comment = context.Comments.Add(comm);
            context.SaveChanges();
            return comment;
        }
    }
}