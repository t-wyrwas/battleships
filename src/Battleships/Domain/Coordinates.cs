public class Coordinate
{
    public const int ASCII_CODE_A = 65;
    public int X { get; init; }
    public int Y { get; init; }

    public static Coordinate From(string value)
    {
        var lengthValid = value.Length > 1 && value.Length <= 3;
        var xCoordinateStr = value.Substring(0, 1);
        var xCoordinateValid = "ABCDEFGHIJ".Split().ToList().Contains(xCoordinateStr);
        var xCoordinate = ((int)xCoordinateStr.ToCharArray()[0] + 1 - ASCII_CODE_A);
        var yCoordinateStr = value.Substring(1, value.Length);
        int yCoordinate = default(int);
        int.TryParse(yCoordinateStr, out yCoordinate);
        var yCoordinateValid = yCoordinate != default(int);

        if (!lengthValid || !xCoordinateValid || !yCoordinateValid)
        {
            throw new ArgumentException($"Invalid cooridnate string: {value}");
        }

        return new Coordinate { X = xCoordinate, Y = yCoordinate};
    }
}