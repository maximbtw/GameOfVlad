using Microsoft.Xna.Framework.Input;

namespace GameOfVlad.Tools
{
    public class StateKeyboard
    {
        private KeyboardState currentState;
        private KeyboardState prevState;

        public void UpdateStart()
        {
            currentState = Keyboard.GetState();
        }

        public void UpdateEnd()
        {
            prevState = currentState;
        }

        public bool CommandUp(Keys key)
        {
            return currentState.IsKeyUp(key) && prevState.IsKeyDown(key);
        }

        public bool CommandDown(Keys key)
        {
            return currentState.IsKeyDown(key) && prevState.IsKeyUp(key);
        }

    }
}
