using Microsoft.EntityFrameworkCore;
using PexesoCore.Entity;

namespace PexesoCore.Service;

public class TimeServiceEF : ITimeService
{
    public void AddTime(Time time)
    {
        using (var context = new PexesoDbContext())
        {
            context.times.Add(time);
            context.SaveChanges();
        }
    }

    public IList<Time> GetTopTime()
    {
        using (var context = new PexesoDbContext())
        {
            var list = (from s in context.times orderby s.PlayedTime select s).ToList(); //sort by time
            return list.Distinct().Take(3).ToList(); //remove duplicates and take first 3
        }
        
    }

    public void ResetTime()
    {
        string nameTable = "times";
        using (var context = new PexesoDbContext())
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + nameTable);
        }
    }
}