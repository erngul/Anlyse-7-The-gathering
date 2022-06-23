// See https://aka.ms/new-console-template for more information

// Console.WriteLine("Hello, World!");

using The_gathering_v2.Models;

var game = new Game();
// Game.CreateCurrentState();
game.CreateCurrentState();
game.GeneralBoard.NotifyPreparationPhase();
game.GeneralBoard.GetACardFromTheDeck();
game.GeneralBoard.NotifyDrawingPhase();
game.GeneralBoard.AddPermanentCardToBoard(0);
game.GeneralBoard.NotifyMainPhase();
game.GeneralBoard.NotifyEndingPhase();
game.NextTurn();
game.GeneralBoard.NotifyPreparationPhase();
game.GeneralBoard.GetACardFromTheDeck();
game.GeneralBoard.NotifyDrawingPhase();
game.GeneralBoard.AddPermanentCardToBoard(0);
game.GeneralBoard.NotifyMainPhase();
game.GeneralBoard.NotifyEndingPhase();
return;
// game.GeneralBoard.


