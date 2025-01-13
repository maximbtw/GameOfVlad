using System;
using Microsoft.Xna.Framework;

namespace GameOfVlad.Utils;

public static class LevelHelper
{
    public static Vector2 GeneratePositionBehindLevel(Random random, Rectangle levelBounds, int offset)
    {
        int minX = levelBounds.X - offset;
        int maxX = levelBounds.X + levelBounds.Width + offset;

        int minY = levelBounds.Y - offset;
        int maxY = levelBounds.Y + levelBounds.Height + offset;

        int x, y;
        
        var isBoundedByHorizontal = random.Next(0, 2) == 0;
        if (isBoundedByHorizontal)
        {
            x = random.Next(0, 2) == 0 ? minX : maxX;
            y = random.Next(minY, maxY);
        }
        else
        {
            x = random.Next(minX, maxX);
            y = random.Next(0, 2) == 0 ? minY : maxY;
        }

        return new Vector2(x, y);
    }

    public static bool IsPositionBehindLevel(Vector2 position, Rectangle levelBounds, int offset)
    {
        return 
            position.X > levelBounds.X + levelBounds.Width + offset ||
            position.X < levelBounds.X - offset ||
            position.Y > levelBounds.Y + levelBounds.Height + offset ||
            position.Y < levelBounds.Y - offset;
    }
}