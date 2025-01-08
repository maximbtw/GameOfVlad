using System;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.UI.Background;

public class BackgroundGenerator : UiComponent
{
    public override int DrawOrder => (int)DrawOrderType.Background;

    private readonly Vector2 _startPoint;
    private readonly Size _size;

    private int _horizontalTiles;
    private int _verticalTiles;

    public BackgroundGenerator(Texture2D texture, Vector2 startPoint, Size size)
    {
        this.Texture = texture;
        _startPoint = startPoint;
        _size = size;
    }

    protected override void InitCore(ContentManager content)
    {
        base.InitCore(content);
        
        int textureWidth = this.Texture.Width;
        int textureHeight = this.Texture.Height;
        
        _horizontalTiles = (int)Math.Ceiling(_size.Width / textureWidth);
        _verticalTiles = (int)Math.Ceiling(_size.Height / textureHeight);
    }

    protected override void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int y = 0; y < _verticalTiles; y++)
        {
            for (int x = 0; x < _horizontalTiles; x++)
            {
                var position = new Vector2(
                    _startPoint.X + x * this.Texture.Width,
                    _startPoint.Y + y * this.Texture.Height
                );
                
                spriteBatch.Draw(this.Texture, position, this.Color);
            }
        }
    }
}