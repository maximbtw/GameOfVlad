using Microsoft.Xna.Framework;

namespace GameOfVlad;

public class ResolutionManager(GraphicsDeviceManager graphics)
{
    private const int BaseWidth = 2560;
    private const int BaseHeight = 1440;

    private float ScaleX { get; set; } = 1f; 
    private float ScaleY { get; set; } = 1f; 

    public void SetResolution(int width, int height, bool isFullScreen)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.IsFullScreen = isFullScreen;
        
        graphics.ApplyChanges();

        // Пересчёт масштабов
        ScaleX = (float)width / BaseWidth;
        ScaleY = (float)height / BaseHeight;

        ScreenScale.UpdateScale(
            baseWidth: 2560,
            baseHeight: 1440, 
            graphics.GraphicsDevice.Viewport.Width,
            graphics.GraphicsDevice.Viewport.Height);
    }

    public void ChangeFullScreen()
    {
        graphics.IsFullScreen = !graphics.IsFullScreen;
        
        graphics.ApplyChanges();
    }
}