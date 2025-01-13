using Microsoft.Xna.Framework;

namespace GameOfVlad.Utils;

public class Timer
{
    public float Time { get; private set; }

    public void Update(GameTime gameTime)
    {
        this.Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
    
    public void Reset()
    {
        this.Time = 0;
    }
}