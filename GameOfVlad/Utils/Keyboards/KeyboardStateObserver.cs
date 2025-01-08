using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Utils.Keyboards;

public sealed class KeyboardStateObserver
{
    public event EventHandler<KeyEventArgs> KeyPressed;
    public event EventHandler<KeyEventArgs> KeyUnpressed;
    public event EventHandler<KeyboardStateEventArgs> OnUpdated;
    

    private KeyboardState _previousState = Keyboard.GetState();

    public void Update(GameTime gameTime)
    {
        KeyboardState currentState = Keyboard.GetState();
        foreach (Keys key in currentState.GetPressedKeys())
        {
            if (!_previousState.IsKeyDown(key))
            {
                KeyPressed?.Invoke(this, new KeyEventArgs(key));
            }
        }

        foreach (Keys key  in _previousState.GetPressedKeys())
        {
            if (!currentState.IsKeyDown(key))
            {
                KeyUnpressed?.Invoke(this, new KeyEventArgs(key));
            }
        }
        
        OnUpdated?.Invoke(this, new KeyboardStateEventArgs(currentState, gameTime));

        _previousState = currentState;
    }
}