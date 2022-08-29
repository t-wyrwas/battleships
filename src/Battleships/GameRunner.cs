using Battleships.Domain;
using Battleships.Domain.Ships;
using Battleships.UI;

namespace Battleships;

public class GameRunner
{
    public GameRunner(IBoardDrawer boardDrawer, Board board)
    {
        _boardDrawer = boardDrawer;
        _board = board;
    }

    public void RunGame()
    {
        _boardDrawer.DrawBoard();
        foreach (var ship in _board.Fleet)
        {
            _boardDrawer.DrawShip(ship);
        }

        while (true)
        {
            var input = _boardDrawer.FetchUserInput();
            if(input.command == UserCommand.QuitGame)
            {
                return;
            }
            if(input.command == UserCommand.RestartGame)
            {
                throw new NotImplementedException();
            }
            if(input.command == UserCommand.NextMove)
            {
                var coordinate = input.coordinate!.Value;
                var (result, ship) = _board.Fire(coordinate);
                if(result == MoveResult.Hit)
                {
                    _boardDrawer.DrawHit(coordinate);
                    if(ship!.GetState() == ShipState.Sunk)
                    {
                        _boardDrawer.DrawShip(ship, asSunk: true);
                        _boardDrawer.WriteMessage("Ship sank!");
                    }
                }
                if(result == MoveResult.Miss)
                {
                    _boardDrawer.DrawMiss(coordinate);
                }
            }

            if(!_board.DoesFleetStillExist())
            {
                _boardDrawer.WriteMessage("You won!");
            }
        }
    }

    private IBoardDrawer _boardDrawer;
    private Board _board;
}