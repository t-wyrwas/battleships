namespace Battleships.Domain.Ships;

public class DummyFleetGenerator : IFleetGenerator
{
    public List<Ship> GenerateFleet(IEnumerable<ShipType> types)
    {
        return new List<Ship> { new Ship { Direction = Direction.N, Position = Coordinate.From("C8"), Type = ShipType.Destroyer },
                                new Ship { Direction = Direction.W, Position = Coordinate.From("I2"), Type = ShipType.Battleship } };
    }
}