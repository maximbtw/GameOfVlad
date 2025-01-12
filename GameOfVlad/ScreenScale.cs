using System;
using Microsoft.Xna.Framework;

namespace GameOfVlad;

public static class ScreenScale
{
    public static float GlobalScale { get; private set; } = 1;
    
    public static Vector2 ScaleFactor { get; private set; } = Vector2.One;

    public static void UpdateScale(int baseWidth, int baseHeight, int currentWidth, int currentHeight)
    {
        float scaleX = (float)currentWidth / baseWidth;
        float scaleY = (float)currentHeight / baseHeight;
        
        GlobalScale = Math.Min(scaleX, scaleY);
        
        ScaleFactor = new Vector2(
            (float)currentWidth / baseWidth,
            (float)currentHeight / baseHeight
        );
    }

    public static Vector2 Scale(Vector2 value)
    {
        return value * ScaleFactor;
    }

    public static float Scale(float value)
    {
        return value * Math.Min(ScaleFactor.X, ScaleFactor.Y);
    }
}