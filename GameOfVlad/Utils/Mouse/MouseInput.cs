using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Utils.Mouse;

public class MouseInput
{
    private MouseState _previousMouseState;
    private MouseState _currentMouseState;

    public void Update()
    {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
    }

    public bool IsLeftClick() =>
        _previousMouseState.LeftButton == ButtonState.Released &&
        _currentMouseState.LeftButton == ButtonState.Pressed;

    public bool IsRightClick() =>
        _previousMouseState.RightButton == ButtonState.Released &&
        _currentMouseState.RightButton == ButtonState.Pressed;

    public Vector2 GetMousePosition() => new(_currentMouseState.X, _currentMouseState.Y);
}