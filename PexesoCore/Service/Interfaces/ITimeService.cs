using PexesoCore.Entity;

namespace PexesoCore.Service;

public interface ITimeService
{
    void AddTime(Time time);

    IList<Time> GetTopTime();

    void ResetTime();
}