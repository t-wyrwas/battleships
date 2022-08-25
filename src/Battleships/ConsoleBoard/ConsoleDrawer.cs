using Battleships.Board;

namespace Battleships.ConsoleBoard;

public class ConsoleDrawer : IBoardDrawer
{
    private readonly int _x;
    private readonly int _y;

    public ConsoleDrawer(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void DrawBoard()
    {
        Console.Clear();
        Console.SetCursorPosition(3, 1);
        Console.WriteLine("Battleships");
        Console.SetCursorPosition(_y + 1, _x);
        var columnIds = "ABCDEFGHIJ";

        foreach (var c in columnIds)
        {
            Console.Write($" {c}");
        }

        for (int i = 1; i < 11; ++i)
        {
            Console.SetCursorPosition(i < 10 ? _y : _y - 1, _x + i);
            Console.Write(i);
        }
    }

    
}