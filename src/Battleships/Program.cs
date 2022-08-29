using Battleships.ConsoleUI;
using Battleships.Domain;
using Battleships.Domain.Ships;
using Battleships;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Batteships!");

var drawer = new ConsoleDrawer(5, 5);
var fleetGenerator = new DummyFleetGenerator();
var board = new Board(fleetGenerator.GenerateFleet());
var gameRunner = new GameRunner(drawer, board);

gameRunner.RunGame();
