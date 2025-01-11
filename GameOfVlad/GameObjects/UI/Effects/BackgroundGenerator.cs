using System;
using System.Collections.Generic;
using GameOfVlad.GameRenderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.UI.Effects;

public class BackgroundGenerator : GameObject, IGameObject
{
    public int DrawOrder => (int)DrawOrderType.Background;
    public int UpdateOrder => 1;

    public override IEnumerable<IRendererObject> ChildrenAfter
    {
        get => _backgroundImages;
        set => throw new NotSupportedException();
    }

    private const int DrawOffset = 1000;
    
    private readonly Rectangle _levelBounds;
    private readonly List<BackgroundImage> _backgroundImages = new();
    
    public BackgroundGenerator(Texture2D texture, Rectangle levelBounds)
    {
        this.Texture = texture;

        _levelBounds = levelBounds;
    }

    protected override void LoadCore()
    {
        int textureWidth = this.Texture.Width;
        int textureHeight = this.Texture.Height;

        var horizontalTiles = (int)Math.Ceiling((_levelBounds.Width + DrawOffset * 2) / (float)textureWidth);
        var verticalTiles = (int)Math.Ceiling((_levelBounds.Height + DrawOffset * 2) / (float)textureHeight);
        
        for (int y = 0; y < verticalTiles; y++)
        {
            for (int x = 0; x < horizontalTiles; x++)
            {
                var position = new Vector2(
                    _levelBounds.X - DrawOffset + x * this.Texture.Width,
                    _levelBounds.Y - DrawOffset + y * this.Texture.Height
                );

                var image = new BackgroundImage
                {
                    Texture = this.Texture,
                    Position = position
                };

                _backgroundImages.Add(image);
            }
        }

        base.LoadCore();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
    
    private class BackgroundImage : GameObject, IGameObject
    {
        public int DrawOrder => (int)DrawOrderType.Background;
        public int UpdateOrder => 1;
    }
}