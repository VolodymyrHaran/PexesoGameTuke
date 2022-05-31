namespace PexesoCore.Core;

[Serializable]
public class Field
{
    private readonly Card[,] _cards;
    public int RowCount { get; }
    public int ColumnCount { get; }
    private DateTime startTime;
    private bool IsStarted;
    public bool FirstTimePlay = true;
    public string nick = "s";
    public Card firstCard;
    public Card secondCard;
    public bool cardsArediff = false;

    public Field(int rowCount, int columnCount)
    {
        RowCount = rowCount;
        ColumnCount = columnCount;
        _cards = new Card[rowCount, columnCount];
        startTime = DateTime.UtcNow;
        IsStarted = false;
        Generate();
    }

    public Card GetCard(int row, int column)
    {
        return _cards[row, column];
    }
    public Card[,] GetCards()
    {
        return _cards;
    }

    private void Generate()
    {
        Random rnd = new Random();
        int count = RowCount * ColumnCount / 2;
        var intList = new List<int>();
        int x = 0;
        for (int row = 0; row < RowCount; row++)
        {
            for (int column = 0; column < ColumnCount; column++)
            {
                var newEl = rnd.Next(1, count + 1);
                while (intList.Count(newEl.Equals) >= 2)
                {
                    newEl = rnd.Next(1, count + 1);
                }
                intList.Add(newEl);
                _cards[row, column] = new Card(newEl, x);
                x++;
            }
        }
    }

    public Card GetCardByIndex(int index)
    {//тут ошибка
        int x = -1;
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                x++;
                if (x == index) return GetCard(i, j);
                
            }
        }

        return null;
    }
    public void OpenCard(Card card)
    {
        card.Shown = true;
    }

    public void CloseCard(Card card)
    {
        if(!card.IsGuessed) card.Shown = false;
    }

    public bool CheckCards()
    {
        return firstCard.Id == secondCard.Id;
    }

    public bool IsOpen(int row, int column)
    {
        return _cards[row, column].Shown;
    }

    public bool CheckWin()
    {
        foreach (var card in _cards)
        {
            if (!card.Shown) return false;
        }
        return true;
    }

    public void StartTime()
    {
        startTime = DateTime.UtcNow;
        IsStarted = true;
    }

    public int GetTime()
    {
        return IsStarted ? (DateTime.UtcNow - startTime).Seconds : 0;
    }
}