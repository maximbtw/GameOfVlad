using Microsoft.Xna.Framework;

namespace GameOfVlad.Utils;

public class FpsUpdater
{
    public int Fps { get; private set; }
    
    private int _frameCount;
    private double _elapsedTime;

    public void Update(GameTime gameTime)
    {
        // Считаем количество кадров и время
        _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        _frameCount++;

        if (_elapsedTime >= 1)
        {
            this.Fps = _frameCount;
            _frameCount = 0;
            _elapsedTime = 0;
        }
    }
}