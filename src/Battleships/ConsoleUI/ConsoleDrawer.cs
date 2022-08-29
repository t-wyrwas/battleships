using Battleships.UI;
using Battleships.Domain;
using Battleships.Domain.Ships;

namespace Battleships.ConsoleUI;

public class ConsoleDrawer : IBoardDrawer
{
    private const char HIT_MARK = 'X';
    private const char MISS_MARK = 'O';
    private const char SHIP_MARK = 'V';
    private const char SUNK_SHIP_MARK = 'S';
    private readonly int _boardX;
    private readonly int _boardY;
    private readonly int _commandPaneX;
    private readonly int _commandPaneY;


    public ConsoleDrawer(int x, int y)
    {
        _boardX = x;
        _boardY = y;
        _commandPaneX = x;
        _commandPaneY = y + 30;
    }

    public void DrawBoard()
    {
        Console.Clear();
        Console.SetCursorPosition(3, 1);
        Console.WriteLine("Battleships");

        Console.SetCursorPosition(_boardX + 2, _boardY);
        var columnIds = "ABCDEFGHIJ";
        foreach (var c in columnIds)
        {
            Console.Write($"{c} ");
        }

        for (int i = 1; i < 11; ++i)
        {
            Console.SetCursorPosition(_boardX, _boardY + i);
            Console.Write(i);
        }
    }

    ~ConsoleDrawer()
    {
        Console.Clear();
    }

    public void DrawHit(Coordinate coordinate)
    {
        // Console.SetCursorPosition(coordinate.X * 2 + _boardX, coordinate.Y + _boardY);
        var (x, y) = GetConsoleCoordinates(coordinate);
        Console.SetCursorPosition(x, y);
        Console.Write(HIT_MARK);
    }

    public void DrawMiss(Coordinate coordinate)
    {
        var (x, y) = GetConsoleCoordinates(coordinate);
        Console.SetCursorPosition(x, y);
        Console.Write(MISS_MARK);
    }

    public void DrawShip(Ship ship, bool asSunk = false)
    {
        var (x, y) = GetConsoleCoordinates(ship.Position);
        for (int i = 0; i < (int)ship.Type; ++i)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(asSunk ? SUNK_SHIP_MARK : SHIP_MARK);
            (x, y) = ship.Direction switch
            {
                Direction.N => (x, y - 1),
                Direction.E => (x + 2, y),
                Direction.S => (x, y + 1),
                Direction.W => (x - 2, y),
            };
        }
    }

    public void WriteMessage(string msg)
    {
        Console.SetCursorPosition(_commandPaneX, _commandPaneY - 3);
        Console.Write("                                           "); // TODO this is lame
        Console.SetCursorPosition(_commandPaneX, _commandPaneY - 3);
        Console.Write(msg);
    }

    public (UserCommand command, Coordinate? coordinate) FetchUserInput()
    {
        string? input = "";
        while (true)
        {
            Console.SetCursorPosition(_commandPaneX, _commandPaneY);
            Console.WriteLine("Provide coordinates to fire at! (e.g.: A5, J8)");
            Console.WriteLine("\\q - quit");
            Console.WriteLine("\\r - restart");
            Console.Write("> ");
            var position = Console.GetCursorPosition();
            input = Console.ReadLine();
            Console.SetCursorPosition(position.Left, position.Top);
            Console.Write("                                ");  // TODO this is lame
            Console.SetCursorPosition(position.Left, position.Top);
            switch (input)
            {
                case "\\q":
                    return (UserCommand.QuitGame, null);
                case "\\r":
                    return (UserCommand.RestartGame, null);
                default:
                    try
                    {
                        return (UserCommand.NextMove, Coordinate.From(input!));
                    }
                    catch (ArgumentException)
                    {
                        WriteMessage("Invalid input! - try again");
                    }
                    break;
            };
        }
    }

    private (int x, int y) GetConsoleCoordinates(Coordinate coordinate) => (_boardX + coordinate.X * 2, _boardY + coordinate.Y);
}