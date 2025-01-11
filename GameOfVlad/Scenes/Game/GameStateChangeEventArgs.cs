using System;
using GameOfVlad.Game;

namespace GameOfVlad.Scenes.Game;

public class GameStateChangeEventArgs(GameState gameState) : EventArgs
{
    public GameState GameState { get; } = gameState;
}