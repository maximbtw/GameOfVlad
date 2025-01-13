using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.GameObjects.Effects.Generators;

public class BackgroundGenerator : GameObject, IGameObject
{
    public int DrawOrder => (int)DrawOrderType.Background;
    public int UpdateOrder => 1;

    private const int DrawOffset = 2000;
    
    private readonly Rectangle _levelBounds;
    private readonly IEffectDrawer _effectDrawer;
    
    public BackgroundGenerator(IEffectDrawer effectDrawer, Texture2D texture, Rectangle levelBounds)
    {
        this.Texture = texture;

        _effectDrawer = effectDrawer;
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

                _effectDrawer.AddEffect(image);
            }
        }

        base.LoadCore();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }
    
    private class BackgroundImage : GameObject, IEffect
    {
        public int DrawOrder => (int)DrawOrderType.Background;
        public int UpdateOrder => 1;
    }
}