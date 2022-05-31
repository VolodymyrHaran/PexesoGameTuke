namespace PexesoCore;
[Serializable]
public class Card
{
    public Card(int id, int index)
    {
        Id = id;
        Shown = false;
        Index = index;
        IsGuessed = false;
    }
    
    public int Id { get;}
    public bool Shown { get; set; }
    
    public int Index { get; set; }
    
    public bool IsGuessed { get; set; }
}