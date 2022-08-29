namespace Battleships.Domain;
using Battleships.Domain.Ships;

public class Board
{
    private List<Ship> _ships;

    public Board(List<Ship> ships)
    {
        _ships = ships;
    }

    public (MoveResult result, Ship? ship) Fire(Coordinate at)
    {
        foreach (var ship in _ships)
        {
            var result = ship.TryHit(at);
            if(result == MoveResult.Hit)
            {
                return (result, ship);
            }
        }
        return (MoveResult.Miss, null);
    }
}