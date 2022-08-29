namespace Battleships.Domain.Ships;

public interface IFleetGenerator
{
    List<Ship> GenerateFleet();
}
