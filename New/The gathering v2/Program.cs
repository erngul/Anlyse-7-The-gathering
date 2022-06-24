/*
Eren Gul 0993650
Kaykhosrow Hasany 0998409
*/

// See https://aka.ms/new-console-template for more information


// Console.WriteLine("Hello, World!");

using The_gathering_v2.Models;
using The_gathering_v2.Models.Colors;

var game = new Game();
// Game.CreateCurrentState();
game.CreateCurrentState();
game.GeneralBoard.NotifyPreparationPhase();
game.GeneralBoard.GetACardFromTheDeck();
game.GeneralBoard.AddLandCardToBoard();
game.GeneralBoard.UseLandEnergy(game.GeneralBoard.Attacker, new Blue());
game.GeneralBoard.UseLandEnergy(game.GeneralBoard.Attacker, new Blue());
game.GeneralBoard.AddPermanentCardToBoard(0);
game.GeneralBoard.NotifyDrawingPhase();
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


