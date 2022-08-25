namespace Battleships.Domain;

public record Coordinate
{
    public const int ASCII_CODE_A = 65;
    public int X { get; }
    public int Y { get; }
    public Coordinate(int x, int y)
    {
        var invalidX = x < 1 || x > 10;
        var invalidY = y < 1 || y > 10;
        if(invalidX || invalidY)
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
        var xCoordinateStr = value.Substring(0, 1);
        var xCoordinateValid = "ABCDEFGHIJ".Contains(xCoordinateStr); // TODO
        var xCoordinate = ((int)xCoordinateStr.ToCharArray()[0] + 1 - ASCII_CODE_A);
        var yCoordinateStr = value.Substring(1, value.Length - 1);
        int yCoordinate = default(int);
        int.TryParse(yCoordinateStr, out yCoordinate);
        var yCoordinateValid = yCoordinate >= 1 && yCoordinate <= 10;

        if (!xCoordinateValid || !yCoordinateValid)
        {
            throw new ArgumentException($"Invalid cooridnate string: {value}");
        }

        return new Coordinate(xCoordinate, yCoordinate);
    }
}