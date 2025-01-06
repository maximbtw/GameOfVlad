using System;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Tools.Keyboards;

public class KeyEventArgs(Keys key) : EventArgs
{
    public Keys Key { get; } = key;
}