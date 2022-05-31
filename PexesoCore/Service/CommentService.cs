using Microsoft.EntityFrameworkCore;
using PexesoCore.Entity;

namespace PexesoCore.Service;

public class CommentService : ICommentService
{
    public void AddComment(Comment comment)
    {
        using (var context = new PexesoDbContext())
        {
            context.comments.Add(comment);
            context.SaveChanges();
        }
    }

    public IList<Comment> GetAllComments()
    {
        using (var context = new PexesoDbContext())
        {
            return (from s in context.comments select s).ToList(); 
        }
    }

    public void ResetComments()
    {
        string nameTable = "comments";
        using (var context = new PexesoDbContext())
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + nameTable);
        }
    }
}