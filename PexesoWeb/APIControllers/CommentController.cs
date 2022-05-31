using Microsoft.AspNetCore.Mvc;
using PexesoCore.Entity;
using PexesoCore.Service;

namespace PexesoWeb.APIControllers;

[Route("[controller]")]
[ApiController]

public class CommentController
{
    private ICommentService commentService = new CommentService();
    
    [HttpGet]
    public IEnumerable<Comment> GetComments()
    {
        return commentService.GetAllComments();
    }

    [HttpPost]
    public void PostComment(Comment comment)
    {
        commentService.AddComment(comment);
    }
}