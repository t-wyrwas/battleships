using Battleships.UI;
using Battleships.Domain;

namespace Battleships.ConsoleBoard;

public class ConsoleDrawer : IBoardDrawer
{
    private const char HIT_MARK = 'X';
    private const char MISS_MARK = 'O';
    private const char SHIP_MARK = '\u2B1C';
    private readonly int _boardX;
    private readonly int _boardY;

    public ConsoleDrawer(int x, int y)
    {
        _boardX = x;
        _boardY = y;
    }

    public void DrawBoard()
    {
        Console.Clear();
        Console.SetCursorPosition(3, 1);
        Console.WriteLine("Battleships");
        Console.SetCursorPosition(_boardY + 1, _boardX);
        var columnIds = "ABCDEFGHIJ";

        foreach (var c in columnIds)
        {
            Console.Write($" {c}");
        }

        for (int i = 1; i < 11; ++i)
        {
            Console.SetCursorPosition(i < 10 ? _boardY : _boardY - 1, _boardX + i);
            Console.Write(i);
        }
    }

    public void DrawHit(Coordinate coordinate)
    {
        Console.SetCursorPosition(coordinate.X * 2 + _boardX, coordinate.Y + _boardY);
        Console.Write(HIT_MARK);
    }

    public void DrawMiss(Coordinate coordinate)
    {
        Console.SetCursorPosition(coordinate.X * 2 + _boardX, coordinate.Y + _boardY);
        Console.Write(MISS_MARK);
    }

    public void DrawShip(IEnumerable<Coordinate> shape)
    {
        foreach (var coordinate in shape)
        {
            Console.SetCursorPosition(coordinate.X * 2 + _boardX, coordinate.Y + _boardY);
            Console.Write(SHIP_MARK);
        }
    }
}