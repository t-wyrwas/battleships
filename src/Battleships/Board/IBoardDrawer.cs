namespace Battleships.Board;
using Battleships.Domain;

public interface IBoardDrawer
{
    void DrawBoard();
    void DrawHit(Coordinate coordinate);
    void DrawMiss(Coordinate coordinate);
}