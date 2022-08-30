namespace Battleships.Domain.Ships;

public class RandomFleetGenerator : IFleetGenerator
{
    public List<Ship> GenerateFleet(IEnumerable<ShipType> types)
    {
        var fleet = new List<Ship>();
        var occupiedPositions = new HashSet<Coordinate>();

        foreach (var shipType in types)
        {
            while (true)
            {
                var newShip = GenerateShip(shipType);
                var shipOccupiedPositions = newShip.GetOccupiedPositions();
                if (occupiedPositions.Overlaps(shipOccupiedPositions))
                {
                    continue;
                }
                else
                {
                    occupiedPositions.UnionWith(shipOccupiedPositions);
                    fleet.Add(newShip);
                    break;
                }
            }
        }

        return fleet;
    }

    private Ship GenerateShip(ShipType type)
    {
        var random = new Random();
        var shipLength = (int)type;
        var direction = (Direction)random.Next((int)Direction.N, (int)Direction.W + 1);
        var position = direction switch
        {
            Direction.N => new Coordinate(x: random.Next(Coordinate.MIN, Coordinate.MAX + 1), y: random.Next(shipLength, Coordinate.MAX + 1)),
            Direction.E => new Coordinate(x: random.Next(Coordinate.MIN, Coordinate.MAX + 1 - shipLength + 1), y: random.Next(Coordinate.MIN, Coordinate.MAX + 1)),
            Direction.S => new Coordinate(x: random.Next(Coordinate.MIN, Coordinate.MAX + 1), y: random.Next(Coordinate.MIN, Coordinate.MAX + 1 - shipLength + 1)),
            Direction.W => new Coordinate(x: random.Next(Coordinate.MIN + shipLength, Coordinate.MAX + 1), y: random.Next(Coordinate.MIN, Coordinate.MAX + 1)),
            _ => throw new ArgumentException($"Wrong ship type: {direction}"),
        };
        return new Ship { Direction = direction, Position = position, Type = type };
    }
}