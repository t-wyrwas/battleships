using Battleships.Domain;
using Battleships.Domain.Ships;

namespace Battleships.UI;

public interface IBoardDrawer
{
    void DrawBoard();
    void DrawHit(Coordinate coordinate);
    void DrawMiss(Coordinate coordinate);
    void DrawShip(Ship ship);
    (UserCommand command, Coordinate? coordinate) FetchUserInput();
}