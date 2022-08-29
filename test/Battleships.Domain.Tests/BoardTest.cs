namespace Battleships.Domain.Tests;

using Battleships.Domain;
using Battleships.Domain.Ships;
using FluentAssertions;

public class BoardTest
{
    [Fact]
    public void Fire_AtEmptyCoordinate_ReturnsMiss()
    {
        var sut = new Board(new List<Ship> { new Ship { Direction = Direction.N, Position = Coordinate.From("C8"), Type = ShipType.Destroyer } });

        var (result, ship) = sut.Fire(Coordinate.From("A5"));

        result.Should().Be(MoveResult.Miss);
        ship.Should().Be(null);
    }

    [Fact]
    public void Fire_AtOccupiedCoordinate_ReturnsHit()
    {
        var ship = new Ship { Direction = Direction.N, Position = Coordinate.From("C8"), Type = ShipType.Destroyer };
        var sut = new Board(new List<Ship> { ship });

        var (result, hitShip) = sut.Fire(Coordinate.From("C6"));

        result.Should().Be(MoveResult.Hit);
        hitShip.Should().Be(ship);
        hitShip!.GetState().Should().Be(ShipState.Damaged);
    }

    [Fact]
    public void Fire_AtLastShipCoordinate_ReturnsSink()
    {
        var ship = new Ship { Direction = Direction.N, Position = Coordinate.From("C8"), Type = ShipType.Destroyer };
        var sut = new Board(new List<Ship> { ship });

        _ = sut.Fire(Coordinate.From("C8"));
        _ = sut.Fire(Coordinate.From("C7"));
        _ = sut.Fire(Coordinate.From("C6"));
        var (result, sunkShip) = sut.Fire(Coordinate.From("C5"));

        result.Should().Be(MoveResult.Hit);
        sunkShip.Should().Be(ship);
        sunkShip!.GetState().Should().Be(ShipState.Sunk);
    }

    [Fact]
    public void Fire_AllRoundsInShipCoordinate_ReturnsDamaged()
    {
        var ship = new Ship { Direction = Direction.N, Position = Coordinate.From("C8"), Type = ShipType.Destroyer };
        var sut = new Board(new List<Ship> { ship });

        _ = sut.Fire(Coordinate.From("C8"));
        _ = sut.Fire(Coordinate.From("C8"));
        _ = sut.Fire(Coordinate.From("C8"));
        var (result, sunkShip) = sut.Fire(Coordinate.From("C8"));

        result.Should().Be(MoveResult.Hit);
        sunkShip.Should().Be(ship);
        sunkShip!.GetState().Should().Be(ShipState.Damaged);
    }

    [Fact]
    public void DoesFleetStillExists_RunOnExistingFleet_ReturnsTrue()
    {
        var firstShip = new Ship { Direction = Direction.N, Position = Coordinate.From("C8"), Type = ShipType.Destroyer };
        var secondShip = new Ship { Direction = Direction.S, Position = Coordinate.From("E2"), Type = ShipType.Destroyer };
        var sut = new Board(new List<Ship> { firstShip, secondShip });

        _ = sut.Fire(Coordinate.From("C8"));
        _ = sut.Fire(Coordinate.From("C7"));
        _ = sut.Fire(Coordinate.From("C6"));
        var (lastHitResult, _) = sut.Fire(Coordinate.From("C5"));

        lastHitResult.Should().Be(MoveResult.Hit);
        firstShip.GetState().Should().Be(ShipState.Sunk);
        sut.DoesFleetStillExist().Should().BeTrue();
    }

    [Fact]
    public void DoesFleetStillExists_RunOnSunkFleet_ReturnsFalse()
    {
        var firstShip = new Ship { Direction = Direction.N, Position = Coordinate.From("C8"), Type = ShipType.Destroyer };
        var secondShip = new Ship { Direction = Direction.S, Position = Coordinate.From("E2"), Type = ShipType.Battleship };
        var sut = new Board(new List<Ship> { firstShip, secondShip });

        _ = sut.Fire(Coordinate.From("C8"));
        _ = sut.Fire(Coordinate.From("C7"));
        _ = sut.Fire(Coordinate.From("C6"));
        var (lastHitResult, _) = sut.Fire(Coordinate.From("C5"));

        lastHitResult.Should().Be(MoveResult.Hit);
        firstShip.GetState().Should().Be(ShipState.Sunk);

        _ = sut.Fire(Coordinate.From("E2"));
        _ = sut.Fire(Coordinate.From("E3"));
        _ = sut.Fire(Coordinate.From("E4"));
        _ = sut.Fire(Coordinate.From("E5"));
        (lastHitResult, _) = sut.Fire(Coordinate.From("E6"));

        lastHitResult.Should().Be(MoveResult.Hit);
        secondShip.GetState().Should().Be(ShipState.Sunk);

        sut.DoesFleetStillExist().Should().BeFalse();
    }
}