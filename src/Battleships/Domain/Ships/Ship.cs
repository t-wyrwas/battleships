namespace Battleships.Domain.Ships;
using Battleships.Domain;

public class Ship
{
    // TODO ship should be identifiable by id
    public ShipType Type { get; init; }
    public Coordinate Position { get; init; }
    public Direction Direction { get; init; }

    public ShipState GetState()
    {
        if (Hits.Count > 0 && Hits.Count < ShipLength())
        {
            return ShipState.Damaged;
        }
        if (Hits.Count == ShipLength())
        {
            return ShipState.Sunk;
        }
        return ShipState.Operational;
    }

    public HashSet<Coordinate> GetOccupiedPositions()
    {
        var occupied = new HashSet<Coordinate> { Position };
        for (int i = 1; i < (int)Type; ++i)
        {
            var _ = Direction switch
            {
                Direction.N => occupied.Add(new Coordinate(Position.X, Position.Y - i)),
                Direction.E => occupied.Add(new Coordinate(Position.X + i, Position.Y)),
                Direction.S => occupied.Add(new Coordinate(Position.X, Position.Y + i)),
                Direction.W => occupied.Add(new Coordinate(Position.X - i, Position.Y)),
                _ => throw new ArgumentException($"Wrong ship type: {Direction}")
            };
        }
        return occupied;
    }

    public MoveResult TryHit(Coordinate at)
    {
        var isHit = Direction switch
        {
            Direction.N => at.X == Position.X && (at.Y <= Position.Y && at.Y > Position.Y - ShipLength()),
            Direction.E => at.Y == Position.Y && (at.X >= Position.X && at.X <= Position.X + ShipLength()),
            Direction.S => at.X == Position.X && (at.Y >= Position.Y && at.Y <= Position.Y + ShipLength()),
            Direction.W => at.Y == Position.Y && (at.X <= Position.X && at.X > Position.X - ShipLength()),
            _ => false,
        };

        if (isHit)
        {
            Hits.Add(at);
            return MoveResult.Hit;
        }

        return MoveResult.Miss;
    }

    private int ShipLength() => (int)Type;
    private HashSet<Coordinate> Hits = new HashSet<Coordinate>();
}