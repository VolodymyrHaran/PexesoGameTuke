using Microsoft.AspNetCore.Mvc;
using PexesoCore.Entity;
using PexesoCore.Service;

namespace PexesoWeb.APIControllers;

[Route("[controller]")]
[ApiController]

public class RatingController
{
    private IRatingService commentService = new RatingService();
    
    [HttpGet]
    public IEnumerable<Rating> GetComments()
    {
        return commentService.GetAllRatings();
    }

    [HttpPost]
    public void PostComment(Rating rating)
    {
        commentService.AddRating(rating);
    }
}