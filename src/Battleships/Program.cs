using Battleships.ConsoleUI;
using Battleships.Domain.Ships;
using Battleships;

var drawer = new ConsoleDrawer(5, 5);
var fleetGenerator = new DummyFleetGenerator();
var gameRunner = new GameRunner(drawer, fleetGenerator);

gameRunner.RunGame();
