using Twitter.Models;

namespace Twitter.Data
{
    public interface ICommentRepository
    {
        Comment CreateComment(Comment comm);
    }
}
