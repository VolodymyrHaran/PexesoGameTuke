namespace PexesoCore.Core
{
    public class Program    
    {
        public static void Main()
        {
            var game = new ConsoleUI(new Field(4, 7)).Play();
            while(game) game = new ConsoleUI(new Field(4, 7)).Play();
        }
    }
}