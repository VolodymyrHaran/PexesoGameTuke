using System.Runtime.Serialization.Formatters.Binary;
using PexesoCore.Entity;

namespace PexesoCore.Service;

public class TimeService : ITimeService
{
    private const string FileName = "time.bin";
    private List<Time> _times = new List<Time>();
    public void AddTime(Time time)
    {
        _times.Add(time);
        SaveTime();
    }

    public IList<Time> GetTopTime()
    {
        LoadTime();
        return _times.OrderBy(t => t.PlayedTime).Take(3).ToList();
    }

    public void ResetTime()
    {
        _times = new List<Time>();
        SaveTime();
    }

    private void SaveTime()
    {
        using (var fs = File.OpenWrite(FileName))
        {
            var bf = new BinaryFormatter();
            bf.Serialize(fs,_times);
        }
    }

    private void LoadTime()
    {
        if (File.Exists(FileName))
        {
            using (var fs = File.OpenRead(FileName))
            {
                var bf = new BinaryFormatter();
                _times = (List<Time>) bf.Deserialize(fs);
            }
        }
    }
}