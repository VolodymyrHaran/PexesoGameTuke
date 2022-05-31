namespace PexesoCore.Entity;

[Serializable]
public class Time : IEquatable<Time>
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int PlayedTime { get; set; }
    public DateTime PlayedAt { get; set; }
    
    public bool Equals(Time x)
    {
        return PlayerName.Equals(x.PlayerName);
    }
    
    public override int GetHashCode()
    {
        return PlayerName == null ? 0 : PlayerName.GetHashCode();
    }
    
}