using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.Utils.Draw;

public class LevelBoarderDrawer : IDisposable
{
    private Texture2D _pixelTexture;

    public void DrawLevelBorder(SpriteBatch spriteBatch, Vector2 startPoint, Size levelSize, Color borderColor)
    {
        if (_pixelTexture == null)
        {
            _pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _pixelTexture.SetData([Color.White]);
        }

        DrawHelper.DrawLine(spriteBatch, new Vector2(startPoint.X, startPoint.Y),
            new Vector2(startPoint.X + levelSize.Width, startPoint.Y), borderColor, _pixelTexture);

        DrawHelper.DrawLine(spriteBatch, new Vector2(startPoint.X + levelSize.Width, startPoint.Y),
            new Vector2(startPoint.X + levelSize.Width, startPoint.Y + levelSize.Height), borderColor, _pixelTexture);

        DrawHelper.DrawLine(spriteBatch, new Vector2(startPoint.X + levelSize.Width, startPoint.Y + levelSize.Height),
            new Vector2(startPoint.X, startPoint.Y + levelSize.Height), borderColor, _pixelTexture);

        DrawHelper.DrawLine(spriteBatch, new Vector2(startPoint.X, startPoint.Y + levelSize.Height),
            new Vector2(startPoint.X, startPoint.Y), borderColor, _pixelTexture);
    }

    public void Dispose()
    {
        _pixelTexture?.Dispose();
    }
}