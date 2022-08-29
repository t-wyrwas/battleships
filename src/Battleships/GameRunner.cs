using Battleships.Domain;
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
        foreach(var ship in _board.Fleet)
        {
            _boardDrawer.DrawShip(ship);
        }
        _boardDrawer.FetchUserInput();
        // while(true)
        // {
        // }
    }

    private IBoardDrawer _boardDrawer;
    private Board _board;
}