namespace PexesoCore.Entity;

[Serializable]

public class Comment : IEquatable<Comment>
{
    
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public string PlayerComment { get; set; }
    public bool Equals(Comment? other)
    {
        return Id == other.Id && PlayerName.Equals(other.PlayerName) && PlayerComment.Equals(other.PlayerComment);
    }
    
    public override int GetHashCode()
    {
        return PlayerName == null && PlayerComment == null ? 0 : PlayerName.GetHashCode() + PlayerComment.GetHashCode();
    }

}