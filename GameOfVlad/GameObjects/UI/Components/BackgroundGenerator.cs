using System;
using GameOfVlad.GameObjects.UI.Interfaces;
using GameOfVlad.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Components;

public class BackgroundGenerator : UiComponent, IUiComponent
{
    public int DrawOrder => (int)DrawOrderType.Background;
    public int UpdateOrder => 1;

    private const int DrawOffset = 1000;

    private readonly Rectangle _levelBounds;

    private int _horizontalTiles;
    private int _verticalTiles;

    public BackgroundGenerator(ContentManager contentManager, Texture2D texture, Rectangle levelBounds) :
        base(contentManager)
    {
        this.Texture = texture;

        _levelBounds = levelBounds;
    }

    protected override void LoadCore()
    {
        int textureWidth = this.Texture.Width;
        int textureHeight = this.Texture.Height;

        _horizontalTiles = (int)Math.Ceiling((_levelBounds.Width + DrawOffset * 2) / (float)textureWidth);
        _verticalTiles = (int)Math.Ceiling((_levelBounds.Height + DrawOffset * 2) / (float)textureHeight);

        base.LoadCore();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for (int y = 0; y < _verticalTiles; y++)
        {
            for (int x = 0; x < _horizontalTiles; x++)
            {
                var position = new Vector2(
                    _levelBounds.X - DrawOffset + x * this.Texture.Width,
                    _levelBounds.Y - DrawOffset + y * this.Texture.Height
                );

                spriteBatch.Draw(this.Texture, position, this.Color);
            }
        }
    }
}