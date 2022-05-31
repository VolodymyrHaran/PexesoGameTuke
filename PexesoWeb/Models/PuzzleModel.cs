using PexesoCore.Entity;
using PexesoCore.Core;

namespace PexesoWeb.Models;

public class PuzzleModel
{
    public Field Field { get; set; }
    
    public IList<Time> Times { get; set; }
    
    public IList<Rating> Ratings { get; set; }
    
    public IList<Comment> Comments { get; set; }
}