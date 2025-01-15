using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace GameOfVlad.Utils.Mouse;

public class MouseInput
{
    private MouseState _previousMouseState;
    private MouseState _currentMouseState;
    
    public event EventHandler<MouseEventArgs> OnLeftClick;
    public event EventHandler<MouseEventArgs> OnRightClick;
    public event EventHandler<MouseEventArgs> OnScrollWheelUp;
    public event EventHandler<MouseEventArgs> OnScrollWheelDown;

    public void Update()
    {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        
        if (IsLeftClick())
        {
            OnLeftClick?.Invoke(this, new MouseEventArgs(GetMousePosition()));
        }

        if (IsRightClick())
        {
            OnRightClick?.Invoke(this, new MouseEventArgs(GetMousePosition()));
        }
        
        if (IsScrollWheelUp())
        {
            OnScrollWheelUp?.Invoke(this, new MouseEventArgs(GetMousePosition()));
        }

        if (IsScrollWheelDown())
        {
            OnScrollWheelDown?.Invoke(this, new MouseEventArgs(GetMousePosition()));
        }
    }
    
    public bool IsLeftButtonPressed() => _currentMouseState.LeftButton == ButtonState.Pressed;
    
    public bool IsRightButtonPressed() => _currentMouseState.RightButton == ButtonState.Pressed;

    public bool IsLeftClick() =>
        _previousMouseState.LeftButton == ButtonState.Released &&
        _currentMouseState.LeftButton == ButtonState.Pressed;

    public bool IsRightClick() =>
        _previousMouseState.RightButton == ButtonState.Released &&
        _currentMouseState.RightButton == ButtonState.Pressed;

    public bool IsScrollWheelUp() => _previousMouseState.ScrollWheelValue < _currentMouseState.ScrollWheelValue;

    public bool IsScrollWheelDown() =>  _previousMouseState.ScrollWheelValue > _currentMouseState.ScrollWheelValue;

    public Vector2 GetMousePosition() => new(_currentMouseState.X, _currentMouseState.Y);
}