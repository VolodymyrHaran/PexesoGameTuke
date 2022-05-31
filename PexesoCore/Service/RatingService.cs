using Microsoft.EntityFrameworkCore;
using PexesoCore.Entity;

namespace PexesoCore.Service;

public class RatingService : IRatingService
{
    public void AddRating(Rating rating)
    {
        using (var context = new PexesoDbContext())
        {
            context.ratings.Add(rating);
            context.SaveChanges();
        }
    }

    public IList<Rating> GetAllRatings()
    {
        using (var context = new PexesoDbContext())
        {
            return (from s in context.ratings select s).ToList(); 
        }
    }

    public void ResetRatings()
    {
        string nameTable = "ratings";
        using (var context = new PexesoDbContext())
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + nameTable);
        }
    }
}