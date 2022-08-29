namespace Battleships.Domain;
using Battleships.Domain.Ships;

public class Board
{
    public Board(List<Ship> fleet)
    {
        Fleet = fleet;
    }

    public List<Ship> Fleet {get;}

    public (MoveResult result, Ship? ship) Fire(Coordinate at)
    {
        foreach (var ship in Fleet)
        {
            var result = ship.TryHit(at);
            if(result == MoveResult.Hit)
            {
                return (result, ship);
            }
        }
        return (MoveResult.Miss, null);
    }

    public bool DoesFleetStillExist() => Fleet.Any(s => s.GetState() != ShipState.Sunk);

}