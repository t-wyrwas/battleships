using Battleships.ConsoleBoard;
using Battleships.Domain;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Batteships!");

var drawer = new ConsoleDrawer(5, 5);

drawer.DrawBoard();
drawer.DrawHit(Coordinate.From("C8"));
drawer.DrawHit(Coordinate.From("A5"));

drawer.DrawMiss(Coordinate.From("D10"));
drawer.DrawMiss(Coordinate.From("B3"));

Console.ReadLine();