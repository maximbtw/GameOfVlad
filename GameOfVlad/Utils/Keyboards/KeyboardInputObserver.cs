using System;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Utils.Keyboards;

public class KeyboardInputObserver
{
    private KeyboardState _previousKeyboardState;
    private KeyboardState _currentKeyboardState;
    
    public event Action<KeyEventArgs> KeyPressed;
    
    public event Action<KeyEventArgs> KeyDown;
    
    public event Action<KeyEventArgs> KeyUp;

    public KeyboardInputObserver()
    {
        _previousKeyboardState = Keyboard.GetState();
        _currentKeyboardState = _previousKeyboardState;
    }
    
    public void Update()
    {
        foreach (Keys key in Enum.GetValues(typeof(Keys)))
        {
            if (_previousKeyboardState.IsKeyUp(key) && _currentKeyboardState.IsKeyDown(key))
            {
                KeyDown?.Invoke(new KeyEventArgs(key));
            }

            if (_currentKeyboardState.IsKeyDown(key))
            {
                KeyPressed?.Invoke(new KeyEventArgs(key));
            }

            if (_previousKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key))
            {
                KeyUp?.Invoke(new KeyEventArgs(key));
            }
        }
        
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }
}