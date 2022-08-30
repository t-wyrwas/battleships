using Battleships.Domain;
using Battleships.Domain.Ships;
using Battleships.UI;

namespace Battleships;

public class GameRunner
{
    public GameRunner(IBoardDrawer boardDrawer, IFleetGenerator fleetGenerator)
    {
        _boardDrawer = boardDrawer;
        _fleetGenerator = fleetGenerator;
    }

    public void RunGame()
    {
        var board = GetEmptyBoard();
        DrawBoard(board);
        var gameFinished = false;

        while (true)
        {
            var input = _boardDrawer.FetchUserInput();
            if (input.command == UserCommand.QuitGame)
            {
                return;
            }
            if (input.command == UserCommand.RestartGame)
            {
                board = GetEmptyBoard();
                DrawBoard(board);
                gameFinished = false;
            }
            if (input.command == UserCommand.NextMove && !gameFinished)
            {
                var coordinate = input.coordinate!.Value;
                var (result, ship) = board.Fire(coordinate);
                if (result == MoveResult.Hit)
                {
                    _boardDrawer.DrawHit(coordinate);
                    if (ship!.GetState() == ShipState.Sunk)
                    {
                        _boardDrawer.DrawShip(ship, asSunk: true);
                        _boardDrawer.WriteMessage("Ship sank!");
                    }
                    else
                    {
                        _boardDrawer.WriteMessage("Hit!");
                    }
                }
                if (result == MoveResult.Miss)
                {
                    _boardDrawer.DrawMiss(coordinate);
                    _boardDrawer.WriteMessage("Missed!");
                }
            }

            gameFinished = !board.DoesFleetStillExist();
            if (gameFinished)
            {
                _boardDrawer.WriteMessage("You won!");
            }
        }
    }

    private Board GetEmptyBoard() => new Board(_fleetGenerator.GenerateFleet(new [] {ShipType.Battleship, ShipType.Destroyer, ShipType.Destroyer}));
    private void DrawBoard(Board board)
    {
        _boardDrawer.DrawBoard();
        foreach (var ship in board.Fleet)
        {
            _boardDrawer.DrawShip(ship);
        }
    }

    private IBoardDrawer _boardDrawer;
    private IFleetGenerator _fleetGenerator;
}