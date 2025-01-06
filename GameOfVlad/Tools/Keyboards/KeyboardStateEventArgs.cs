using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Tools.Keyboards;

public class KeyboardStateEventArgs(KeyboardState keyboardState, GameTime gameTime) : EventArgs
{
    public KeyboardState Key { get; } = keyboardState;
    
    public GameTime GameTime { get; } = gameTime;
}