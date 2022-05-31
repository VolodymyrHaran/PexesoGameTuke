using PexesoCore.Entity;

namespace PexesoCore.Service;

public interface ICommentService
{
    void AddComment(Comment comment);

    IList<Comment> GetAllComments();

    void ResetComments();
}