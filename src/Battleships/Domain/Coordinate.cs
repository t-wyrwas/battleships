namespace Battleships.Domain;

public record struct Coordinate
{
    public int X { get; }
    public int Y { get; }
    public const int MAX = 10;
    public const int MIN = 1;

    public Coordinate(int x, int y)
    {
        var invalidX = x < MIN || x > MAX;
        var invalidY = y < MIN || y > MAX;
        if (invalidX || invalidY)
        {
            throw new ArgumentException($"Invalid coordinates: {x}:{y}");
        }
        X = x;
        Y = y;
    }

    public static Coordinate From(string value)
    {
        var lengthValid = value.Length > 1 && value.Length <= 3;
        if (!lengthValid)
        {
            throw new ArgumentException($"Invalid cooridnate string: {value}");
        }

        value = value.ToUpper();

        var xCoordinateStr = value.Substring(0, 1);
        var xCoordinate = ((int)xCoordinateStr.ToCharArray()[0] + 1 - ASCII_CODE_A);

        var yCoordinateStr = value.Substring(1, value.Length - 1);
        int yCoordinate = default(int);
        int.TryParse(yCoordinateStr, out yCoordinate);

        return new Coordinate(xCoordinate, yCoordinate);
    }

    private const int ASCII_CODE_A = 65;
    private const string X_AXIS_VALUES = "ABCDEFGHIJ";
}