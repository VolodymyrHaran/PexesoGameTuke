namespace PexesoCore.Entity;

[Serializable]

public class Rating : IEquatable<Rating>
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public int Rate { get; set; }
    public bool Equals(Rating? other)
    {
        return Id == other.Id && PlayerName.Equals(other.PlayerName) && Rate == other.Rate;
    }
    
    public override int GetHashCode()
    {
        return PlayerName == null ? 0 : PlayerName.GetHashCode() + Rate;
    }
}