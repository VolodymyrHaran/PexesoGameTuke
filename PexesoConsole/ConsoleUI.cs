using PexesoCore.Entity;
using PexesoCore.Service;

namespace PexesoCore.Core;

public class ConsoleUI
{
    private readonly Field _field;
    //private readonly ITimeService _timeService = new TimeService();
    private readonly ITimeService _timeService = new TimeServiceEF();
    public ConsoleUI(Field field)
    {
        _field = field;
    }
    
    private void ShowField()
    {
        Console.WriteLine("Time: " + _field.GetTime());
        for (int row = 0; row < _field.RowCount; row++)
        {
            for (int column = 0; column < _field.ColumnCount; column++)
            {
                Console.Write(_field.GetCard(row,column).Shown ? _field.GetCard(row,column).Id: 0); //if card close - open it
                Console.Write("\t");
            }
            Console.WriteLine();
        }
    }

    private Card Input()
    {
        Console.Write("Input the tiles row and column to open: ");
        var inputArray = Console.ReadLine().Split(); //parse string row column
        int row,column;
        try
        {
            row = int.Parse(inputArray[0]) - 1;
            column = int.Parse(inputArray[1]) - 1;
        }
        catch (Exception e)
        {
            row = column = -1;
        }
        while (inputArray.Length != 2 || row < 0 || column<0 || row>=_field.RowCount || column>=_field.ColumnCount ||_field.IsOpen(row,column))
        { //check field and input 
            Console.Write("Input correct row and column: ");
            inputArray = Console.ReadLine().Split();
            try
            {
                row = int.Parse(inputArray[0]) - 1;
                column = int.Parse(inputArray[1]) - 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Row or Column is not Integer");
            }
        }

        Card card = _field.GetCard(row, column);
        while (!_field.OpenCard(card))
        {
            Console.WriteLine("Select another card, this card is already open");
            card = Input();
            Console.Clear();
            ShowField();
        }
        Console.Clear();
        ShowField();
        return card;
    }

    public bool Play()
    {
        Console.WriteLine("Input ur name: ");
        string playerName = Console.ReadLine();
        while (playerName != null && playerName.Length<1)
        {
            Console.WriteLine("Input correct name: ");
            playerName = Console.ReadLine();
        }
        
        while (!_field.CheckWin()) //start game
        {
            Console.Clear();
            PrintTopTime();
            ShowField();
            
            Console.WriteLine("First card input");
            Card firstCard = Input();
            Console.WriteLine("Second card input");
            Card secondCard = Input();
            _field.StartTime();
            while (firstCard==secondCard)
            {
                Console.WriteLine("Select another card");
                secondCard = Input();
            }
            if (_field.CheckCards(firstCard, secondCard)) Console.WriteLine("Nice, press any key to continue.");
            else
            {
                Console.WriteLine("Cards are different, press any key to continue.");
                _field.CloseCard(firstCard);
                _field.CloseCard(secondCard);
            }
            Console.ReadKey();
        }
        _timeService.AddTime(new Time{PlayerName = playerName, PlayedTime = _field.GetTime(), PlayedAt = DateTime.UtcNow});
        Console.WriteLine("You win!");
        PrintTopTime();
        Console.WriteLine("Play again? Yes or no");
        var end = Console.ReadLine().ToLower();
        while (end != "yes" && end != "no")
        {
            Console.WriteLine("Play again? Write \"yes\" or \"no\"");
            end = Console.ReadLine().ToLower();
        }
        if(end.Equals("yes")) return true;
        return false;
    }

    public void PrintTopTime()
    {
        foreach (var time in _timeService.GetTopTime())
        {
            Console.WriteLine("{0} {1}",time.PlayerName,time.PlayedTime);
        }
    }
}