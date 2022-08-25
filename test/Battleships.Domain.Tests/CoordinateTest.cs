namespace Battleships.Domain.Tests;

using Battleships.Domain;
using FluentAssertions;

public class CoordinateTest
{
    [Fact]
    public void FromString_CorrectStringRepresentation_ReturnsCorrectCoordinate()
    {
        var expectedCoordinate = new Coordinate { X = 3, Y = 8 };
        var coordinate = Coordinate.From("C8");
        coordinate.Should().Be(expectedCoordinate);
    }

    [Fact]
    public void FromString_A1_ReturnsCorrectCoordinate()
    {
        var expectedCoordinate = new Coordinate { X = 1, Y = 1 };
        var coordinate = Coordinate.From("A1");
        coordinate.Should().Be(expectedCoordinate);
    }

    [Fact]
    public void FromString_J10_ReturnsCorrectCoordinate()
    {
        var expectedCoordinate = new Coordinate { X = 10, Y = 10 };
        var coordinate = Coordinate.From("J10");
        coordinate.Should().Be(expectedCoordinate);
    }

}