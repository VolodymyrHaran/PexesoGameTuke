using PexesoCore.Entity;

namespace PexesoCore.Service;

public interface IRatingService
{
    void AddRating(Rating rating);

    IList<Rating> GetAllRatings();

    void ResetRatings();
}