using System;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Utils.Keyboards;

public class KeyEventArgs(Keys key) : EventArgs
{
    public Keys Key { get; } = key;
}