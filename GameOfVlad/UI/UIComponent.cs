using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfVlad.UI;

public abstract class UiComponent : IGameObject
{
    public Vector2 Position { get; set; } = new(0, 0);
    
    public Texture2D Texture { get; set; }
    
    public Color Color { get; set; } = Color.White;

    public IGameObject Parent { get; set; } = null!;
    public IEnumerable<IGameObject> Children { get; set; }
    public bool IsActive { get; set; } = true;
    
    public virtual int DrawOrder => (int)DrawOrderType.FrontCanvas;

    public virtual int UpdateOrder => 0;

    protected virtual Rectangle BoundingBox
    {
        get
        {
            if (this.Texture == null)
            {
                return Rectangle.Empty;
            }

            return new((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }

    public void Init(ContentManager content)
    {
        InitCore(content);
    }

    public void Terminate()
    {
        TerminateCore();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawCore(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        UpdateCore(gameTime);
    }
    
    protected virtual void DrawCore(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.Texture, this.Position, this.Color);
    }

    protected virtual void UpdateCore(GameTime gameTime)
    {
    }

    protected virtual void InitCore(ContentManager content)
    {
    }
    
    protected virtual void TerminateCore()
    {
    }
}