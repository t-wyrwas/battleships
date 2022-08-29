using Battleships.Domain;
using Battleships.Domain.Ships;

namespace Battleships.UI;

public interface IBoardDrawer
{
    void DrawBoard();
    void DrawHit(Coordinate coordinate);
    void DrawMiss(Coordinate coordinate);
    void DrawShip(Ship ship, bool asSunk = false);
    (UserCommand command, Coordinate? coordinate) FetchUserInput();
    void WriteMessage(string msg);
}