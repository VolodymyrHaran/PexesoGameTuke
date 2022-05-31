using Microsoft.AspNetCore.Mvc;
using PexesoCore.Entity;
using PexesoCore.Service;

namespace PexesoWeb.APIControllers;

//https:/localhost:7149/Time
[Route("[controller]")]
[ApiController]
public class TimeController : ControllerBase
{
    private ITimeService timeService = new TimeServiceEF();

    [HttpGet]
    public IEnumerable<Time> GetTimes()
    {
        return timeService.GetTopTime();
    }

    [HttpPost]
    public void PostTime(Time time)
    {
        time.PlayedAt = DateTime.UtcNow;
        timeService.AddTime(time);
    }
}