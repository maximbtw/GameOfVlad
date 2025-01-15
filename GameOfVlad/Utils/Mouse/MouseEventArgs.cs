using System;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Utils.Mouse;

public class MouseEventArgs(Vector2 mousePosition) : EventArgs
{
    public Vector2 MousePosition { get; set; } = mousePosition;
}