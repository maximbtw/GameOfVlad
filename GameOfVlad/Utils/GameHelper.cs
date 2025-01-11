using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils;

public static class GameHelper
{
    public static Vector2 GetDirectionByVelocity(Vector2 velocity)
    {
        var direction = new Vector2(velocity.X, velocity.Y);
        direction.Normalize();

        return direction;
    }
    
    public static float AngleToRadians(float angle) => MathF.PI * angle / 180f;

    public static Vector2 AdjustPositionByTexture(Vector2 position, Texture2D texture)
    {
        var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
        
        return position - origin;
    }
    
    public static Vector2 GetDirectionByRotationInDegree(float rotationInDegrees)
    {
        float rotationInRadians = AngleToRadians(rotationInDegrees);

        return GetDirectionByRotationInRadians(rotationInRadians);
    }
    
    public static Vector2 GetDirectionByRotationInRadians(float rotationInRadians)
    {
        var direction = new Vector2(MathF.Cos(rotationInRadians), MathF.Sin(rotationInRadians));
        
        direction.Normalize();

        return direction;
    }
    
    public static Vector2 CenterObjectOnScreen(Texture2D texture, Viewport viewport)
    {
        var screenCenter = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        
        var textureCenter = new Vector2(texture.Width / 2f, texture.Height / 2f);
        
        return screenCenter - textureCenter;
    }
    
    public static Vector2 GetOffsetDirectionByRandom(Random random, Vector2 direction, Range<int> offsetAngleRange)
    {
        float randomAngleInDegrees = random.Next(offsetAngleRange.MinValue, offsetAngleRange.MaxValue + 1);
        float randomAngleInRadians = AngleToRadians(randomAngleInDegrees);
        
        float cos = MathF.Cos(randomAngleInRadians);
        float sin = MathF.Sin(randomAngleInRadians);
        
        return new Vector2(
            direction.X * cos - direction.Y * sin,
            direction.X * sin + direction.Y * cos
        );
    }
}
